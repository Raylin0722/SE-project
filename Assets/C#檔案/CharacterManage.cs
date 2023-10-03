using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManage : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject[] WatermelonPrefabs;
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


    

    
}
