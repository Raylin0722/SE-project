using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class FruitSelect : MonoBehaviour
{
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scene
    public GameObject page_FruitSelect; // the page for page_FruitSelect
    [SerializeField] GameObject bFrame;
    [SerializeField] GameObject wFrame;
    static public int fruit;// 0->watermelon, 1->banana


    // Server.cs
    private ServerMethod.Server ServerScript;
    [SerializeField] int[] Faction = {0,2,1,0,1,0};

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
        Faction[0] = 0;
        Faction[1] = fruit + 2;
        Faction[fruit+2] = 1;
        Faction[fruit+1] = 0;
        // Your code
        page_FruitSelect.SetActive(false); // Close All button in Fruit Select
        ALL_Button.SetActive(true); // Open All button in Main_Scene
    }

    // Close All button in Main_Scene
    public void Fruit_Select()
    {
        if(Faction[0]==1)
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

    public void bhighlight()
    {
        bFrame.SetActive(true);
        wFrame.SetActive(false);
        fruit=1;
    }
    public void whighlight()
    {
        wFrame.SetActive(true);
        bFrame.SetActive(false);
        fruit=0;

    }
}
