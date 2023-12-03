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
    static public int fruit = -1;// 0->watermelon, 1->banana

    // Server.cs
    private ServerMethod.Server ServerScript;

    // Start is called before the first frame update
    void Start()
    {
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        Fruit_Select();
    }

    // Update is called once per frame
    void Update()
    {
        Fruit_Select(); // This is only for you to test,and you can delete it.
    }

    // Check Button
    public void Check()
    {
        wFrame.SetActive(false);
        bFrame.SetActive(false);
        if(fruit==-1)   return;
        else if(fruit==0)
        {
            ServerScript.faction[2] = 1;
            ServerScript.faction[3] = 0;
        }
        else
        {
            ServerScript.faction[2] = 0;
            ServerScript.faction[3] = 1;
        }
        ServerScript.faction[0] = 0;
        ServerScript.faction[1] = fruit + 2;
        fruit = -1;
    }

    // Close All button in Main_Scene
    public void Fruit_Select()
    {
        if(ServerScript.faction[0]==1)
        {
            page_FruitSelect.SetActive(true); // Open All button in Fruit Select
            ALL_Button.SetActive(false); // Close All button in Main_Scene
        }
        else
        {
            page_FruitSelect.SetActive(false); // Close All button in Fruit Select
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