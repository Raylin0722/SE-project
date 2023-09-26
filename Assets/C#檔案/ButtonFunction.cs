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
    [SerializeField] TextMeshProUGUI Energy;
    float minute;
    bool GameIsStart;
    int ShowMinute;
    int sec;
    float pastTime;
    int currentEnergy;

    void Start()
    {
        WhiteBack.SetActive(false);
        BlackBackground.SetActive(true);
        StartButton.SetActive(true);
        Tool.SetActive(false);
        Time.timeScale=0f;
        minute=60f;
        GameIsStart=false;
        ShowMinute=2;
        sec=0;
        pastTime=0f;
        currentEnergy=100;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameIsStart)
        {
            countDown();
            energy();

        }
    }

    public void pause()
    {
        Time.timeScale=0f;
        WhiteBack.SetActive(true);
        Continue.SetActive(true);
        Replay.SetActive(true);
        Exit.SetActive(true);
        GameIsStart=false;
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
        GameIsStart=true;
    }
    public void replay()
    {
        Time.timeScale=1f;
        WhiteBack.SetActive(false);
        Continue.SetActive(false);
        Replay.SetActive(false);
        Exit.SetActive(false);
        Time.timeScale=1f;
        SceneManager.LoadScene("Background");
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
        }
    }
    void energy()
    {
        pastTime+=Time.deltaTime;
        if(pastTime>=1f)
        {
            pastTime=0f;
            currentEnergy+=15;
            if(currentEnergy<=200)
            {
                Energy.text="#"+currentEnergy.ToString()+"/200";
            }
            else
            {
                Energy.text="#200/200";
            }
        }
    }
}
