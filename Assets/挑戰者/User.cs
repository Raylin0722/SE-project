using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using System;
using System.Threading;
using UnityEngine.SceneManagement;
using TMPro;
using ServerMethod;

public class USER : MonoBehaviour
{
    public GameObject Change; // Change portrait Button
    public GameObject[] portraits; // The all portraits in USER

    // Server.cs
    private ServerMethod.Server ServerScript;
    public Image Experience_line;

    // Start is called before the first frame update
    void Start()
    {
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        if(ServerScript.faction[0]==1)    return;
    }

    // Update is called once per frame
    void Update()
    {
        if(ServerScript.faction.Length!=0)   Update_Display();
        int num = 0;
        for(int i = 2; i<6 ; i++)
        {
            if(ServerScript.faction[i]==1)      num = num + 1;
        }
        if(num>1)       Change.gameObject.SetActive(true);
        else            Change.gameObject.SetActive(false);
    }

    // When click < Change >
    public void Button_Change()
    {
        for(int i = ServerScript.faction[1] + 1; i<ServerScript.faction.Length; i++)
        {
            if(ServerScript.faction[i]==1)   
            {
                ServerScript.faction[1] = i;
                StartCoroutine(ServerScript.updateFaction(ServerScript.faction[1]));
                Update_Display();
                return;
            }
        }
        for(int i = 2; i<=ServerScript.faction[1]; i++)
        {
            if(ServerScript.faction[i]==1)   
            {
                ServerScript.faction[1] = i;
                StartCoroutine(ServerScript.updateFaction(ServerScript.faction[1]));
                Update_Display();
                return;
            }
        }
    }

    // Display your portraits
    private void Update_Display()
    {
        if(ServerScript.faction[0]==1)    return;

        // Show current page && Hide the others
        for(int i = 0; i<portraits.Length; i++)
        {
            portraits[i].SetActive(i==ServerScript.faction[1]-2);
        }
        
        Experience_line.fillAmount = (float)(ServerScript.exp[1] / (500 * Math.Pow(2.5,ServerScript.exp[0]-1)));
    }
}