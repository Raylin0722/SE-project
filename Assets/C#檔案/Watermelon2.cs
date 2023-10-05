using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon2 : MonoBehaviour
{
    static public float speed=-4f;
    
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
        if(transform.position.x>(-7.08))
        {
            transform.Translate(speed*Time.deltaTime, 0, 0);
        }
    }
    
}
