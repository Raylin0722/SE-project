using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts;
public class GameManage : MonoBehaviour{
    static public bool toolIsActive=false;
    static public bool toolIsUseable=true;
    static public int currentLevel;
    //[SerializeField] GameObject toolFrame;
    [SerializeField] GameObject W1B;
    [SerializeField] GameObject W2B;
    [SerializeField] GameObject W3B;
    [SerializeField] GameObject W4B;
    [SerializeField] GameObject W5B;
    [SerializeField] GameObject Ground;
    private ServerMethod.Server ServerScript;
    private GameObject[] WB;
    void Start(){
        //toolIsActive=false;
        //level=1;
        //toolFrame.SetActive(false);
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        Ground.SetActive(false);
        WB=new GameObject[]{W1B,W2B,W3B,W4B,W5B};
        for(int i=0;i<5;i++)WB[i].SetActive(false);
    }
    private float remaintime=0f;
    private float[] toolcooltime=new float[2]{15f,30f};
    private int[] wEnergy_builtin=new int[7]{150,70,250,150,200,150,200};
    private int[] wEnergy=new int[5];
    private int who;
    private int wholevel;
    void Update(){
        for(int i=0;i<5;i++){
            who=ServerScript.lineup[i]-1;
            wholevel=ServerScript.character[who];
            wEnergy[i]=(int)((double)wEnergy_builtin[who]*Math.Pow(1.4,wholevel-1));
        }
        if(ButtonFunction.GameIsStart){
            Ground.SetActive(true);
            for(int i=0;i<5;i++)WB[i].SetActive(ButtonFunction.currentEnergy<wEnergy[i]);
        }
        if(!toolIsUseable){
            remaintime+=Time.deltaTime;
            if(remaintime>=toolcooltime[ServerScript.lineup[5]-1]){
                remaintime=0f;
                toolIsActive=true;
            }
        }
    }
}
