using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour{
    public int attackDamage = 150; // Attack damage
    public int maxHealth = 700; // Maximum health
    public float moveSpeed = 1.7f; // Movement speed
    public float attackRange = 7.0f; // Attack range
    public float attackCooldown = 2.0f; // Attack cooldown
    public int cost = 0; // Cost required

    private float lastAttackTime = 0.0f;
    public int currentHealth;
    private Animator animator; 
    private Rigidbody2D rb;
    private void Start() {
        gameObject.tag="Untagged";
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyLayer"), LayerMask.NameToLayer("Tower2Layer"),true);
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; // Initialize health
        animator = GetComponent<Animator>();
        animator.SetBool("isAttack", false);
    }

private void Update() {
        // Implement attack logic here, such as detecting enemies entering attack range and performing attacks
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        bool judgeMove=true;
        if(gameObject.tag=="Untagged")
        {
            return;
        }
        foreach (Collider2D col in hitColliders) {
            //加上碰到地板-蔡松豪
            if (gameObject.tag!=col.tag&&col.tag!="Untagged"&&col.tag!="ground"&&col.tag!="bullet") {
                judgeMove=false;
                //Debug.Log("判斷是否移動"+judgeMove);
                //Debug.Log("Detected enemy with tag: " + col.tag); // 打印敌人的标签
                AttackTarget(col.gameObject);
                break;
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

    // Calculate the movement direction based on the character's current forward direction
    Vector3 movement = new Vector3(moveSpeed, 0f, 0.0f) * Time.deltaTime;
    animator.CrossFade("walk", 0f);

    // Move the character
    transform.Translate(movement);
    }

    private void AttackTarget(GameObject enemy){
        if (Time.time - lastAttackTime >= attackCooldown){
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
        Debug.Log("碰到"+collision.gameObject.tag);
        if (collision.gameObject.CompareTag("ground")) // 假设Ground是地面的标签
        {
            Debug.Log("減速囉");
            // 更改Rock的标签为"Player"
            if(gameObject.layer==7){
                gameObject.tag = "Player";
                Debug.Log("便標籤囉");
            }
            else if(gameObject.layer==9){
                gameObject.tag = "enemy";
                Debug.Log("便敵人囉");
            }
            
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            
            //rb.velocity = Vector2.zero;
            //rb.isKinematic = true;
        }
    }
    //

    /*public void TakeDamage(int damage) {
        // Reduce health
        currentHealth -= damage;
        if (currentHealth <= 0) {
            Die(); // Execute death logic when health is less than or equal to 0
        }
    }

    private IEnumerator Die() {
        yield return null;
        // Implement death logic here, such as playing death animation or removing the object
        Destroy(gameObject);
    }*/
}
