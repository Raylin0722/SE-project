using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts;
public class CharacterManage : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject[] WatermelonPrefabs;
    [SerializeField] GameObject[] PepperOPrefabs;//1-1
    [SerializeField] GameObject[] PepperRPrefabs;//1-2
    [SerializeField] GameObject[] PepperGPrefabs;//1-3
    [SerializeField] GameObject[] PepperYPrefabs;//1-4
    [SerializeField] GameObject[] PepperSPrefabs;//1-5
    [SerializeField] GameObject[] MyWatermelonPrefabs;
    [SerializeField] GameObject[] w1coolbar;
    [SerializeField] GameObject[] w2coolbar;
    [SerializeField] GameObject[] w3coolbar;
    [SerializeField] GameObject[] w4coolbar;
    [SerializeField] GameObject[] w5coolbar;
    private GameObject[][] PepperPrefabs;
    private GameObject[][] wcoolbar;
    public GameObject castle1;
    float passtime;
    static public int record;
    static public int CharacterIdInCd=0;
    int GoOnShoot = 0;
    void Start()
    {
        passtime=0f;
        record=0;
        PepperPrefabs = new GameObject[][] {PepperOPrefabs,PepperRPrefabs,PepperGPrefabs,PepperYPrefabs,PepperSPrefabs};
        wcoolbar = new GameObject[][] {w1coolbar,w2coolbar,w3coolbar,w4coolbar,w5coolbar};
        EnemySeq = new int[][] {Seq_1_1,Seq_1_2,Seq_1_3,Seq_1_4,Seq_1_5,Seq_1_6};
        EnemyTime = new int[][] {time_1_1,time_1_2,time_1_3,time_1_4,time_1_5,time_1_6};
    }

    // Update is called once per frame
    private bool[] wisUseable=new bool[5]{true,true,true,true,true};
    private float[] wCoolTime=new float[5]{0f,0f,0f,0f,0f};
    private float[] temp=new float[5]{0f,0f,0f,0f,0f};
    private int[] wi=new int[5]{0,0,0,0,0};
    private float[] wCoolTimeUnit=new float[5]{4.77f,3f,7f,4.77f,5.88f};
    private int[] wEnergy=new int[5]{150,70,250,150,200};
    void Update()
    {
        //判斷是否進入CD
        if(CharacterIdInCd!=0){
            watermelonProduct(CharacterIdInCd-1);
            CharacterIdInCd=0;
        }
        //
        if(ButtonFunction.GameIsStart)
        {
            passtime+=Time.deltaTime;
            //Debug.Log("CM:"+GameManage.currentLevel);
            if(GameManage.currentLevel!=0)Level(GameManage.currentLevel);
        }
        
        
        for(int i=0;i<5;i++){
            if(!wisUseable[i])
            {
                wCoolTime[i]+=Time.deltaTime;
                temp[i]+=Time.deltaTime;

                if(temp[i]>=(wCoolTimeUnit[i]*Math.Pow(1.15, GameManage.level-1))*0.25f)
                {
                    wcoolbar[i][wi[i]].SetActive(false);
                    temp[i]=0f;
                    wi[i]++;
                }
                if(wCoolTime[i]>=wCoolTimeUnit[i]*Math.Pow(1.15, GameManage.level-1))
                {
                    wCoolTime[i]=0f;
                    wcoolbar[i][3].SetActive(false);
                    wisUseable[i]=true;
                }
            
            }
            else
            {
                wi[i] = 0;
            }
        }
    }

    public void watermelonProduct(int index)
    {
        if(wisUseable[index]&&GoOnShoot==1&&CharacterIdInCd==index+1)
        {
            wisUseable[index]=false;
            for(int i=0;i<4;i++)wcoolbar[index][i].SetActive(true);
            CharacterIdInCd=0;
            GoOnShoot=0;
        }
        else if(ButtonFunction.currentEnergy>=wEnergy[index]*Math.Pow(1.4, GameManage.level-1) && wisUseable[index])
        {
            Slingshot shot = castle1.GetComponent<Slingshot>();
            if(shot.Rock!=null)
            {
                Destroy(shot.Rock);
                shot.slingshotState = SlingshotState.do_nothing;
            }
            shot.SetEnergy(wEnergy[index]);
            //ButtonFunction.currentEnergy-=150;
            GameObject Watermelon=Instantiate(MyWatermelonPrefabs[index], transform);
            //Watermelon1.transform.position=new Vector3(-7.08f, -1f, 0f);
            shot.CharacterIdInCd_shoot=1;
            shot.Rock=Watermelon;
            if (shot != null)
            {
                shot.slingshotState = SlingshotState.Idle;
            }
            GoOnShoot=1;
        }
        
    }
    
    int[] Seq_1_1 = {1, -1, 3, 1, 1, 3, 1, 1, 1, 1, 3, 4, 4, 1, 0, 3, 1};
    int[] time_1_1= {6, 12, 22, 30, 35, 45, 58, 80, 90, 100, 110, 120, 130, 135, 148, 161, 162};
    int[] Seq_1_2 = {0, -1, 3, 4, 0, 3, 1, 1, 4, 0, 3, 4, 1, 1, 0, 3, 0};
    int[] time_1_2= {6, 12, 22, 34, 44 , 54, 58, 80, 90, 100, 110, 130, 140, 141, 151, 161, 171};
    int[] Seq_1_3 = {3, -1, 0, 1, 1, 0, 4, 1, 1, 1, 1, 4, 0, 3, 4, 0, 4, 1};
    int[] time_1_3= {6, 12, 27, 35, 36, 46, 58, 70, 74, 78, 82, 93, 103, 113, 140, 155, 170, 175};
    int[] Seq_1_4 = {4, -1, 3, 0, 1, 4, 1, 0, 1, 1, 4, 3, 1, 1, 3, 0, 4, 4, 1};
    int[] time_1_4= {7, 15, 25, 35, 45, 47, 61, 71, 75, 79, 91, 101, 111, 115, 125, 135, 157, 172, 177};
    int[] Seq_1_5 = {4, -1, 3, 4, 3, 4, 0, 1, 1, 4, 3, 3, 0, -1, 4, 1, 0, 3, 3};
    int[] time_1_5= {6, 12, 22, 34, 40, 54, 58, 68, 78, 90, 100, 110, 123, 130, 140, 144, 151, 161, 172};
    int[] Seq_1_6 = {4, -1, 3, 0, 1, 0, 3, 0, 1, 1, 1, 0, 4, 4, 3, -1, 1, 1, 3, 3, 1};
    int[] time_1_6= {6, 12, 25, 35, 38, 46, 58, 70, 74, 78, 82, 92, 102, 108, 113, 130, 140, 145, 155, 170, 175};

    private int[][] EnemySeq;
    private int[][] EnemyTime;
    void Level(int index)
    {
        index=(index/10-1)*6+index%10-1;
        if(passtime>=EnemyTime[index][record])
        {
            if(EnemySeq[index][record]!=(-1))
            {
                GameObject enemies=Instantiate(PepperPrefabs[index][EnemySeq[index][record]], transform);
                enemies.transform.position=new Vector3(15.0f, 0.0f, 0f);
                enemies.transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            record++;
            if(record==EnemyTime[index].Length-1)
            {
                passtime=0;
                record=0;
            }
        }
    }
}