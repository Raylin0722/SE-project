using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

public class START : MonoBehaviour
{
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scene
    public GameObject Back; // Close Button
    public GameObject page_START; // the page which you want to close
    public AudioSource Music_Main_Scene; // the Music in Main Scene


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When click < Level >
    public void Button_Level()
    {
        // call the PVE
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        
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
        }
        
        SceneManager.LoadScene("Background", LoadSceneMode.Single);
        
    }

    // When click < BACK > 
    public void Button_Close()
    {
        page_START.SetActive(false);
        ALL_Button.SetActive(true); // Open Main_Scene
    }
}
