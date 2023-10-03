using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    
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
