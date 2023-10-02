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
    [SerializeField] GameObject W1;
    [SerializeField] GameObject W2;
    [SerializeField] GameObject W3;
    [SerializeField] GameObject W4;
    [SerializeField] GameObject W5;
    [SerializeField] GameObject[] WatermelonCharacters;
    [SerializeField] float[] speed;
    [SerializeField] int level;
    


    float minute;
    bool GameIsStart;
    int ShowMinute;
    int sec;
    float pastTime;
    int currentEnergy;
    //float timer;
    bool[] characterIsMoving = new bool[5]; 

    void Start()
    {
        WhiteBack.SetActive(false);
        BlackBackground.SetActive(true);
        StartButton.SetActive(true);
        Tool.SetActive(false);
        Upgrade.SetActive(false);
        W1.SetActive(false);
        W2.SetActive(false);
        W3.SetActive(false);
        W4.SetActive(false);
        W5.SetActive(false);
        Time.timeScale=0f;
        minute=60f;
        GameIsStart=false;
        ShowMinute=2;
        sec=0;
        pastTime=0f;
        currentEnergy=100;
        level=1;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameIsStart)
        {
            countDown();
            energy();
            microComputer();
            if(characterIsMoving[0]==true)
            {
                W1.transform.Translate(speed[0]*Time.deltaTime, 0, 0);
                characterStop(0);
            }
                
            if(characterIsMoving[1]==true)
            {
                W2.transform.Translate(speed[1]*Time.deltaTime, 0, 0);
                characterStop(1);

            }
                
            if(characterIsMoving[2]==true)
            {
                W3.transform.Translate(speed[2]*Time.deltaTime, 0, 0);
                characterStop(2);

            }
                
            if(characterIsMoving[3]==true)
            {
                W4.transform.Translate(speed[3]*Time.deltaTime, 0, 0);
                characterStop(3);
            }
                
            if(characterIsMoving[4]==true)
            {
                W5.transform.Translate(speed[4]*Time.deltaTime, 0, 0);
                characterStop(4);
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
                Energy.text=currentEnergy.ToString()+"/200";
            }
            else
            {
                Energy.text="#200/200";
            }
        }
    }

    int count=0;
    float ttemp=0;
    public Vector3 initialPosition = new Vector3(7.09f, -0.87f, 0f); 

    void microComputer()
    {
        ttemp+=Time.deltaTime;
        
        if(ttemp>=5f)
        {
            ttemp=0f;
            count++;
            Debug.Log("count:"+count);

            if(count>=1)
            {
                Debug.Log("count:"+count);
                int temp=count%5;
                WatermelonCharacters[temp].transform.position=initialPosition;
                WatermelonCharacters[temp].SetActive(true);
                characterIsMoving[temp]=true;
                Debug.Log("character is moving "+characterIsMoving[0]+characterIsMoving[1]+characterIsMoving[2]+characterIsMoving[3]+characterIsMoving[4]);
            }
        }
    }

    void characterStop(int a)
    {
        if (WatermelonCharacters[a].transform.position.x<-7.08)
        {
            WatermelonCharacters[a].SetActive(false);
            characterIsMoving[a]=false;
        }
    }
    
    

}
