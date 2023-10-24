using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    
    // Start is called before the first frame update
    static public bool toolIsActive=false;
    static public int level=1;
    static public bool toolIsUseable=true;
    //[SerializeField] GameObject toolFrame;


    void Start()
    {
        //toolIsActive=false;
        //level=1;
        //toolFrame.SetActive(false);
    }

    float fifteen=0f;
    // Update is called once per frame
    void Update()
    {
        if(!toolIsUseable)
        {
            fifteen+=Time.deltaTime;
            if(fifteen>=15)
            {
                fifteen=0f;
                toolIsActive=true;
            }
        }
        // if(Health.currentHealth==0)
        // {
        //     Time.timeScale=0f;
        // }
    }
}
