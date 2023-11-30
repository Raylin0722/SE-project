using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class USER : MonoBehaviour
{
    public GameObject Change; // Change portrait Button
    public GameObject[] portraits; // The all portraits in USER

    // Server.cs
    private ServerMethod.Server ServerScript;
    [SerializeField] int[] Faction = {0,2,1,0,1,0};

    // Start is called before the first frame update
    void Start()
    {
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        if(Faction[0]==1)    return;

        /*for(int i = 0; i<portraits.Length; i++)
        {
            portraits[i].SetActive(i==ServerScript.faction-1);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        Update_Display();
    }

    // When click < Change >
    public void Button_Change()
    {
        for(int i = Faction[1] + 1; i<Faction.Length; i++)
        {
            if(Faction[i]==1)   
            {
                Faction[1] = i;
                Update_Display();
                return;
            }
        }
        for(int i = 2; i<=Faction[1]; i++)
        {
            if(Faction[i]==1)   
            {
                Faction[1] = i;
                Update_Display();
                return;
            }
        }
    }

    // Display your portraits
    private void Update_Display()
    {
        if(Faction[0]==1)    return;

        // Show current page && Hide the others
        for(int i = 0; i<portraits.Length; i++)
        {
            portraits[i].SetActive(i==Faction[1]-2);
        }
    }
}
