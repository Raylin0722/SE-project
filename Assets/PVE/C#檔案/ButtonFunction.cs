using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Assets.Scripts;
using ServerMethod;
using System;
using System.Linq;

public class ButtonFunction : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Bomb;
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
    public FadeOutEffect fadeOutEffect; 
    [SerializeField] GameObject Victory_1;
    [SerializeField] GameObject Victory_2;
    [SerializeField] GameObject Defeat;
    [SerializeField] GameObject Close;
    [SerializeField] GameObject Next;
    [SerializeField] GameObject Close_bottom;
    [SerializeField] GameObject Next_bottom;
    [SerializeField] GameObject castle1;
    [SerializeField] GameObject castle2;
    //
    static public int judge_victory=0;
    static public int judge_defeat=0;
    // 
    float minute;
    static public bool GameIsStart;
    int ShowMinute;
    int sec;
    float pastTime;
    static public int currentEnergy;
    int energyLimit;
    int initialEnergy;
    int InsideGameUpgrade;
    int recovery;
    public static float windCooldown=0.0f;
    float oneSec;
    float temp;
    float increment;
    float doubleEnergy=1f;
    float UpgradeEnergy;
    private ServerMethod.Server ServerScript;

    // exp 、 dollars 、 tears for end gane
    public Text Exp;
    public Text Dollar;
    public Text Tear;
    private struct Award
    {
        public float exp;
        public float dollar;
        public float tear;
    }
    private List<Award> Award_List = new List<Award>();

    void Start()
    {
        WhiteBack.SetActive(false);
        //BlackBackground.SetActive(true);
        //StartButton.SetActive(true);
        Tool.SetActive(false);
        Upgrade.SetActive(false);
        Wicon1.SetActive(false);
        Wicon2.SetActive(false);
        Wicon3.SetActive(false);
        Wicon4.SetActive(false);
        Wicon5.SetActive(false);
        toolFrame.SetActive(false);
        energyIcon.SetActive(false);
        //
        Victory_1.SetActive(false);
        Victory_2.SetActive(false);
        Defeat.SetActive(false);
        Close.SetActive(false);
        Next.SetActive(false);
        //
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
        oneSec=0f;
        increment=(12+GameManage.level*3+(5/7)*InsideGameUpgrade)*doubleEnergy;
        currentEnergy=100+(int)increment;
        temp=currentEnergy;
        energyLimit=140+(GameManage.level)*60;
        initialEnergy=energyLimit/2;
        InsideGameUpgrade=0;
        recovery=3*GameManage.level;
        UpgradeEnergy=110+5f*InsideGameUpgrade;
        //BlackBackground.SetActive(false);
        Tool.SetActive(true);
        Upgrade.SetActive(true);
        //StartButton.SetActive(false);
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
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        Award_Definition();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameIsStart)
        {
            countDown();
            energy();
            //currentEnergy=200;
        }
        //判斷我方冷風能不能用以及冷風使用時間過3秒要將移動速度調整回來
        //12-09新增炸彈,功能是炸死離主堡最近的,冷卻一律用windCooldown
        if(windCooldown>0.0f){
            if(ServerScript.lineup[5]==1){
                float last=windCooldown;
                windCooldown-=Time.deltaTime;
                if(last>7.0f&&windCooldown<=7.0f){
                    itemWind(true,true);
                    GameManage.toolIsActive=false;
                }
                if(windCooldown<0) windCooldown=0.0f;
            }
            else if(ServerScript.lineup[5]==2){
                windCooldown-=Time.deltaTime;
                GameManage.toolIsActive=false;
                if(windCooldown<0) windCooldown=0.0f;
            }
        }
        else GameManage.toolIsUseable=true;

        if(judge_victory==1)
        {
            Victory_1_End();
        }
        if(judge_defeat==1)
        {
            Defeat_End();
        }
    }
    public void Victory_1_End()
    {
        WhiteBack.SetActive(true);
        Time.timeScale=0f;
        Victory_1.SetActive(true);
        Next.SetActive(true);
        Next_bottom.SetActive(true);
        judge_victory=0;
    }
    public void Victory_2_End()
    {
        WhiteBack.SetActive(true);
        Victory_1.SetActive(false);
        Next.SetActive(false);
        Next_bottom.SetActive(false);
        Victory_2.SetActive(true);
        Close.SetActive(true);
        Close_bottom.SetActive(true);
        Award_Calculate(1);
    }
    public void go_Lobby()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Defeat_End()
    {
        WhiteBack.SetActive(true);
        judge_defeat=0;
        Time.timeScale=0f;
        Defeat.SetActive(true);
        Close.SetActive(true);
        Close_bottom.SetActive(true);
        Award_Calculate(0);
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
        StartCoroutine(ServerScript.beforeGame());
    }
    public void exit()
    {
        Time.timeScale=1f;
        WhiteBack.SetActive(false);
        Continue.SetActive(false);
        Replay.SetActive(false);
        Exit.SetActive(false);
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        ServerScript.CallUpdateUserData();
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
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
                Health health_mine = castle1.GetComponent<Health>();
                Health health_yours = castle2.GetComponent<Health>();
                if(health_mine.currentHealth>health_yours.currentHealth)
                {
                    judge_victory=1;
                }
                else
                {
                    judge_defeat=1;
                }
                Time.timeScale=0f;
                GameIsStart=false;
                BlackBackground.SetActive(true);
                Tool.SetActive(false);
                Upgrade.SetActive(false);
            }
        }
        if(ShowMinute==0)
        {
            doubleEnergy=2f;
        }

        if(GameIsStart)
        {
            StopWatch.text=ShowMinute.ToString("00")+":"+((int)minute).ToString("00");
        }
    }
    void energy()
    {
        pastTime+=Time.deltaTime;
        oneSec+=Time.deltaTime;
        int detect=0;
        if(oneSec>1f)
        {
            oneSec=0f;
            currentEnergy=currentEnergy+(int)increment;
            temp=currentEnergy;
            if(currentEnergy<=energyLimit)
            {
                Energy.text=currentEnergy.ToString()+"/"+energyLimit.ToString();
            }
            else
            {
                currentEnergy=energyLimit;
                Energy.text=energyLimit.ToString()+"/"+energyLimit.ToString();
            }
            detect=1;
        }
        if(pastTime>0.1f)
        {
            pastTime=0f;
            if(detect==0){
                temp=temp+increment/10f;
                //Debug.Log(temp);

                if(temp<=energyLimit)
                {
                    Energy.text=((int)temp).ToString()+"/"+energyLimit.ToString();
                }
                else
                {
                    temp=energyLimit;
                    Energy.text=energyLimit.ToString()+"/"+energyLimit.ToString();
                }
            }
            detect=0;
        }
        
    }
    
    //itemWind(True,True) --> player use and enemy moveSpeed recovery
    public static void itemWind(bool whoUse,bool divOrMul){
        string objectTag = (whoUse) ?"enemy" :"Player" ;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(objectTag);
            foreach (GameObject enemy in enemies) {
                Attack enemyAttack = enemy.GetComponent<Attack>();
                if (enemyAttack != null) {
                    enemyAttack.moveSpeed = (divOrMul) ?enemyAttack.unwindMoveSpeed : enemyAttack.windMoveSpeed;
                }
            }
    }
    //itemBomb(True) --> player use 
    public void itemBomb(bool whoUse){
        string objectTag = (whoUse) ?"enemy" :"Player" ;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(objectTag);
            enemies =(whoUse) ? enemies.OrderBy(enemy => enemy.transform.position.x).ToArray()
            : enemies = enemies.OrderByDescending(enemy => enemy.transform.position.x).ToArray();
            if(enemies != null && enemies.Length > 0 && enemies[0] != null){
                float centerX = enemies[0].transform.position.x;
                float centerY = enemies[0].transform.position.y;
                if (centerX != 15 && centerX != -15) {
                    Bomb.GetComponent<Bomb>().Trigger();
                    Bomb.transform.position = new Vector3(centerX, centerY, 0f);
                    Destroy(enemies[0]);
                    GameManage.toolIsUseable = false;
                    GameManage.toolIsActive = true;
                    windCooldown=10.0f;
                    fadeOutEffect.StartCooldown();
                }
            }
    }
    public void tool()
    {
        if(GameManage.toolIsUseable&&ServerScript.lineup[5]==1)
        {
            GameManage.toolIsUseable=false;
            GameManage.toolIsActive=true;
            itemWind(true,false);
            fadeOutEffect.StartCooldown();

            Wind[] Winds = FindObjectsOfType<Wind>();
            foreach (Wind Wind in Winds)
            {
                if(!Wind.who)
                    Wind.ActivateWind();
            }
            windCooldown=10.0f;
        }
        else if(GameManage.toolIsUseable&&ServerScript.lineup[5]==2)
        {
            itemBomb(true);
        }
        
    }
    
    [SerializeField] TextMeshProUGUI Upgradetext;
    public void upgrade()
    {
        if(currentEnergy>=(UpgradeEnergy) && InsideGameUpgrade<7)
        {
            Slingshot shot = castle1.GetComponent<Slingshot>();
            if(shot.Rock!=null)
            {
                Destroy(shot.Rock);
                shot.slingshotState = SlingshotState.do_nothing;
            }
            currentEnergy=currentEnergy-(int)UpgradeEnergy;
            
            if(InsideGameUpgrade==6)
            {
                energyLimit=360;
            }
            else
            {
                energyLimit+=(160/7);
            }
            InsideGameUpgrade++;
            UpgradeEnergy=110+5*InsideGameUpgrade;
            Debug.Log(InsideGameUpgrade);
            Debug.Log(UpgradeEnergy);
            //Text textComponent = Upgrade.GetComponent<Text>();
            Upgradetext.text=UpgradeEnergy.ToString();
            if(UpgradeEnergy==145)
            {
                Upgradetext.text="Max";
            }
        }
    }

    public void Award_Calculate(int state) // 0 => Lose  ,   1 => Win
    {
        //int[] clearance = [0,0,0,0,0,0,0,0,0,0,0,0];
        int index = 6*(GameManage.currentLevel/10-1)+(GameManage.currentLevel%10-1);
        if(state==1)
        {
            Dollar.text = ((int)((Award_List[index].dollar)
            *Math.Pow(2.0f,-(ServerScript.clearance[index]-1)))).ToString();
            Exp.text = ((int)((Award_List[index].exp)
            *Math.Pow(2.0f, -(ServerScript.clearance[index]-1)))).ToString();
            Tear.text = ((int)(Award_List[index].tear)).ToString();
            if(ServerScript.clearance[index]!=1) Tear.text = "0";
        }
        
    }

    private void Award_Definition()
    {
        Award one_of_one = new Award
        {
            exp = 300.0f,
            dollar = 500.0f,
            tear = 2.0f
        };
        Award_List.Add(one_of_one);
        Award one_of_two = new Award
        {
            exp = 320.0f,
            dollar = 500.0f,
            tear = 2.0f
        };
        Award_List.Add(one_of_two);
        Award one_of_three = new Award
        {
            exp = 340.0f,
            dollar = 500.0f,
            tear = 2.0f
        };
        Award_List.Add(one_of_three);
        Award one_of_four = new Award
        {
            exp = 360.0f,
            dollar = 500.0f,
            tear = 2.0f
        };
        Award_List.Add(one_of_four);
        Award one_of_five = new Award
        {
            exp = 380.0f,
            dollar = 500.0f,
            tear = 2.0f
        };
        Award_List.Add(one_of_five);
        Award one_of_six = new Award
        {
            exp = 400.0f,
            dollar = 600.0f,
            tear = 3.0f
        };
        Award_List.Add(one_of_six);
        Award two_of_one = new Award
        {
            exp = 400.0f,
            dollar = 600.0f,
            tear = 2.0f
        };
        Award_List.Add(two_of_one);
        Award two_of_two = new Award
        {
            exp = 420.0f,
            dollar = 600.0f,
            tear = 2.0f
        };
        Award_List.Add(two_of_two);
        Award two_of_three = new Award
        {
            exp = 440.0f,
            dollar = 600.0f,
            tear = 2.0f
        };
        Award_List.Add(two_of_three);
        Award two_of_four = new Award
        {
            exp = 460.0f,
            dollar = 600.0f,
            tear = 2.0f
        };
        Award_List.Add(two_of_four);
        Award two_of_five = new Award
        {
            exp = 480.0f,
            dollar = 600.0f,
            tear = 2.0f
        };
        Award_List.Add(two_of_five);
        Award two_of_six = new Award
        {
            exp = 500.0f,
            dollar = 700.0f,
            tear = 3.0f
        };
        Award_List.Add(two_of_six);
    }
}


