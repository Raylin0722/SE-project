using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Ranking_player_information_Method;

public class Ranking_list : MonoBehaviour
{
    public GameObject Close; // Close Button
    public GameObject page_Ranking_list; // the page which you want to close
    private ServerMethod.Server ServerScript; // Server.cs
    public GameObject Last_Prefab; // 玩家條目的Prefab
    public Transform Ranking_List; // 容納條目的容器
    public bool bool_update = true; // whether is the ranking list update

    // Start is called before the first frame update
    void Start()
    {
        ServerScript = FindObjectOfType<ServerMethod.Server>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bool_update==true && ServerScript.rankFaction.Count!=0)
        {
            Update_Ranking_List();
            bool_update = false;
        }
    }

    // Update the Rankimg_List
    void Update_Ranking_List()
    {
        for(int i = 4 ; i<50 ; i++)
        {
            if(i<4) continue;   
            GameObject prefab = Instantiate(Last_Prefab, Ranking_List);
            prefab.GetComponent<Ranking_player_information>().Set_tag(i);
        }
    }

    // When click < X > 
    public void Button_Close()
    {
        bool_update = true;
        page_Ranking_list.SetActive(false);
    }
}
