using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyW3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float speed=2f;
    // Update is called once per frame
    void Update()
    {
        if(transform.position.x<(7.08))
        {
            transform.Translate(speed*Time.deltaTime, 0, 0);
        }
    }
}
