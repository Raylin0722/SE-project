using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MainMenu : MonoBehaviour {
    public Button registerButton;
    public Button loginButton;
    public TMP_Text playerDisplay;
    private void Start() {
        if(DBManager.LoggedIn) {
            playerDisplay.text = "Player: " + DBManager.username; 
        }
        registerButton.interactable = !DBManager.LoggedIn;
        loginButton.interactable = !DBManager.LoggedIn;
        ALL_DEFINE();
    }
    public void GoToRegister() {
        message = 100;
        SceneManager.LoadScene("RegisterMenu");
    }

    public void GoToLogin() {
        message = 100;
        SceneManager.LoadScene("LoginMenu");
    }

    public int Click_times;
    private float time = 5f;
    public static int message = 100;
    private void Update()
    {
        if(Click_times>0)
        {
            //time -= Time.deltaTime;
        }
        if(time<=0f)
        {
            Click_times = 0;
            time = 5f;
        }
    }
    public void GoToSampleSCene() {
        Click_times = Click_times + 1;
        Debug.Log(Click_times);
        if(Click_times>=7)      
        {
            message = 87;
            SceneManager.LoadScene("SampleScene");
        }
    }

    public static string username;
    public static int money;
    public static int[] exp;
    public static int[] character;
    public static int[] lineup;
    public static int tear;
    public static int castleLevel;
    public static int slingshotLevel;
    public static int[] clearance;
    public static int energy;
    public static int remainTime;
    public static string updateTime;
    public static int volume;
    public static int backVolume;
    public static bool shock;
    public static bool remind;
    public static string chestTime;
    public static int[] faction;
    public static int[] props;
    private void ALL_DEFINE()
    {
        username = "87";
        money = 9999;
        exp = new int[2];
        exp[0] = 15;
        exp[1] = (int)(500 * Math.Pow(2.5,exp[0]-1));
        character = new int[7];
        for(int i = 0; i<7; i++)    character[i] = 2;
        lineup = new int[6];
        for(int i = 0; i<5; i++)    lineup[i] = i+1;
        lineup[5] = 1;
        tear = 999;
        castleLevel = 2;
        slingshotLevel = 2;
        clearance = new int[12];
        for(int i = 0; i<12; i++)    clearance[i] = 1;
        energy = 30;
        remainTime = 0;
        volume = 100;
        backVolume = 100;
        shock = false;
        remind = false;
        chestTime = "2023-12-25 10:10:10";
        faction = new int[6];
        faction[1] = 2;
        for(int i = 2; i<6; i++)    faction[i] = 1;
        props = new int[2];
        props[0] = -1;
        props[1] = 999;
    }
}
