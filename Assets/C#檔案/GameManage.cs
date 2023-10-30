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
    //[SerializeField] GameObject toolFrame;
    [SerializeField] GameObject W1B;
    [SerializeField] GameObject W2B;
    [SerializeField] GameObject W3B;
    [SerializeField] GameObject W4B;
    [SerializeField] GameObject W5B;

    [SerializeField] GameObject Ground;
    


    void Start()
    {
        //toolIsActive=false;
        //level=1;
        //toolFrame.SetActive(false);
        Ground.SetActive(false);
        W1B.SetActive(false);
        W2B.SetActive(false);
        W3B.SetActive(false);
        W4B.SetActive(false);
        W5B.SetActive(false);

    }

    float fifteen=0f;
    // Update is called once per frame
    void Update()
    {
        if(ButtonFunction.GameIsStart)
        {
            Ground.SetActive(true);
            // W1B.SetActive(true);
            // W2B.SetActive(true);
            // W3B.SetActive(true);
            // W4B.SetActive(true);
            // W5B.SetActive(true);
            if(ButtonFunction.currentEnergy>=150*Math.Pow(1.4, level-1))
            {
                W1B.SetActive(false);
            }
            else
            {
                W1B.SetActive(true);
            }
            if(ButtonFunction.currentEnergy>=70*Math.Pow(1.4, level-1))
            {
                W2B.SetActive(false);
            }
            else
            {
                W2B.SetActive(true);
            }if(ButtonFunction.currentEnergy>=250*Math.Pow(1.4, level-1))
            {
                W3B.SetActive(false);
            }
            else
            {
                W3B.SetActive(true);
            }if(ButtonFunction.currentEnergy>=150*Math.Pow(1.4, level-1))
            {
                W4B.SetActive(false);
            }
            else
            {
                W4B.SetActive(true);
            }if(ButtonFunction.currentEnergy>=200*Math.Pow(1.4, level-1))
            {
                W5B.SetActive(false);
            }
            else
            {
                W5B.SetActive(true);
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
