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
    private int Faction; 

    // Start is called before the first frame update
    void Start()
    {
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        if(Faction==0)  return;
        for(int i = 0; i<portraits.Length; i++)
        {
            portraits[i].SetActive(i==ServerScript.faction-1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Update_Display(ServerScript.faction);
        
    }

    // When click < Change >
    public void Button_Change()
    {
        ServerScript.faction = (ServerScript.faction+1)%5;
        if(ServerScript.faction==0)
        {
            ServerScript.faction = 1;
        }
    }

    // Display your portraits
    private void Update_Display(int Faction)
    {
        if(Faction==0)  return;
        // Show current page && Hide the others
        for(int i = 0; i<portraits.Length; i++)
        {
            portraits[i].SetActive(i==Faction-1);
        }
    }
}
