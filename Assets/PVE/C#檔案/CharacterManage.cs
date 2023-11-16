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
    public GameObject castle1;
    float passtime;
    static public int record;
    static public int CharacterIdInCd=0;
    int GoOnShoot = 0;
    void Start()
    {
        passtime=0f;
        record=0;
    }

    // Update is called once per frame
    bool w1isUseable=true;
    bool w2isUseable=true;
    bool w3isUseable=true;
    bool w4isUseable=true;
    bool w5isUseable=true;
    float w1CoolTime=0f;
    float w2CoolTime=0f;
    float w3CoolTime=0f;
    float w4CoolTime=0f;
    float w5CoolTime=0f;
    float temp1=0f;
    float temp2=0f;
    float temp3=0f;
    float temp4=0f;
    float temp5=0f;

    int w1i=0;
    int w2i=0;
    int w3i=0;
    int w4i=0;
    int w5i=0;

    void Update()
    {
        //判斷是否進入CD
        switch(CharacterIdInCd)
        {
            case 0:
                break;
            case 1:
                watermelon1Product();
                CharacterIdInCd=0;
                break;
            case 2:
                watermelon2Product();
                CharacterIdInCd=0;
                break;
            case 3:
                watermelon3Product();
                CharacterIdInCd=0;
                break;
            case 4:
                
                watermelon4Product();
                CharacterIdInCd=0;
                
                break;
            case 5:
                watermelon5Product();
                CharacterIdInCd=0;
                break;
        }
        //
        if(ButtonFunction.GameIsStart)
        {
            passtime+=Time.deltaTime;
            //Debug.Log("CM:"+GameManage.currentLevel);
            switch(GameManage.currentLevel)
            {
                case 11:
                    Level1_1();
                    break;
                case 12:
                    Level1_2();
                    break;
                case 13:
                    Level1_3();
                    break;
                case 14:
                    Level1_4();
                    break;
                case 15:
                    Level1_5();
                    break;
                case 16:
                    Level1_6();
                    break;
            }
        }
        
        

        if(!w1isUseable)
        {
            w1CoolTime+=Time.deltaTime;
            temp1+=Time.deltaTime;

            if(temp1>=(4.77f*Math.Pow(1.15, GameManage.level-1))*0.25f)
            {
                w1coolbar[w1i].SetActive(false);
                temp1=0f;
                w1i++;
            }
            if(w1CoolTime>=4.77f*Math.Pow(1.15, GameManage.level-1))
            {
                w1CoolTime=0f;
                w1coolbar[3].SetActive(false);
                w1isUseable=true;
            }
            
        }
        else
        {
            w1i = 0;
        }
        if(!w2isUseable)
        {
            w2CoolTime+=Time.deltaTime;
            temp2+=Time.deltaTime;
            if(temp2>=(3f*Math.Pow(1.15, GameManage.level-1))*0.25f)
            {
                w2coolbar[w2i].SetActive(false);
                temp2=0f;
                w2i++;
            }
            if(w2CoolTime>=3f*Math.Pow(1.15, GameManage.level-1))
            {
                w2CoolTime=0f;
                w2coolbar[3].SetActive(false);
                w2isUseable=true;
            }
        }
        else
        {
            w2i = 0;
        }
        if(!w3isUseable)
        {
            w3CoolTime+=Time.deltaTime;
            temp3+=Time.deltaTime;
            if(temp3>=(7f*Math.Pow(1.15, GameManage.level-1))*0.25f)
            {
                w3coolbar[w3i].SetActive(false);
                temp3=0f;
                w3i++;
            }
            if(w3CoolTime>=7f*Math.Pow(1.15, GameManage.level-1))
            {
                w3CoolTime=0f;
                w3coolbar[3].SetActive(false);
                w3isUseable=true;
            }
        }
        else
        {
            w3i = 0;
        }
        if(!w4isUseable)
        {
            w4CoolTime+=Time.deltaTime;
            temp4+=Time.deltaTime;
            if(temp4>=(4.77f*Math.Pow(1.15, GameManage.level-1))*0.25f)
            {
                w4coolbar[w4i].SetActive(false);
                temp4=0f;
                w4i++;
            }
            if(w4CoolTime>=4.77f*Math.Pow(1.15, GameManage.level-1))
            {
                w4CoolTime=0f;
                w4coolbar[3].SetActive(false);
                w4isUseable=true;
            }
        }
        else
        {
            w4i = 0;
        }
        if(!w5isUseable)
        {
            w5CoolTime+=Time.deltaTime;
            temp5+=Time.deltaTime;
            if(temp5>=(5.88f*Math.Pow(1.15, GameManage.level-1))*0.25f)
            {
                w5coolbar[w5i].SetActive(false);
                temp5=0f;
                w5i++;
            }
            if(w5CoolTime>=5.88f*Math.Pow(1.15, GameManage.level-1))
            {
                w5CoolTime=0f;
                w5coolbar[3].SetActive(false);
                w5isUseable=true;
            }
        }
        else
        {
            w5i = 0;
        }
    }

    public void watermelon1Product()
    {
        if(w1isUseable&&GoOnShoot==1&&CharacterIdInCd==1)
        {
            w1isUseable=false;
            w1coolbar[0].SetActive(true);
            w1coolbar[2].SetActive(true);
            w1coolbar[3].SetActive(true);
            w1coolbar[1].SetActive(true);
            CharacterIdInCd=0;
            GoOnShoot=0;
        }
        else if(ButtonFunction.currentEnergy>=150*Math.Pow(1.4, GameManage.level-1) && w1isUseable)
        {
            Slingshot shot = castle1.GetComponent<Slingshot>();
            if(shot.Rock!=null)
            {
                Destroy(shot.Rock);
                shot.slingshotState = SlingshotState.do_nothing;
            }
            shot.SetEnergy(150);
            //ButtonFunction.currentEnergy-=150;
            GameObject Watermelon1=Instantiate(MyWatermelonPrefabs[0], transform);
            //Watermelon1.transform.position=new Vector3(-7.08f, -1f, 0f);
            shot.CharacterIdInCd_shoot=1;
            shot.Rock=Watermelon1;
            if (shot != null)
            {
                shot.slingshotState = SlingshotState.Idle;
            }
            GoOnShoot=1;
        }
        
    }
    public void watermelon2Product()
    {
        if(w2isUseable&&GoOnShoot==1&&CharacterIdInCd==2)
        {

            w2isUseable=false;
            w2coolbar[0].SetActive(true);
            w2coolbar[2].SetActive(true);
            w2coolbar[3].SetActive(true);
            w2coolbar[1].SetActive(true);
            CharacterIdInCd=0;
            GoOnShoot=0;
        }
        else if(ButtonFunction.currentEnergy>=70*Math.Pow(1.4, GameManage.level-1) && w2isUseable)
        {
            Slingshot shot = castle1.GetComponent<Slingshot>();
            if(shot.Rock!=null)
            {
                Destroy(shot.Rock);
                shot.slingshotState = SlingshotState.do_nothing;
            }
            shot.SetEnergy(70);
            //ButtonFunction.currentEnergy-=70;
            GameObject Watermelon2=Instantiate(MyWatermelonPrefabs[1], transform);
            //Watermelon2.transform.position=new Vector3(-7.08f, -1f, 0f);
            shot.CharacterIdInCd_shoot=2;
            shot.Rock=Watermelon2;
            if (shot != null)
            {
                shot.slingshotState = SlingshotState.Idle;
            }
            GoOnShoot=1;
        }
    }
    public void watermelon3Product()
    {
        if(w3isUseable&&GoOnShoot==1&&CharacterIdInCd==3)
        {
            w3isUseable=false;
            w3coolbar[0].SetActive(true);
            w3coolbar[2].SetActive(true);
            w3coolbar[3].SetActive(true);
            w3coolbar[1].SetActive(true);
            CharacterIdInCd=0;
            GoOnShoot=0;
        }
        else if(ButtonFunction.currentEnergy>=250*Math.Pow(1.4, GameManage.level-1) && w3isUseable)
        {
            Slingshot shot = castle1.GetComponent<Slingshot>();
            if(shot.Rock!=null)
            {
                Destroy(shot.Rock);
                shot.slingshotState = SlingshotState.do_nothing;
            }
            
            shot.SetEnergy(250);
            //ButtonFunction.currentEnergy-=250;
            GameObject Watermelon3=Instantiate(MyWatermelonPrefabs[2], transform);
            //Watermelon3.transform.position=new Vector3(-7.08f, -1f, 0f);
            shot.CharacterIdInCd_shoot=3;
            shot.Rock=Watermelon3;
            if (shot != null)
            {
                shot.slingshotState = SlingshotState.Idle;
            }
            GoOnShoot=1;
        }
        
    }
    public void watermelon4Product()
    {

        if( w4isUseable&&GoOnShoot==1&&CharacterIdInCd==4)
        {
        
            w4isUseable=false;
            w4coolbar[0].SetActive(true);
            w4coolbar[2].SetActive(true);
            w4coolbar[3].SetActive(true);
            w4coolbar[1].SetActive(true);
            CharacterIdInCd=0;
            GoOnShoot=0;
        }
        else if(ButtonFunction.currentEnergy>=150*Math.Pow(1.4, GameManage.level-1) && w4isUseable)
        {
            Slingshot shot = castle1.GetComponent<Slingshot>();
            if(shot.Rock!=null)
            {
                Destroy(shot.Rock);
                shot.slingshotState = SlingshotState.do_nothing;
            }
 
            shot.SetEnergy(150);
            //ButtonFunction.currentEnergy-=150;
            GameObject Watermelon4=Instantiate(MyWatermelonPrefabs[3], transform);
            //Watermelon4.transform.position=new Vector3(-7.08f, -1f, 0f);
            shot.CharacterIdInCd_shoot=4;
            shot.Rock=Watermelon4;
            if (shot != null)
            {
                shot.slingshotState = SlingshotState.Idle;
            }
            GoOnShoot=1;
        }
        
    }
    public void watermelon5Product()
    {
        if( w5isUseable&&GoOnShoot==1&&CharacterIdInCd==5)
        {
            w5isUseable=false;
            w5coolbar[0].SetActive(true);
            w5coolbar[2].SetActive(true);
            w5coolbar[3].SetActive(true);
            w5coolbar[1].SetActive(true);
            CharacterIdInCd=0;
            GoOnShoot=0;
        }
        else if(ButtonFunction.currentEnergy>=200*Math.Pow(1.4, GameManage.level-1) && w5isUseable)
        {
            Slingshot shot = castle1.GetComponent<Slingshot>();
            if(shot.Rock!=null)
            {
                Destroy(shot.Rock);
                shot.slingshotState = SlingshotState.do_nothing;
            }
            
            shot.SetEnergy(200);
            //ButtonFunction.currentEnergy-=200;
            GameObject Watermelon5=Instantiate(MyWatermelonPrefabs[4], transform);
            //Watermelon5.transform.position=new Vector3(-7.08f, -1f, 0f);
            shot.CharacterIdInCd_shoot=5;
            shot.Rock=Watermelon5;
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

    void Level1_1()
    {
        if(passtime>=time_1_1[record])
        {
            if(Seq_1_1[record]!=(-1))
            {
                GameObject enemies=Instantiate(PepperRPrefabs[Seq_1_1[record]], transform);
                enemies.transform.position=new Vector3(15.0f, 0.0f, 0f);
                enemies.transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            record++;
            if(record==time_1_1.Length-1)
            {
                passtime=0;
                record=0;
            }
        }
    }
    void Level1_2()
    {
        if(passtime>=time_1_2[record])
        {
            if(Seq_1_2[record]!=(-1))
            {
                GameObject enemies=Instantiate(PepperRPrefabs[Seq_1_2[record]], transform);
                enemies.transform.position=new Vector3(15.0f, 0.0f, 0f);
                enemies.transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            record++;
            if(record==time_1_2.Length-1)
            {
                passtime=0;
                record=0;
            }
        }
    }
    void Level1_3()
    {
        if(passtime>=time_1_3[record])
        {
            if(Seq_1_3[record]!=(-1))
            {
                GameObject enemies=Instantiate(PepperRPrefabs[Seq_1_3[record]], transform);
                enemies.transform.position=new Vector3(15.0f, 0.0f, 0f);
                enemies.transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            record++;
            if(record==time_1_3.Length-1)
            {
                passtime=0;
                record=0;
            }
        }
    }
    void Level1_4()
    {
        if(passtime>=time_1_4[record])
        {
            if(Seq_1_4[record]!=(-1))
            {
                GameObject enemies=Instantiate(PepperRPrefabs[Seq_1_4[record]], transform);
                enemies.transform.position=new Vector3(15.0f, 0.0f, 0f);
                enemies.transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            record++;
            if(record==time_1_4.Length-1)
            {
                passtime=0;
                record=0;
            }
        }
    }
    void Level1_5()
    {
        if(passtime>=time_1_5[record])
        {
            if(Seq_1_5[record]!=(-1))
            {
                GameObject enemies=Instantiate(PepperRPrefabs[Seq_1_5[record]], transform);
                enemies.transform.position=new Vector3(15.0f, 0.0f, 0f);
                enemies.transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            record++;
            if(record==time_1_5.Length-1)
            {
                passtime=0;
                record=0;
            }
        }
    }
    void Level1_6()
    {
        if(passtime>=time_1_6[record])
        {
            if(Seq_1_6[record]!=(-1))
            {
                GameObject enemies=Instantiate(PepperRPrefabs[Seq_1_6[record]], transform);
                enemies.transform.position=new Vector3(15.0f, 0.0f, 0f);
                enemies.transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            record++;
            if(record==time_1_6.Length-1)
            {
                passtime=0;
                record=0;
            }
        }
    }
}
