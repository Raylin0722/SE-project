using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour{
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
        //主塔死亡並觸發動畫
        GetComponent<Animator>().SetTrigger("crash");
        yield return new WaitForSeconds(2.0f);

        
        // Implement death logic here, such as playing death animation or removing the object
        

        Destroy(gameObject);
    }
}
