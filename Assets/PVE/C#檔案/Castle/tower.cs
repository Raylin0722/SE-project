using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : MonoBehaviour
{
    public int attackDamage = 150;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    private float attackRange = 20.0f;
    private float timer; // 计时器

    public float bulletSpeed;

    void Update()
    {
        timer += Time.deltaTime;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        
        

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            float judge_front = enemy.transform.position.x - transform.position.x;

            if (judge_front > 0)
            {
                if (distance < attackRange)
                {
                    //timer += Time.deltaTime; // 使用 Time.deltaTime 累积时间
                    //Debug.Log("有人囉 timer 是" + timer);

                    // 判断是否满足攻击条件（例如，每秒攻击一次）
                    if (timer >= 1.0f)
                    {
                        timer = 0; // 重置计时器
                        Debug.Log("開扁囉");
                        Shoot(enemy.transform); // 传递敌人的位置
                    }
                }
            }
            //跑出地图把它删掉
            else if (judge_front < -5)
            {
                Destroy(enemy);
            }
        }
    }

    void Shoot(Transform target)
    {
        Debug.Log("射出");
        // 创建子弹的实例
        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Debug.Log("目標位置"+target.position);
        // 计算子弹的方向，以便追踪敌人的未来位置
        Vector2 direction = (target.position - bulletSpawnPoint.position).normalized;
        Debug.Log("方向"+direction);
        // 设置子弹速度
        Bullets bulletScript = newBullet.GetComponent<Bullets>();
        if (bulletScript != null)
        {
            Debug.Log("創建子彈");
            bulletScript.SetVelocity(direction * bulletSpeed);
            bulletScript.SetTarget(target);
        
        }
    
    }
}
