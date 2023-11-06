using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class USER : MonoBehaviour
{
    public GameObject Change; // Change portrait Button
    public GameObject[] portraits; // The all portraits in USER
    private int Current = 0; // The current position about your location in USER 

    // Start is called before the first frame update
    void Start()
    {
        // Show current page && Hide the others
        for (int i = 0; i < portraits.Length; i++)
        {
            if (i == Current)
            {
                portraits[i].SetActive(true);
            }
            else
            {
                portraits[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When click < Change >
    public void Button_Change()
    {
        Current = Current + 1;
        if(Current >= portraits.Length)
        {
            Current = 0;
        }
        // Show current page && Hide the others
        for (int i = 0; i < portraits.Length; i++)
        {
            if (i == Current)
            {
                portraits[i].SetActive(true);
            }
            else
            {
                portraits[i].SetActive(false);
            }
        }
    }
}
