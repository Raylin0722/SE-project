using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : MonoBehaviour
{
    public GameObject bullet;
    private GameObject enemy;
    public Transform bulletPos;
    private float timer;
    void Start(){
        enemy = GameObject.FindGameObjectWithTag("enemy");
    }
    void Update(){
        
        float distance = Vector2.Distance(transform.position , enemy.transform.position);
        if(distance <10)
        {
            timer += Time.deltaTime;
            if(timer>2)
            {
                timer = 0;
                shoot();
            }
        }

    }
    void shoot(){
        Instantiate(bullet,bulletPos.position,Quaternion.identity);
    }
}