using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;
using TMPro;
using ServerMethod;
using System;

public class START : MonoBehaviour
{
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scene
    public GameObject Back; // Close Button
    public GameObject page_START; // the page which you want to close
    public Text energy; // energy value
    private ServerMethod.Server ServerScript; // Server.cs
    public GameObject hint; // the hint about Insufficient energy
    public GameObject[] Unlock; // the unlocked level image
    public GameObject[] Lock; // the locked level image
    private Coroutine endCoroutine;

    public GameObject Cat;

    private int[] Cat_x=new int[13]{-698,-524,-221,-78,-137,-80,128,333,366,537,730,814,-893};
    private int[] Cat_y=new int[13]{-118,-236,-252,-147,46,228,378,126,-70,-211,13,213,-100};
    // Start is called before the first frame update
    void Start()
    {
        if(MainMenu.message!=87)    ServerScript = FindObjectOfType<ServerMethod.Server>();
    }

    // Update is called once per frame
    void Update()
    {
        if(MainMenu.message==87)
        {
            energy.text = "30/30";
            for(int i = 0; i<12 ; i++)
            {
                Unlock[i].gameObject.SetActive(true);
                Lock[i].gameObject.SetActive(false);
            }
            RectTransform cat_position = Cat.GetComponent<RectTransform>();
            cat_position.anchoredPosition = new Vector2(Cat_x[11], Cat_y[11]);
            return;
        }

        if(ServerScript.clearance.Length!=0)
        {
            Update_values(); // Update energy
        }
        RectTransform catRectTransform = Cat.GetComponent<RectTransform>();
        catRectTransform.anchoredPosition = new Vector2(Cat_x[CurrentStage()], Cat_y[CurrentStage()]);
    }

    private int CurrentStage()
    {
        int stage = 12;  // 初始化为一个不可能的值

        for (int i = 0; i < 12; i++)
        {
            if (ServerScript.clearance[i] == 0)
            {
                stage = i-1;
                break;
            }
        }
        if(stage==12 && ServerScript.clearance[11]!=0)  stage = 11;
        if(stage==-1)stage=12;
        
        return stage;
    }
    // When click < Level >
    public void Button_Level()
    {
        GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        string buttonTag = clickedButton.tag;
        GameManage.currentLevel = int.Parse(buttonTag);
        
        if(MainMenu.message==87)    SceneManager.LoadScene("Background", LoadSceneMode.Single);

        // detect whether your energy is sufficient
        if(ServerScript.energy<5)   
        {
            endCoroutine = StartCoroutine(Enengy_Hint(1f));
            return;
        }
        // 真實關卡前同步
        for(int i = 0; i<6*((GameManage.currentLevel/10)-1) + (GameManage.currentLevel%10-1) ; i++)
        {
            if(ServerScript.clearance[i]==0)    return;
        }  
        StartCoroutine(Surver_Before_Game((result) => 
        {
            Debug.Log(result);
            if(result==true)
            {
                for(int i = 0; i<6*((GameManage.currentLevel/10)-1) + (GameManage.currentLevel%10-1) ; i++)
                {
                    if(ServerScript.clearance[i]==0)           return;
                }
                SceneManager.LoadScene("Background", LoadSceneMode.Single);
            }
            else
            {
                endCoroutine = StartCoroutine(Enengy_Hint(1f));
                return;
            }
        }));
    }

    private IEnumerator Surver_Before_Game(Action<bool> callback)
    {
        IEnumerator coroutine = ServerScript.beforeGame();
        yield return StartCoroutine(coroutine);
        
        Return result = coroutine.Current as Return;
        bool bool_success = false;
        if(result!=null)
        {
            bool_success = result.success;
        }

        callback.Invoke(bool_success);
    }

    // When click < BACK > 
    public void Button_Close()
    {
        if(endCoroutine!=null)
        {
            StopCoroutine(endCoroutine);
            endCoroutine = null;
        }
        page_START.SetActive(false);
        ALL_Button.SetActive(true); // Open Main_Scene
    }

    // Update energy && money && tear
    public void Update_values()
    {
        energy.text = ServerScript.energy.ToString() + "/30";
        for(int i = 0; i<ServerScript.clearance.Length ; i++)
        {
            Unlock[i].gameObject.SetActive(false);
            Lock[i].gameObject.SetActive(true);
            if(ServerScript.clearance[i]!=0)
            {
                Unlock[i].gameObject.SetActive(true);
                Lock[i].gameObject.SetActive(false);
            }
        }
    }

    // the hint about insufficient energy 
    IEnumerator Enengy_Hint(float delay)
    {
        hint.SetActive(false);
        hint.SetActive(true);
        yield return new WaitForSeconds(delay);
        hint.SetActive(false);
    }
}
