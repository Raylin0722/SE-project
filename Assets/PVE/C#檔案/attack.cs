using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Attack : MonoBehaviour{
    public int attackDamage = 150; // Attack damage
    public int maxHealth = 700; // Maximum health
    public float moveSpeed = 1.7f; // Movement speed
    public float windMoveSpeed ;
    public float unwindMoveSpeed ;

    public float attackRange = 7.0f; // Attack range
    public float attackCooldown = 2.0f; // Attack cooldown
    public int cost = 0; // Cost required

    private float lastAttackTime = 0.0f;
    public int currentHealth;
    [SerializeField]private Animator animator; 
    private Rigidbody2D rb;
    public bool isAngel ;
    private void Start() {
        unwindMoveSpeed=moveSpeed;
        windMoveSpeed=moveSpeed*0.5f;
        gameObject.tag="Untagged";
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyLayer"), LayerMask.NameToLayer("Tower2Layer"),true);
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; // Initialize health
        // animator = GetComponentInChildren<Animator>();
        animator.SetBool("isAttack", false);
        animator.SetBool("isStart", false);
        lastAttackTime = 0.0f;
    }
    

    private void Update() {
        // Implement attack logic here, such as detecting enemies entering attack range and performing attacks
        //----
        Vector2 center = transform.position;
        Vector2 bottomLeft = (gameObject.tag=="Player") 
        ? new Vector2(center.x , center.y - 10) : new Vector2(center.x - attackRange, center.y - 10);
        Vector2 topRight = (gameObject.tag=="Player") 
        ? new Vector2(center.x + attackRange, center.y + 10) : new Vector2(center.x , center.y + 10);
        Collider2D[] hitColliders =  Physics2D.OverlapAreaAll(bottomLeft, topRight);
        //----
        bool judgeMove=true;
        if(gameObject.tag=="Untagged")
        {
            return;
        }
        //做排序,x小到大
        hitColliders = hitColliders.OrderBy(col => col.transform.position.x).ToArray();
        if(gameObject.tag=="enemy") {
            hitColliders = hitColliders.Reverse().ToArray();
        }
        if (isAngel) {Debug.Log("ansasdsdas");
            Collider2D tmp = null;
            bool hasObject = false ;
            foreach (Collider2D col in hitColliders) {
                if(col.tag!=gameObject.tag&&(col.tag=="enemy"||col.tag=="Player")
                &&((transform.position.x-col.transform.position.x)*transform.right.x<=0)) {
                    hasObject=true;
                }
                if (col.tag == gameObject.tag &&((transform.position.x-col.transform.position.x)*transform.right.x<=0)){
                    if(!tmp){
                        tmp = col;
                        continue;
                    }
                    Debug.Log(col.tag);
                    Health colHealth = col.GetComponent<Health>();
                    Health tmpHealth = tmp.GetComponent<Health>();
                    if ((colHealth.currentHealth != colHealth.maxHealth && colHealth.currentHealth < tmpHealth.currentHealth)) {
                        tmp = col;
                    }
                }
            }
            //Debug.Log(hasObject);
            float targetX = (gameObject.tag == "Player") ? -850.5f : -880.5926f;
            float distanceX = Mathf.Abs(transform.position.x - targetX);
            if(object.ReferenceEquals(tmp, gameObject)
            &&tmp.GetComponent<Health>().currentHealth != tmp.GetComponent<Health>().maxHealth){
                judgeMove = false;
                AttackTarget(tmp.gameObject);
            }
            else if ( tmp.GetComponent<Health>().currentHealth != tmp.GetComponent<Health>().maxHealth
            &&tmp.gameObject.layer!=6&&tmp.gameObject.layer!=8) {
                judgeMove = false;
                AttackTarget(tmp.gameObject);
            }
            else if(distanceX<=attackRange || hasObject){
                judgeMove = false;
                AttackTarget(gameObject);
            }
        }
        else{
            foreach (Collider2D col in hitColliders) {
            //加上碰到地板-蔡松豪
                if (gameObject.tag!=col.tag&&col.tag!="Untagged"&&col.tag!="ground"&&col.tag!="bullet"
                &&((transform.position.x-col.transform.position.x)*transform.right.x<=0)) {
                    judgeMove=false;
                    // Debug.Log("判斷是否移動"+judgeMove);
                    //Debug.Log("Detected enemy with tag: " + col.tag); // 打印敌人的标签
                    AttackTarget(col.gameObject);
                    break;
                }
            }
        }
        if(judgeMove){
            //Debug.Log("移動");
            animator.SetBool("isAttack", false);
            MoveCharacter();
        }
        else{
            rb.velocity = Vector2.zero;
            //Debug.Log("不移動");
            animator.SetBool("isAttack", true);
        }
    }
    private void MoveCharacter(){        
    lastAttackTime = Time.time;
    // Calculate the movement direction based on the character's current forward direction
    Vector3 movement = new Vector3(moveSpeed, 0f, 0.0f) * Time.deltaTime;
    animator.CrossFade("walk", 0f);

    // Move the character
    transform.Translate(movement);
    }

    private void AttackTarget(GameObject enemy){
        if (Time.time - lastAttackTime >= attackCooldown){
            print(gameObject.name + " Start attack");
            //play attack animation 
            animator.SetBool("isAttack", true);
            animator.CrossFade("attack", 0f);
            //get current health
            Health enemyHealth = enemy.GetComponent<Health>();
                // attack
                enemyHealth.TakeDamage(attackDamage);
                lastAttackTime = Time.time;
        }
    }

    //加上落地之後改tag為player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("碰到"+collision.gameObject.tag);
        if (collision.gameObject.CompareTag("ground")) // 假设Ground是地面的标签
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isStart", true);
            //moveSpeed=unwindMoveSpeed;
            // 更改Rock的标签为"Player"
            if(gameObject.layer==7){
                gameObject.tag = "Player";
                //Debug.Log("便標籤囉");
            }
            else if(gameObject.layer==9){
                gameObject.tag = "enemy";
                //Debug.Log("便敵人囉");
            }
            
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            
            //rb.velocity = Vector2.zero;
            //rb.isKinematic = true;
        }
    }

}

