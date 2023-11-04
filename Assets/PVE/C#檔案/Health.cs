using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour{
    public int maxHealth = 100;
    public int currentHealth=100;
    

    private void Start(){
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage){
        if(gameObject.layer==6||gameObject.layer==8)
        {
            Debug.Log("被打了");
        }
        currentHealth -= damage;
        if (currentHealth <= 0){
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die() {

        // Implement death logic here, such as playing death animation or removing the object
        
        if(gameObject.layer==6||gameObject.layer==8)
        {
            //主塔死亡並觸發動畫
            Debug.Log("爆掉囉");
            GetComponent<Animator>().SetTrigger("crash");
            yield return new WaitForSeconds(1.2f);
            Destroy(gameObject);
            Time.timeScale=0f;
        }
        else 
        {
            Destroy(gameObject);
        }
        

    }
}
