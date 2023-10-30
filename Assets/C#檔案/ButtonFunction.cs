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
    [SerializeField] GameObject[] frames;
    [SerializeField] GameObject Wicon1;
    [SerializeField] GameObject Wicon2;
    [SerializeField] GameObject Wicon3;
    [SerializeField] GameObject Wicon4;
    [SerializeField] GameObject Wicon5;
    [SerializeField] GameObject toolFrame;
    [SerializeField] GameObject energyIcon;

    float minute;
    static public bool GameIsStart;
    int ShowMinute;
    int sec;
    float pastTime;
    static public int currentEnergy;
    int energyLimit;
    int initialEnergy;
    float threeSec;
    int InsideGameUpgrade;
    int recovery;

    void Start()
    {
        WhiteBack.SetActive(false);
        BlackBackground.SetActive(true);
        StartButton.SetActive(true);
        Tool.SetActive(false);
        Upgrade.SetActive(false);
        Wicon1.SetActive(false);
        Wicon2.SetActive(false);
        Wicon3.SetActive(false);
        Wicon4.SetActive(false);
        Wicon5.SetActive(false);
        toolFrame.SetActive(false);
        energyIcon.SetActive(false);
        for(int i=0;i<5;i++)
        {
            frames[i].SetActive(false);
        }
        Time.timeScale=0f;
        minute=60f;
        GameIsStart=false;
        ShowMinute=2;
        sec=0;
        pastTime=0f;
        currentEnergy=100;
        energyLimit=140+(GameManage.level)*60;
        initialEnergy=energyLimit/2;
        threeSec=0f;
        InsideGameUpgrade=0;
        recovery=3*GameManage.level;

    }

    // Update is called once per frame
    void Update()
    {
        if(GameIsStart)
        {
            countDown();
            energy();
        }
        if(GameManage.toolIsActive)
        {
            threeSec+=Time.deltaTime;
            if(threeSec>=3)
            {
                GameManage.toolIsActive=false;
                threeSec=0f;
                Watermelon1.speed*=2;
                Watermelon2.speed*=2;
                Watermelon3.speed*=2;
                Watermelon4.speed*=2;
                Watermelon5.speed*=2;

            }
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
        Wicon1.SetActive(true);
        Wicon2.SetActive(true);
        Wicon3.SetActive(true);
        Wicon4.SetActive(true);
        Wicon5.SetActive(true);
        toolFrame.SetActive(true);
        energyIcon.SetActive(true);
        for(int i=0;i<5;i++)
        {
            frames[i].SetActive(true);
        }
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
            currentEnergy=currentEnergy+12+GameManage.level*3+5/7*InsideGameUpgrade;
            if(currentEnergy<=energyLimit)
            {
                Energy.text=currentEnergy.ToString()+"/"+energyLimit.ToString();
            }
            else
            {
                currentEnergy=energyLimit;
                Energy.text=energyLimit.ToString()+"/"+energyLimit.ToString();
            }
        }
    }
    
    public void tool()
    {
        if(GameManage.toolIsUseable)
        {
            GameManage.toolIsUseable=false;
            GameManage.toolIsActive=true;
            Watermelon1.speed*=0.5f;
            Watermelon2.speed*=0.5f;
            Watermelon3.speed*=0.5f;
            Watermelon4.speed*=0.5f;
            Watermelon5.speed*=0.5f;
        }
        
    }

    public void upgrade()
    {
        if(currentEnergy>=(110+5*InsideGameUpgrade) && InsideGameUpgrade<7)
        {
            currentEnergy=currentEnergy-(110+5*InsideGameUpgrade);
            
            if(InsideGameUpgrade==6)
            {
                energyLimit=360;
            }
            else
            {
                energyLimit+=(160/7);
            }
            InsideGameUpgrade++;
            Debug.Log(InsideGameUpgrade);
            
        }
        

    }
    
}
