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
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Background", LoadSceneMode.Single);
    }

    // When click < BACK > 
    public void Button_Close()
    {
        page_START.SetActive(false);
        ALL_Button.SetActive(true); // Open Main_Scene
    }
}
