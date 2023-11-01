using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ranking_list : MonoBehaviour
{
    public GameObject Close; // Close Button
    public GameObject page_Ranking_list; // the page which you want to close

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // When click < X > 
    public void Button_Close()
    {
        page_Ranking_list.SetActive(false);
    }

}
