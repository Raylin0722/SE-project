using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_weapon : MonoBehaviour
{
    private GameObject enemy;
    public float force;
    private Rigidbody2D rb;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GameObject.FindGameObjectWithTag("enemy");
        Vector3 direction = enemy.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x,direction.y).normalized*force;
        //用來轉子彈角度
        float rot = Mathf.Atan2(-direction.y,-direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rot);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>10)
        {
            //之後扣血
            //ohter.gameObject.GetComponent<enemyhealth>().health -=20;
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("enemy"))
        {
            Debug.Log("HI");
            Destroy(gameObject);
        }
        
    }
}