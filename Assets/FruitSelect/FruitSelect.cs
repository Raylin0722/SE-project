using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSelect : MonoBehaviour
{
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scene
    public GameObject page_FruitSelect; // the page for page_FruitSelect

    // Server.cs
    private ServerMethod.Server ServerScript;

    // Start is called before the first frame update
    void Start()
    {
        ServerScript = FindObjectOfType<ServerMethod.Server>();
    }

    // Update is called once per frame
    void Update()
    {
        //Fruit_Select(); // This is only for you to test,and you can delete it.
    }

    // Check Button
    public void Check()
    {
        // Your code

        page_FruitSelect.SetActive(false); // Open All button in Fruit Select
        ALL_Button.SetActive(true); // Open All button in Main_Scene
    }

    // Close All button in Main_Scene
    public void Fruit_Select()
    {
        if(ServerScript.faction==0)
        {
            page_FruitSelect.SetActive(true); // Open All button in Fruit Select
            ALL_Button.SetActive(false); // Close All button in Main_Scene
        }
        else
        {
            page_FruitSelect.SetActive(false); // Close All button in Fruit Select
            ALL_Button.SetActive(true); // Open All button in Main_Scene
        }
    }
}
