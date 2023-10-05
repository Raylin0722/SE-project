using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon1 : MonoBehaviour
{
    // Start is called before the first frame update
    static public float speed=-1.7f;
    
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
