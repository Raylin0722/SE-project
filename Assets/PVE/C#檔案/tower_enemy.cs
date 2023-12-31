using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts;
public class tower_enemy : MonoBehaviour{
    public int attackDamage = 150;
    public GameObject bulletPrefab;
    private Transform bulletSpawnPoint;
    public float attackRange = 15.0f;
    private float timer; // 计时器
    public float bulletSpeed=7;
    public static float windCooldown=0.0f;
    static public bool enemyToolIsActive=false;
    static public bool enemyToolIsUseable=true;
    private ServerMethod.Server ServerScript;
    private int wholevel;
    void Start(){
        if(MainMenu.message==100)   ServerScript = FindObjectOfType<ServerMethod.Server>();
        wholevel=GameManage.currentLevel/10;
        attackDamage+=(wholevel-1)*50;
        bulletSpawnPoint = new GameObject().transform;
        bulletSpawnPoint.position = new Vector3(15, 0, 0);
    }
    void Update(){
        //判斷敵人的冷風
        if(windCooldown>0.0f){
            float last=windCooldown;
            windCooldown-=Time.deltaTime;
            if(last>7.0f&&windCooldown<=7.0f) {
                ButtonFunction.itemWind(false,true);
                enemyToolIsActive=false;
            }
            if(windCooldown<0) windCooldown=0.0f;
        }else enemyToolIsUseable=true;
        timer += Time.deltaTime;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
        enemies = enemies.OrderBy(col => col.transform.position.x).ToArray();
        enemies = enemies.Reverse().ToArray();
        foreach (GameObject enemy in enemies){
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            float judge_front = enemy.transform.position.x - transform.position.x;
            //啟用冷風,如果距離小於5
            if (distance <= 5.0f&&enemyToolIsUseable) {
                ButtonFunction.itemWind(false,false);
                Wind[] Winds = FindObjectsOfType<Wind>();
                foreach (Wind Wind in Winds)if(Wind.who)Wind.ActivateWind();
                windCooldown=10.0f;
                enemyToolIsUseable=false;
                enemyToolIsActive=true;
            }
            if (judge_front < 0){
                if (distance < attackRange){
                    //timer += Time.deltaTime; // 使用 Time.deltaTime 累积时间
                    //Debug.Log("有人囉 timer 是" + timer);
                    // 判断是否满足攻击条件（例如，每秒攻击一次）
                    if (timer >= 1.3f){
                        timer = 0; // 重置计时器
                        //Debug.Log("開扁囉");
                        Shoot(enemy.transform); // 传递敌人的位置
                    }
                }
            }else if (judge_front < -5)Destroy(enemy);//跑出地图把它删掉
        }
    }
    void Shoot(Transform target){
        //Debug.Log("射出");
        // 创建子弹的实例
        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        //Debug.Log("目標位置"+target.position);
        // 计算子弹的方向，以便追踪敌人的未来位置
        Vector2 direction = (target.position - bulletSpawnPoint.position).normalized;
        //Debug.Log("方向"+direction);
        // 设置子弹速度
        Bullets bulletScript = newBullet.GetComponent<Bullets>();
        if (bulletScript != null){
            //Debug.Log("創建子彈");
            bulletScript.SetVelocity(direction * bulletSpeed);
            bulletScript.SetTarget(target);
            bulletScript.SetAttackDamage(attackDamage);
            bulletScript.SetBulletSpeed(bulletSpeed);
        }
    }
}