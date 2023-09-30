using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tower_health : MonoBehaviour
{
    public float health;
    public float maxhealth;
    public Image healthbar;
    // Start is called before the first frame update
    void Start()
    {
        maxhealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = Mathf.Clamp(health/maxhealth,0,1);
        if(health<=0)
        {
            
            Destroy(gameObject);
        }
    }
}