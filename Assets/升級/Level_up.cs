using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_up : MonoBehaviour
{
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scene
    public GameObject Back; // Close Button
    public GameObject page_Level_up; // the page which you want to close
    public GameObject Upgrade; // Upgrade Button

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When click < BACK >
    public void Button_Back()
    {
        page_Level_up.SetActive(false);
        ALL_Button.SetActive(true); // Open All button in Main_Scene
    }

    // When click < Upgrade >
    public void Button_Upgrade()
    {
        
    }
}
