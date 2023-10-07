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
    private void Start() {
        currentHealth = maxHealth; // Initialize health
        animator = GetComponent<Animator>();
        animator.SetBool("isAttack", false);
    }

    private void Update() {
        // Implement attack logic here, such as detecting enemies entering attack range and performing attacks
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        bool judgeMove=true;
        foreach (Collider2D col in hitColliders) {
            if (gameObject.tag!=col.tag) {
                judgeMove=false;
                AttackTarget(col.gameObject);
                break;
            }
        }
        if(judgeMove){
            animator.SetBool("isAttack", false);
            MoveCharacter();
        }
        else{
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
