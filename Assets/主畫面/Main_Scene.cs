using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Small_ranking_list_Method;
using System;

public class ButtonManager : MonoBehaviour
{
    public GameObject main_scene; // The all gameobject
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scene
    public GameObject User; // Player information in the upper left corner
    public GameObject Ranking_list; // Ranking list
    public GameObject page_Ranking_list; // the page for Ranking list
    public GameObject Shop; // Shop
    public GameObject page_Shop; // the page for Shop
    public GameObject Book; // Book
    public GameObject page_Book; // the page for Book
    public GameObject Level_up; // Prerpare for the war
    public GameObject page_Level_up; // the page for Level up
    public GameObject Starts; // Select level to PVE
    public GameObject page_Start; // the page for PVE
    public GameObject Setting; // Settings
    public GameObject page_Setting; // the page for Settings
    public GameObject Friends; // Friends
    public GameObject page_Friends; // the page for Friends
    public GameObject Top_up; // Top up
    public GameObject page_Top_up; // the page for Top up
    public Text energy; // energy value
    public Text money; // money value
    public Text tear; // tear value
    public Text username;
    public Text level;
    public Text timetoGetEnergy;
    public AudioSource Music_Main_Scene; // the Music in Main Scene
    private ServerMethod.Server ServerScript; // Server.cs

    public GameObject ranking_list; 
    public Image[] Rank; 


    private void Start()
    {
        ALL_Button.SetActive(true);
        page_Ranking_list.SetActive(false);
        page_Shop.SetActive(false);
        page_Book.SetActive(false);
        page_Level_up.SetActive(false);
        page_Setting.SetActive(false);
        page_Friends.SetActive(false);
        page_Top_up.SetActive(false);
        page_Start.SetActive(false);
        Play_Music();
        ServerScript = FindObjectOfType<ServerMethod.Server>();
    }

    // Update is called once per frame
    void Update()
    {
        Update_values(); // Update energy && money && tear
        Update_Ranking_List(); // Update Ranking_List in Main_Scene
    }


    // Click < Ranking_list > 
    public void Button_Ranking_list()
    {
        page_Ranking_list.SetActive(true);
        //ALL_Button.SetActive(false); // Close All button in Main_Scene
    }

    private void Update_Ranking_List()
    {
        int index = -1;
        for(int i = 0; i<ServerScript.rankName.Count; i++)
        {
            if(ServerScript.rankName[i]==ServerScript.username)
            {
                index = i;
                break;
            }
        }
        if(index==0)
        {
            Rank[2].rectTransform.anchoredPosition = new Vector2(0.5f,179f);
            Rank[0].rectTransform.anchoredPosition = new Vector2(0.5f,9.65f);
            Rank[2].GetComponent<Small_ranking_list>().Set_tag(index);
            Rank[1].GetComponent<Small_ranking_list>().Set_tag(index+1);
            Rank[0].GetComponent<Small_ranking_list>().Set_tag(index+2);
            Rank[3].GetComponent<Small_ranking_list>().Set_tag(index+3);
            Rank[4].GetComponent<Small_ranking_list>().Set_tag(index+4);
        }
        else if(index==1)
        {
            Rank[2].rectTransform.anchoredPosition = new Vector2(0.5f,94f);
            Rank[1].rectTransform.anchoredPosition = new Vector2(0.5f,9.65f);
            Rank[0].GetComponent<Small_ranking_list>().Set_tag(index-1);
            Rank[2].GetComponent<Small_ranking_list>().Set_tag(index);
            Rank[1].GetComponent<Small_ranking_list>().Set_tag(index+1);
            Rank[3].GetComponent<Small_ranking_list>().Set_tag(index+2);
            Rank[4].GetComponent<Small_ranking_list>().Set_tag(index+3);
        }
        else
        {
            Rank[0].GetComponent<Small_ranking_list>().Set_tag(index-2);
            Rank[1].GetComponent<Small_ranking_list>().Set_tag(index-1);
            Rank[2].GetComponent<Small_ranking_list>().Set_tag(index);
            Rank[3].GetComponent<Small_ranking_list>().Set_tag(index+1);
            Rank[4].GetComponent<Small_ranking_list>().Set_tag(index+2);
        }
    }

    // Click < Shop > 
    public void Button_Shop()
    {
        page_Shop.SetActive(true);
        ALL_Button.SetActive(false); // Close All button in Main_Scene
    }

    // Click < Book > 
    public void Button_Book()
    {
        page_Book.SetActive(true);
        ALL_Button.SetActive(false); // Close All button in Main_Scene
    }

    // Click < Level_up > 
    public void Button_Level_up()
    {
        page_Level_up.SetActive(true);
        ALL_Button.SetActive(false); // Close All button in Main_Scene
    }

    // Click < Start > 
    public void Button_Start()
    {
        page_Start.SetActive(true);
        ALL_Button.SetActive(false); // Close All button in Main_Scene
    }

    // Click < Settings > 
    public void Button_Setting()
    {
        page_Setting.SetActive(true);
    }

    // Click < Friends > 
    public void Button_Friends()
    {
        page_Friends.SetActive(true);
    }

    // Click < Top up > 
    public void Button_Top_up()
    {
        page_Top_up.SetActive(true); // You can active it when you want to do
        //page_Top_up.SetActive(!page_Top_up.activeSelf); // You can delete it when you want to do
    }

    // Update energy && money && tear
    public void Update_values()
    {
        DateTime now = DateTime.Now;
        TimeSpan timediff = now - DateTime.Parse(ServerScript.updateTime);
        Debug.Log(ServerScript.updateTime);
        int during = ServerScript.remainTime + (int)timediff.TotalSeconds, min = 0, sec = 0, tempTime = 0;
        if(during > 1200){
            ServerScript.CallUpdateUserData();
            now = DateTime.Now;
            timediff = now - DateTime.Parse(ServerScript.updateTime);
            during = ServerScript.remainTime + (int)timediff.TotalSeconds;
        }
        
        tempTime = 1200 - (during);
        min = tempTime / 60;
        sec = tempTime % 60;
        
        
        if(ServerScript.energy==30){
            min = 0;
            sec = 0;
        }
        timetoGetEnergy.text = "倒數計時:" + min.ToString().PadLeft(2, '0') + ":" + sec.ToString().PadLeft(2, '0');
        energy.text = ServerScript.energy.ToString() + "/30";
        money.text = ServerScript.money.ToString();
        tear.text = ServerScript.tear.ToString();
        username.text = ServerScript.username.ToString();
        level.text = "Lv"+ServerScript.exp[0].ToString();
        
    }

    // Play Music
    private void Play_Music()
    {
        Music_Main_Scene.Play();
    }

    // Stop Music
    private void Stop_Music()
    {
        Music_Main_Scene.Stop();
    }
}