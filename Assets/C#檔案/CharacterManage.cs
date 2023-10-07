using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Update()
    {
        passtime+=Time.deltaTime;
        if(passtime>=6f&&record==0)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            record++;
        }
        else if(passtime>=19&&record==1)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon4.transform.position=new Vector3(7.09f, -0.87f, 0f);
            record++;
        }
        else if(passtime>=35&&record==2)
        {
            GameObject Watermelon3=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon3.transform.position=new Vector3(7.09f, -0.87f, 0f);
            record++;
        }
        else if(passtime>=54&&record==3)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon4.transform.position=new Vector3(7.09f, -0.87f, 0f);
            record++;
        }
        else if(passtime>=64&&record==4)
        {
            GameObject Watermelon1=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon1.transform.position=new Vector3(7.09f, -0.87f, 0f);
            record++;
        }
        else if(passtime>=66&&record==5)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            record++;
        }
        else if(passtime>=72&&record==6)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            record++;
        }
        else if(passtime>=85&&record==7)
        {
            GameObject Watermelon3=Instantiate(WatermelonPrefabs[2], transform);
            Watermelon3.transform.position=new Vector3(7.09f, -0.87f, 0f);
            record++;
        }
        else if(passtime>=105&&record==8)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            record++;
        }
        else if(passtime>=122&&record==9)
        {
            GameObject Watermelon1=Instantiate(WatermelonPrefabs[0], transform);
            Watermelon1.transform.position=new Vector3(7.09f, -0.87f, 0f);
            record++;
        }
        else if(passtime>=132&&record==10)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            record++;
        }
        else if(passtime>=148&&record==11)
        {
            GameObject Watermelon4=Instantiate(WatermelonPrefabs[3], transform);
            Watermelon4.transform.position=new Vector3(7.09f, -0.87f, 0f);
            record++;
        }
        else if(passtime>=162&&record==12)
        {
            GameObject Watermelon2=Instantiate(WatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(7.09f, -0.87f, 0f);
            record++;
        }
    }

    public void watermelon1Product()
    {
        if(ButtonFunction.currentEnergy>=150)
        {
            ButtonFunction.currentEnergy-=150;
            GameObject Watermelon1=Instantiate(MyWatermelonPrefabs[0], transform);
            Watermelon1.transform.position=new Vector3(-7.08f, -1f, 0f);
        }
        
    }
    public void watermelon2Product()
    {
        if(ButtonFunction.currentEnergy>=70)
        {
            ButtonFunction.currentEnergy-=70;
            GameObject Watermelon2=Instantiate(MyWatermelonPrefabs[1], transform);
            Watermelon2.transform.position=new Vector3(-7.08f, -1f, 0f);
        }
       
    }
    public void watermelon3Product()
    {
        if(ButtonFunction.currentEnergy>=250)
        {
            ButtonFunction.currentEnergy-=250;
            GameObject Watermelon3=Instantiate(MyWatermelonPrefabs[2], transform);
            Watermelon3.transform.position=new Vector3(-7.08f, -1f, 0f);
        }
        
    }
    public void watermelon4Product()
    {
        if(ButtonFunction.currentEnergy>=150)
        {
            ButtonFunction.currentEnergy-=150;
            GameObject Watermelon4=Instantiate(MyWatermelonPrefabs[3], transform);
            Watermelon4.transform.position=new Vector3(-7.08f, -1f, 0f);
        }
        
    }
    public void watermelon5Product()
    {
        if(ButtonFunction.currentEnergy>=200)
        {
            ButtonFunction.currentEnergy-=200;
            GameObject Watermelon5=Instantiate(MyWatermelonPrefabs[4], transform);
            Watermelon5.transform.position=new Vector3(-7.08f, -1f, 0f);
        }
        
    }

    
}
