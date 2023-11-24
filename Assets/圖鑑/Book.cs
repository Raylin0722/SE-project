using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scene
    public GameObject Last; // Last Button
    public GameObject Back; // Close Button
    public GameObject page_Book; // the page which you want to close
    public GameObject Next; // Next Button
    public GameObject[] pages; // The all page in Book
    public Image[] Pictures; // The all pictures in Book
    private int Current = 0; // The current position about your location in Book
    private ServerMethod.Server ServerScript; // Server.cs

    // Start is called before the first frame update
    void Start()
    {
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        ShowPage(0);
    }

    // Update is called once per frame
    void Update()
    {
        ShowPage(Current);
    }

    // When click < < >
    public void Button_Last()
    {
        // Next page
        if(Current > 0)
        {
            Current = Current - 1;
            ShowPage(Current);
        }
    }

    // When click < BACK >
    public void Button_Back()
    {
        Current=0;
        ShowPage(0);
        page_Book.SetActive(false);
        ALL_Button.SetActive(true); // Open All button in Main_Scene
    }

    // When click < > >
    public void Button_Next()
    {
        // Next page
        if(Current<7)
        {
            Current = Current + 1;
            if(Current>=7)
            {
                Current = 6;
            }
            ShowPage(Current);
        }
    }

    private void ShowPage(int Current)
    {
        // Show current page && Hide the others
        for(int i = 0; i<pages.Length; i++)
        {
            for(int j = 0; j<4 ; j++)
            {
                Pictures[7*j+i].gameObject.SetActive(false);
            }
            if(i==Current)
            {
                pages[i].SetActive(true);
                Pictures[7*(ServerScript.faction-1)+i].gameObject.SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }
    }
}
