using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyW2 : MonoBehaviour
{
    // Start is called before the first frame update
    static public float blood=(float)(400*Math.Pow(1.2, (GameManage.level-1)));

    void Start()
    {
        
    }

    float speed=(float)(4*Math.Pow(1.1, (GameManage.level-1)));
    // Update is called once per frame
    void Update()
    {
        if(transform.position.x<(7.08))
        {
            transform.Translate(speed*Time.deltaTime, 0, 0);
        }
    }
}
