using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts;
public class CharacterManage : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject[] WatermelonPrefabs;
    [SerializeField] GameObject[] MyWatermelonPrefabs;
    [SerializeField] GameObject[] w1coolbar;
    [SerializeField] GameObject[] w2coolbar;
    [SerializeField] GameObject[] w3coolbar;
    [SerializeField] GameObject[] w4coolbar;
    [SerializeField] GameObject[] w5coolbar;

    public GameObject castle1;
    float passtime;
    int record;

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
        passtime+=Time.deltaTime;
        
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
        if(!w2isUseable)
        {
            w2CoolTime+=Time.deltaTime;
            temp2+=Time.deltaTime;
            if(temp2>=(3f*Math.Pow(1.15, GameManage.level-1))*0.25f)
            {
                w2coolbar[w2i%4].SetActive(false);
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
    }

    public void watermelon1Product()
    {
        if(ButtonFunction.currentEnergy>=150*Math.Pow(1.4, GameManage.level-1) && w1isUseable)
        {
            w1isUseable=false;
            ButtonFunction.currentEnergy-=150;
            GameObject Watermelon1=Instantiate(MyWatermelonPrefabs[0], transform);
            //Watermelon1.transform.position=new Vector3(-7.08f, -1f, 0f);
            Slingshot shot = castle1.GetComponent<Slingshot>();
            shot.Rock=Watermelon1;
            if (shot != null)
            {
                shot.slingshotState = SlingshotState.Idle;
            }

            w1coolbar[0].SetActive(true);
            w1coolbar[2].SetActive(true);
            w1coolbar[3].SetActive(true);
            w1coolbar[1].SetActive(true);
        }
        
    }
    public void watermelon2Product()
    {
        if(ButtonFunction.currentEnergy>=70*Math.Pow(1.4, GameManage.level-1) && w2isUseable)
        {
            w2isUseable=false;
            ButtonFunction.currentEnergy-=70;
            GameObject Watermelon2=Instantiate(MyWatermelonPrefabs[1], transform);
            //Watermelon2.transform.position=new Vector3(-7.08f, -1f, 0f);
            Slingshot shot = castle1.GetComponent<Slingshot>();
            shot.Rock=Watermelon2;
            if (shot != null)
            {
                shot.slingshotState = SlingshotState.Idle;
            }
            w2coolbar[0].SetActive(true);
            w2coolbar[2].SetActive(true);
            w2coolbar[3].SetActive(true);
            w2coolbar[1].SetActive(true);
        }
       
    }
    public void watermelon3Product()
    {

        if(ButtonFunction.currentEnergy>=250*Math.Pow(1.4, GameManage.level-1) && w3isUseable)
        {
            w3isUseable=false;
            ButtonFunction.currentEnergy-=250;
            GameObject Watermelon3=Instantiate(MyWatermelonPrefabs[2], transform);
            //Watermelon3.transform.position=new Vector3(-7.08f, -1f, 0f);
            Slingshot shot = castle1.GetComponent<Slingshot>();
            shot.Rock=Watermelon3;
            if (shot != null)
            {
                shot.slingshotState = SlingshotState.Idle;
            }
            w3coolbar[0].SetActive(true);
            w3coolbar[2].SetActive(true);
            w3coolbar[3].SetActive(true);
            w3coolbar[1].SetActive(true);
        }
        
    }
    public void watermelon4Product()
    {
        if(ButtonFunction.currentEnergy>=150*Math.Pow(1.4, GameManage.level-1) && w4isUseable)
        {
            w4isUseable=false;
            ButtonFunction.currentEnergy-=150;
            GameObject Watermelon4=Instantiate(MyWatermelonPrefabs[3], transform);
            //Watermelon4.transform.position=new Vector3(-7.08f, -1f, 0f);
            Slingshot shot = castle1.GetComponent<Slingshot>();
            shot.Rock=Watermelon4;
            if (shot != null)
            {
                shot.slingshotState = SlingshotState.Idle;
            }
            w4coolbar[0].SetActive(true);
            w4coolbar[2].SetActive(true);
            w4coolbar[3].SetActive(true);
            w4coolbar[1].SetActive(true);
        }
        
    }
    public void watermelon5Product()
    {
        if(ButtonFunction.currentEnergy>=200*Math.Pow(1.4, GameManage.level-1) && w5isUseable)
        {
            w5isUseable=false;
            ButtonFunction.currentEnergy-=200;
            GameObject Watermelon5=Instantiate(MyWatermelonPrefabs[4], transform);
            //Watermelon5.transform.position=new Vector3(-7.08f, -1f, 0f);
            Slingshot shot = castle1.GetComponent<Slingshot>();
            shot.Rock=Watermelon5;
            if (shot != null)
            {
                shot.slingshotState = SlingshotState.Idle;
            }
            w5coolbar[0].SetActive(true);
            w5coolbar[2].SetActive(true);
            w5coolbar[3].SetActive(true);
            w5coolbar[1].SetActive(true);
        }
        
    }

    
}
