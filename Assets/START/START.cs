using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;
using TMPro;

public class START : MonoBehaviour
{
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scene
    public GameObject Back; // Close Button
    public GameObject page_START; // the page which you want to close
    public Text energy; // energy value
    private ServerMethod.Server ServerScript; // Server.cs


    // Start is called before the first frame update
    void Start()
    {
        ServerScript = FindObjectOfType<ServerMethod.Server>();
    }

    // Update is called once per frame
    void Update()
    {
        Update_values(); // Update energy
    }

    // When click < Level >
    public void Button_Level()
    {
        // call the PVE
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        // detect whether your energy is sufficient
        if(ServerScript.energy<5)   return;
        
        GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        string buttonTag = clickedButton.tag;
        Debug.Log("Clicked button tag: " + buttonTag);
        switch(buttonTag)
        {
            case "11":
                GameManage.currentLevel=11;
                //Debug.Log("START:"+GameManage.currentLevel);
                break;
            case "12":
                GameManage.currentLevel=12;
                break;
            case "13":
                GameManage.currentLevel=13;
                break;
            case "14":
                GameManage.currentLevel=14;
                break;
            case "15":
                GameManage.currentLevel=15;
                break;
            case "16":
                GameManage.currentLevel=16;
                break;
            case "21":
                GameManage.currentLevel=21;
                break;
            case "22":
                GameManage.currentLevel=22;
                break;
            case "23":
                GameManage.currentLevel=23;
                break;
            case "24":
                GameManage.currentLevel=24;
                break;
            case "25":
                GameManage.currentLevel=25;
                break;
            case "26":
                GameManage.currentLevel=26;
                break;
        }
        
        SceneManager.LoadScene("Background", LoadSceneMode.Single);
        
    }

    // When click < BACK > 
    public void Button_Close()
    {
        page_START.SetActive(false);
        ALL_Button.SetActive(true); // Open Main_Scene
    }

    // Update energy && money && tear
    public void Update_values()
    {
        energy.text = ServerScript.energy.ToString() + "/30";
    }

}
