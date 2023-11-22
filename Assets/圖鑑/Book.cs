using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scene
    public GameObject Last; // Last Button
    public GameObject Back; // Close Button
    public GameObject page_Book; // the page which you want to close
    public GameObject Next; // Next Button
    public GameObject[] pages; // The all page in Book
    private int Current = 0; // The current position about your location in Book
    private ServerMethod.Server ServerScript; // Server.cs

    // Start is called before the first frame update
    void Start()
    {
        ShowPage(0);
        ServerScript = FindObjectOfType<ServerMethod.Server>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        page_Book.SetActive(false);
        ALL_Button.SetActive(true); // Open All button in Main_Scene
    }

    // When click < > >
    public void Button_Next()
    {
        int Last_page = -1;
        for(int i = 0; i<ServerScript.character.Length ; i++)
        {
            if(ServerScript.character[i]!=0)
            {
                Last_page = i;
            }
        }
        // Next page
        if(Current <= Last_page)
        {
            Current = Current + 1;
            if(Current>=Last_page)
            {
                Current = Last_page;
            }
            ShowPage(Current);
        }
    }

    private void ShowPage(int Current)
    {
        // Show current page && Hide the others
        for(int i = 0; i<pages.Length; i++)
        {
            if(i==Current)
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }
    }
}
