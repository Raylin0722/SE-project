using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon3 : MonoBehaviour
{
    static public float speed=-2f;
    
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
