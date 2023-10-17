using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tower_health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth=100;

    private void Start(){
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        if (currentHealth <= 0){
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die() {
        yield return null;
        // Implement death logic here, such as playing death animation or removing the object

        Destroy(gameObject);
    }
}
