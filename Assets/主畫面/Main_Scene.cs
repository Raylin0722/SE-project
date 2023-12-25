using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Small_ranking_list_Method;
using System;
using ServerMethod;
using Unity.VisualScripting;
public class ButtonManager : MonoBehaviour{
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scene
    public GameObject page_Ranking_list; // the page for Ranking list
    public GameObject page_Shop; // the page for Shop
    public GameObject page_Book; // the page for Book
    public GameObject page_Level_up; // the page for Level up
    public GameObject page_Start; // the page for PVE
    public GameObject page_Setting; // the page for Settings
    public GameObject page_Log_out; // the page for Friends
    public GameObject Top_up; // Top up
    public GameObject page_Top_up; // the page for Top up
    public Text energy; // energy value
    public Text money; // money value
    public Text tear; // tear value
    public Text username;
    public Text level;
    public TextMeshProUGUI timetoGetEnergy;
    public GameObject Music_Main_Scene; // the Music in Main Scene
    private ServerMethod.Server ServerScript; // Server.cs
    public Image[] Rank; 
    public bool bool_level_up = false;
    public bool bool_setting = false;
    private void Start() {
        ALL_Button.SetActive(true);
        page_Ranking_list.SetActive(false);
        page_Shop.SetActive(false);
        page_Book.SetActive(false);
        page_Level_up.SetActive(false);
        page_Setting.SetActive(false);
        page_Log_out.SetActive(false);
        Top_up.SetActive(false);
        page_Top_up.SetActive(false);
        page_Start.SetActive(false);
        if(MainMenu.message!=87) {
            Top_up.SetActive(true);
            ServerScript = FindObjectOfType<ServerMethod.Server>();
        }
        StartCoroutine(Play_Music());
    }
    void Update() {
        if(MainMenu.message!=87)    if(ServerScript.rankClear.Count!=0)    Update_Ranking_List(); // Update Ranking_List in Main_Scene
        Update_values(); // Update energy && money && tear
        if(bool_level_up==false && page_Level_up.activeSelf==true)      bool_level_up = true;
        if(bool_level_up==true && page_Level_up.activeSelf==false) {
            if(MainMenu.message==87)        bool_level_up = false;
            else{
                bool_level_up = false;
                StartCoroutine(Lineup_to_Surver());
            }
        }
        if(bool_setting==false && page_Setting.activeSelf==true)      bool_setting = true;
        if(bool_setting==true && page_Setting.activeSelf==false) {
            if(MainMenu.message==87)        bool_setting = false;
            else{
                bool_setting = false;
                StartCoroutine(Setting_to_Server());
            }
        }
    }
    private IEnumerator Lineup_to_Surver() {
        IEnumerator coroutine = ServerScript.updateLineup(ServerScript.lineup);
        yield return StartCoroutine(coroutine);
        Return result = coroutine.Current as Return;
        Debug.Log(result.success);
        bool_level_up = false;
    }
    public IEnumerator Setting_to_Server() {
        IEnumerator coroutine = ServerScript.setting(ServerScript.volume,ServerScript.backVolume,ServerScript.shock);
        yield return StartCoroutine(coroutine);
        Return result = coroutine.Current as Return;
        bool_setting = false;
    }
    // Click < Ranking_list > 
    public void Button_Ranking_list() {
        if(MainMenu.message!=87)    page_Ranking_list.SetActive(true);
    }
    private void Update_Ranking_List() {
        int index = -1;
        for(int i = 0; i<ServerScript.rankName.Count; i++) {
            if(ServerScript.rankName[i]==ServerScript.username) {
                index = i;
                break;
            }
        }
        if(index==0) {
            Rank[2].rectTransform.anchoredPosition = new Vector2(0.5f,179f);
            Rank[0].rectTransform.anchoredPosition = new Vector2(0.5f,9.65f);
            Rank[2].GetComponent<Small_ranking_list>().Set_tag(index);
            Rank[1].GetComponent<Small_ranking_list>().Set_tag(index+1);
            Rank[0].GetComponent<Small_ranking_list>().Set_tag(index+2);
            Rank[3].GetComponent<Small_ranking_list>().Set_tag(index+3);
            Rank[4].GetComponent<Small_ranking_list>().Set_tag(index+4);
        }
        else if(index==1) {
            Rank[2].rectTransform.anchoredPosition = new Vector2(0.5f,94f);
            Rank[1].rectTransform.anchoredPosition = new Vector2(0.5f,9.65f);
            Rank[0].GetComponent<Small_ranking_list>().Set_tag(index-1);
            Rank[2].GetComponent<Small_ranking_list>().Set_tag(index);
            Rank[1].GetComponent<Small_ranking_list>().Set_tag(index+1);
            Rank[3].GetComponent<Small_ranking_list>().Set_tag(index+2);
            Rank[4].GetComponent<Small_ranking_list>().Set_tag(index+3);
        }
        else {
            Rank[0].GetComponent<Small_ranking_list>().Set_tag(index-2);
            Rank[1].GetComponent<Small_ranking_list>().Set_tag(index-1);
            Rank[2].GetComponent<Small_ranking_list>().Set_tag(index);
            Rank[3].GetComponent<Small_ranking_list>().Set_tag(index+1);
            Rank[4].GetComponent<Small_ranking_list>().Set_tag(index+2);
        }
    }
    // Click < Shop > 
    public void Button_Shop() {
        if(MainMenu.message!=87) {
            page_Shop.SetActive(true);
            ALL_Button.SetActive(false); // Close All button in Main_Scene
        }
    }
    // Update energy && money && tear
    public void Update_values() {
        if(MainMenu.message==87) {
            timetoGetEnergy.text = "00:00";
            energy.text = MainMenu.energy.ToString() + "/30";
            money.text = MainMenu.money.ToString();
            tear.text = MainMenu.tear.ToString();
            username.text = MainMenu.username.ToString();
            level.text = "Lv"+MainMenu.exp[0].ToString();
            return;
        }
        DateTime now = DateTime.Now;
        TimeSpan timediff = now - DateTime.Parse(ServerScript.updateTime);
        //Debug.Log(ServerScript.updateTime);
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
        timetoGetEnergy.text = min.ToString().PadLeft(2, '0') + ":" + sec.ToString().PadLeft(2, '0');
        energy.text = ServerScript.energy.ToString() + "/30";
        money.text = ServerScript.money.ToString();
        tear.text = ServerScript.tear.ToString();
        username.text = ServerScript.username.ToString();
        level.text = "Lv"+ServerScript.exp[0].ToString();
    }
    // Play Music
    private IEnumerator Play_Music() {
        yield return new WaitForSeconds(1.0f);
        Music_Main_Scene.SetActive(true);
        AudioListener.volume = 0f;
        if(MainMenu.message==87)    {
            AudioListener.volume =  Mathf.Clamp01(MainMenu.backVolume/100f);
            MainMenu.backVolume = (int)AudioListener.volume;
        }
        else    AudioListener.volume = Mathf.Clamp01((float)ServerScript.backVolume/100f);
        ServerScript.tear = (int)ServerScript.volume;
        
        ServerScript.money = (int)(AudioListener.volume*1000);
    }
}