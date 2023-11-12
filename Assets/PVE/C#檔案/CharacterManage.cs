using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts;
public class CharacterManage : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject[] WatermelonPrefabs;
    [SerializeField] GameObject[] PepperPrefabs;
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
                Debug.Log("我要進去2");
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
        Debug.Log("進來二了");
        if(w2isUseable&&GoOnShoot==1&&CharacterIdInCd==2)
        {
            Debug.Log("近來CD了");
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
            Debug.Log("要射了");
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
        Debug.Log("我進4了阿然後勒");
        if( w4isUseable&&GoOnShoot==1&&CharacterIdInCd==4)
        {
            Debug.Log("進CD");
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
            Debug.Log("射了");
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
    

    void Level1_1()
    {
        if(passtime>=6f&&record==0)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(15.0f, 0.0f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=19&&record==1)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon4.transform.position=new Vector3(13.0f, 0.0f, 0f);
            Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=35&&record==2)
        {
            GameObject Watermelon3=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon3.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon3.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=54&&record==3)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon4.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=64&&record==4)
        {
            GameObject Watermelon1=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon1.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon1.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=66&&record==5)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=72&&record==6)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=85&&record==7)
        {
            GameObject Watermelon3=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon3.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon3.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=105&&record==8)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=122&&record==9)
        {
            GameObject Watermelon1=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon1.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon1.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=132&&record==10)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=148&&record==11)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon4.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=162&&record==12)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
            passtime=0;
            record=0;
        }
        
    }
    void Level1_2()
    {
        if(passtime>=6f&&record==0)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon2.transform.position=new Vector3(15.0f, 0.0f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=12&&record==1)//sling shot
        {
            //GameObject Watermelon4=Instantiate(WatermelonPrefabs[3], transform);
            //Watermelon4.transform.position=new Vector3(13.0f, 0.0f, 0f);
            //Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=22&&record==2)
        {
            GameObject Watermelon3=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon3.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon3.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=34&&record==3)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[4], transform);
            Watermelon4.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=44&&record==4)
        {
            GameObject Watermelon1=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon1.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon1.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=54&&record==5)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=58&&record==6)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=80&&record==7)
        {
            GameObject Watermelon3=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon3.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon3.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=90&&record==8)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=100&&record==9)
        {
            GameObject Watermelon1=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon1.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon1.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=110&&record==10)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=130&&record==11)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[4], transform);
            Watermelon4.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=140&&record==12)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=141&&record==13)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=151&&record==14)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=161&&record==15)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=171&&record==16)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
            passtime=0;
            record=0;
        }
        
    }
    void Level1_3()
    {
        if(passtime>=6f&&record==0)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon2.transform.position=new Vector3(15.0f, 0.0f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=12&&record==1)//sling shot
        {
            //GameObject Watermelon4=Instantiate(WatermelonPrefabs[3], transform);
            //Watermelon4.transform.position=new Vector3(13.0f, 0.0f, 0f);
            //Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=27&&record==2)
        {
            GameObject Watermelon3=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon3.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon3.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=35&&record==3)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon4.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=36&&record==4)
        {
            GameObject Watermelon1=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon1.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon1.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=46&&record==5)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=58&&record==6)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[4], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=70&&record==7)
        {
            GameObject Watermelon3=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon3.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon3.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=74&&record==8)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=78&&record==9)
        {
            GameObject Watermelon1=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon1.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon1.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=88&&record==10)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=93&&record==11)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon4.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=103&&record==12)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=113&&record==13)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=140&&record==14)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[4], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=155&&record==15)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=170&&record==16)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=175&&record==17)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
            passtime=0;
            record=0;
        }
    }
    void Level1_4()
    {
        if(passtime>=7f&&record==0)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[4], transform);
            Watermelon2.transform.position=new Vector3(15.0f, 0.0f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=15&&record==1)//sling shot
        {
            //GameObject Watermelon4=Instantiate(WatermelonPrefabs[3], transform);
            //Watermelon4.transform.position=new Vector3(13.0f, 0.0f, 0f);
            //Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=25&&record==2)
        {
            GameObject Watermelon3=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon3.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon3.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=35&&record==3)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon4.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=45&&record==4)
        {
            GameObject Watermelon1=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon1.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon1.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=47&&record==5)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[4], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=61&&record==6)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=71&&record==7)
        {
            GameObject Watermelon3=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon3.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon3.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=75&&record==8)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=79&&record==9)
        {
            GameObject Watermelon1=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon1.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon1.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=91&&record==10)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[4], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=101&&record==11)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon4.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=111&&record==12)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=115&&record==13)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=125&&record==14)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=135&&record==15)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=157&&record==16)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[4], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=172&&record==17)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=179&&record==18)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
            passtime=0;
            record=0;
        }
    }
    void Level1_5()
    {
        if(passtime>6f&&record==0)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon2.transform.position=new Vector3(15.0f, 0.0f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=12&&record==1)//sling shot
        {
            //GameObject Watermelon4=Instantiate(WatermelonPrefabs[3], transform);
            //Watermelon4.transform.position=new Vector3(13.0f, 0.0f, 0f);
            //Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=22&&record==2)
        {
            GameObject Watermelon3=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon3.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon3.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=34&&record==3)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[4], transform);
            Watermelon4.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=40&&record==4)
        {
            GameObject Watermelon1=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon1.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon1.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=54&&record==5)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[4], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=58&&record==6)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=68&&record==7)
        {
            GameObject Watermelon3=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon3.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon3.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=76&&record==8)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=90&&record==9)
        {
            GameObject Watermelon1=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon1.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon1.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=100&&record==10)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=110&&record==11)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon4.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=123&&record==12)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=130&&record==13)//slingshot upgrade
        {
            // GameObject Watermelon2=Instantiate(WatermelonPrefabs[0], transform);
            // Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            // Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=140&&record==14)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[4], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=144&&record==15)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=151&&record==16)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=161&&record==17)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=172&&record==18)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
            passtime=0;
            record=0;
        }
    }
    void Level1_6()
    {
        if(passtime>6f&&record==0)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[4], transform);
            Watermelon2.transform.position=new Vector3(15.0f, 0.0f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=12&&record==1)//sling shot
        {
            //GameObject Watermelon4=Instantiate(WatermelonPrefabs[3], transform);
            //Watermelon4.transform.position=new Vector3(13.0f, 0.0f, 0f);
            //Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=25&&record==2)
        {
            GameObject Watermelon3=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon3.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon3.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=35&&record==3)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon4.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=38&&record==4)
        {
            GameObject Watermelon1=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon1.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon1.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=46&&record==5)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=58&&record==6)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=70&&record==7)
        {
            GameObject Watermelon3=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon3.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon3.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=74&&record==8)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=78&&record==9)
        {
            GameObject Watermelon1=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon1.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon1.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=82&&record==10)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=92&&record==11)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon4.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon4.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=102&&record==12)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[4], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=108&&record==13)//slingshot upgrade
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[4], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=113&&record==14)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=130&&record==15)//slingshot upgrade
        {
            // GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            // Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            // Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=140&&record==16)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=145&&record==17)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=155&&record==18)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=170&&record==19)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
        }
        else if(passtime>=175&&record==20)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            Watermelon2.transform.rotation = Quaternion.Euler(0, 180f, 0);
            record++;
            passtime=0;
            record=0;
        }
    }
}
