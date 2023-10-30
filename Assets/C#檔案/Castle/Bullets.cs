using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public int attackDamage = 150;
    private Vector2 velocity;  // 子弹的速度
    private Transform target;  // 追踪的目标
    private float speed = 4.0f;  // 子弹速度

    public void SetVelocity(Vector2 newVelocity)
    {
        velocity = newVelocity;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target != null)
        {
            Debug.Log("進入子彈C#檔案");
            // 计算子弹的方向
            Vector2 direction = (target.position - transform.position).normalized;
            // 旋转子弹的Sprite Renderer以面向移动方向
            if(target.tag=="Player")
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);   
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
        if (collision.CompareTag("enemy"))
        {
            AttackTarget(collision.gameObject);
            Debug.Log("碰到囉ENEMY");
            // 处理子弹与敌人碰撞的逻辑
            // 例如，可以造成伤害或者直接销毁子弹
            Destroy(gameObject);
        }
       else if (collision.CompareTag("Player"))
        {
            AttackTarget(collision.gameObject);
            Debug.Log("碰到囉PLAYER");
            // 处理子弹与敌人碰撞的逻辑
            // 例如，可以造成伤害或者直接销毁子弹
            Destroy(gameObject);
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
