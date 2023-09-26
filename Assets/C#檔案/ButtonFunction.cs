using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonFunction : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject WhiteBack;
    [SerializeField] GameObject Continue; 
    [SerializeField] GameObject Replay; 
    [SerializeField] GameObject Exit; 
    [SerializeField] TextMeshProUGUI StopWatch;
    [SerializeField] GameObject BlackBackground;
    [SerializeField] GameObject Tool;
    [SerializeField] GameObject Upgrade;
    [SerializeField] GameObject StartButton;
    float minute;
    bool GameIsStart;
    int ShowMinute;
    int sec;

    void Start()
    {
        WhiteBack.SetActive(false);
        BlackBackground.SetActive(true);
        StartButton.SetActive(true);
        Time.timeScale=0f;
        minute=60f;
        GameIsStart=false;
        ShowMinute=3;
        sec=0;

    }

    // Update is called once per frame
    void Update()
    {
        if(GameIsStart)
        {
            countDown();
        }
    }

    public void pause()
    {
        Time.timeScale=0f;
        WhiteBack.SetActive(true);
        Continue.SetActive(true);
        Replay.SetActive(true);
        Exit.SetActive(true);
    }
    public void setting()
    {
        Time.timeScale=0f;
    }

    public void ContinueButton()
    {
        Time.timeScale=1f;
        WhiteBack.SetActive(false);
        Continue.SetActive(false);
        Replay.SetActive(false);
        Exit.SetActive(false);
    }
    public void replay()
    {
        Time.timeScale=1f;
        WhiteBack.SetActive(false);
        Continue.SetActive(false);
        Replay.SetActive(false);
        Exit.SetActive(false);
    }
    public void exit()
    {
        Time.timeScale=1f;
        WhiteBack.SetActive(false);
        Continue.SetActive(false);
        Replay.SetActive(false);
        Exit.SetActive(false);
    }
    public void StartGame()
    {
        BlackBackground.SetActive(false);
        Tool.SetActive(true);
        Upgrade.SetActive(true);
        StartButton.SetActive(false);
        Time.timeScale=1f;
        GameIsStart=true;
        //minute-=Time.deltaTime;
        /**/
        //Debug.Log(minute);

    }
    
    void countDown()
    {
        
        minute-=Time.deltaTime;
        sec=(int)minute;
        if(minute<=0f)
        {
            minute=60;
            ShowMinute--;
            if(ShowMinute==(-1))
            {
                Time.timeScale=0f;
                GameIsStart=false;
                BlackBackground.SetActive(true);
                Tool.SetActive(false);
                Upgrade.SetActive(false);
            }
        }

        if(GameIsStart)
        {
            StopWatch.text=ShowMinute.ToString()+":"+((int)minute).ToString();
            //Debug.Log(minute);
        }
        
        
        
    }
}
