using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private int attackDamage;
    private Vector2 velocity;  // 子弹的速度
    private Transform target;  // 追踪的目标
    private float speed = 5.0f;  // 子弹速度

    private float first_change=0;
    public void SetVelocity(Vector2 newVelocity)
    {
        velocity = newVelocity;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    public void SetAttackDamage(int newAttackDamage)
    {
        attackDamage = newAttackDamage;
    }
    public void SetBulletSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    void Update()
    {
    
        if (target != null)
        {

            // 计算子弹的方向
            Vector2 direction = (target.position - transform.position).normalized;
            // 旋转子弹的Sprite Renderer以面向移动方向
            if(target.tag=="Player")
            {   
                if(first_change==0)
                {
                    float newScaleX = transform.localScale.x * -1;
                    transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
                    first_change=1;
                } 
            }
            else
            {
                //transform.localScale = new Vector3(1, 1, 1);   
            }
            // 更新子弹的位置，以追踪目标位置
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else if(target == null)
        {
            Destroy(gameObject);
        }
    }

    // 当子弹碰撞到其他碰撞器时触发碰撞事件
    void OnTriggerEnter2D(Collider2D collision)
    {

        if(target.tag=="enemy")
        {
            if (collision.CompareTag("enemy"))
            {
                Destroy(gameObject);
                AttackTarget(collision.gameObject);
                
            }
        }
        else if(target.tag=="Player")
        {
            if (collision.CompareTag("Player"))
            {
                Destroy(gameObject);
                AttackTarget(collision.gameObject);
                
            }
        }
    }

    void AttackTarget(GameObject enemy)
    {
        //get current health
        Health enemyHealth = enemy.GetComponent<Health>();
        // attack
        enemyHealth.TakeDamage(attackDamage);
    }
}
