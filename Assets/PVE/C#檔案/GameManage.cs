using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManage : MonoBehaviour
{
    
    // Start is called before the first frame update
    static public bool toolIsActive=false;
    static public int level=1;
    static public bool toolIsUseable=true;
    static public int currentLevel;

    //[SerializeField] GameObject toolFrame;
    [SerializeField] GameObject W1B;
    [SerializeField] GameObject W2B;
    [SerializeField] GameObject W3B;
    [SerializeField] GameObject W4B;
    [SerializeField] GameObject W5B;

    [SerializeField] GameObject Ground;
    
    private GameObject[] WB;
    private int[] wEnergy=new int[5]{150,70,250,150,200};

    void Start()
    {
        //toolIsActive=false;
        //level=1;
        //toolFrame.SetActive(false);
        Ground.SetActive(false);
        WB=new GameObject[]{W1B,W2B,W3B,W4B,W5B};
        for(int i=0;i<5;i++)WB[i].SetActive(false);
    }

    float fifteen=0f;
    // Update is called once per frame
    void Update()
    {
        if(ButtonFunction.GameIsStart)
        {
            Ground.SetActive(true);
            for(int i=0;i<5;i++){
                if(ButtonFunction.currentEnergy>=wEnergy[i]*Math.Pow(1.4, level-1))
                {
                    WB[i].SetActive(false);
                }
                else
                {
                    WB[i].SetActive(true);
                }
            }
        }

        if(!toolIsUseable)
        {
            fifteen+=Time.deltaTime;
            if(fifteen>=15)
            {
                fifteen=0f;
                toolIsActive=true;
            }
        }
    }
}
