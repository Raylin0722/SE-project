using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterManage : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject[] WatermelonPrefabs;
    [SerializeField] GameObject[] MyWatermelonPrefabs;
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


    void Update()
    {
        passtime+=Time.deltaTime;
        if(passtime>=6f&&record==0)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(15.0f, -1.8f, 0f);
            record++;
        }
        else if(passtime>=19&&record==1)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon4.transform.position=new Vector3(15.0f, -1.8f, 0f);
            record++;
        }
        else if(passtime>=35&&record==2)
        {
            GameObject Watermelon3=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon3.transform.position=new Vector3(15.0f, -1.8f, 0f);
            record++;
        }
        else if(passtime>=54&&record==3)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon4.transform.position=new Vector3(15.0f, -1.8f, 0f);
            record++;
        }
        else if(passtime>=64&&record==4)
        {
            GameObject Watermelon1=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon1.transform.position=new Vector3(15.0f, -1.8f, 0f);
            record++;
        }
        else if(passtime>=66&&record==5)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(15.0f, -1.8f, 0f);
            record++;
        }
        else if(passtime>=72&&record==6)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(15.0f, -1.8f, 0f);
            record++;
        }
        else if(passtime>=85&&record==7)
        {
            GameObject Watermelon3=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon3.transform.position=new Vector3(15.0f, -1.8f, 0f);
            record++;
        }
        else if(passtime>=105&&record==8)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(15.0f, -1.8f, 0f);
            record++;
        }
        else if(passtime>=122&&record==9)
        {
            GameObject Watermelon1=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon1.transform.position=new Vector3(15.0f, -1.8f, 0f);
            record++;
        }
        else if(passtime>=132&&record==10)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(15.0f, -1.8f, 0f);
            record++;
        }
        else if(passtime>=148&&record==11)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon4.transform.position=new Vector3(15.0f, -1.8f, 0f);
            record++;
        }
        else if(passtime>=162&&record==12)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(15.0f, -1.8f, 0f);
            record++;
        }

        if(!w1isUseable)
        {
            w1CoolTime+=Time.deltaTime;
            if(w1CoolTime>=4.77f)
            {
                w1CoolTime=0f;
                w1isUseable=true;
            }
        }
        if(!w2isUseable)
        {
            w2CoolTime+=Time.deltaTime;
            if(w2CoolTime>=3f)
            {
                w2CoolTime=0f;
                w2isUseable=true;
            }
        }
        if(!w3isUseable)
        {
            w3CoolTime+=Time.deltaTime;
            if(w3CoolTime>=7f)
            {
                w3CoolTime=0f;
                w3isUseable=true;
            }
        }
        if(!w4isUseable)
        {
            w4CoolTime+=Time.deltaTime;
            if(w4CoolTime>=5.5f)
            {
                w4CoolTime=0f;
                w4isUseable=true;
            }
        }
        if(!w5isUseable)
        {
            w5CoolTime+=Time.deltaTime;
            if(w5CoolTime>=4f)
            {
                w5CoolTime=0f;
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
            Watermelon1.transform.position=new Vector3(-7.08f, -1f, 0f);
        }
        
    }
    public void watermelon2Product()
    {
        if(ButtonFunction.currentEnergy>=70*Math.Pow(1.4, GameManage.level-1) && w2isUseable)
        {
            w2isUseable=false;
            ButtonFunction.currentEnergy-=70;
            GameObject Watermelon2=Instantiate(MyWatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(-7.08f, -1f, 0f);
        }
       
    }
    public void watermelon3Product()
    {
        if(ButtonFunction.currentEnergy>=250*Math.Pow(1.4, GameManage.level-1) && w3isUseable)
        {
            w3isUseable=false;
            ButtonFunction.currentEnergy-=250;
            GameObject Watermelon3=Instantiate(MyWatermelonPrefabs[2], transform);
            Watermelon3.transform.position=new Vector3(-7.08f, -1f, 0f);
        }
        
    }
    public void watermelon4Product()
    {
        if(ButtonFunction.currentEnergy>=150*Math.Pow(1.4, GameManage.level-1) && w4isUseable)
        {
            w4isUseable=false;
            ButtonFunction.currentEnergy-=150;
            GameObject Watermelon4=Instantiate(MyWatermelonPrefabs[3], transform);
            Watermelon4.transform.position=new Vector3(-7.08f, -1f, 0f);
        }
        
    }
    public void watermelon5Product()
    {
        if(ButtonFunction.currentEnergy>=200*Math.Pow(1.4, GameManage.level-1) && w5isUseable)
        {
            w5isUseable=false;
            ButtonFunction.currentEnergy-=200;
            GameObject Watermelon5=Instantiate(MyWatermelonPrefabs[4], transform);
            Watermelon5.transform.position=new Vector3(-7.08f, -1f, 0f);
        }
        
    }

    
}
