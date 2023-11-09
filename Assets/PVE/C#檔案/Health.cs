using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour{
    //ButtonFunction buttonFunction = new ButtonFunction(); 
    //public ButtonFunction buttonFunction;   
    [SerializeField] GameObject HPbar;

    public int maxHealth = 100;
    public int currentHealth=100;
    
    

    private void Start(){
        currentHealth = maxHealth;
        
    }

    public void TakeDamage(int damage){
        if(gameObject.layer==6||gameObject.layer==8)
        {
            Debug.Log("被打了");
            HPbar.GetComponent<Image>().fillAmount=currentHealth/maxHealth;
        }
        currentHealth -= damage;
        

        if (currentHealth <= 0){
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die() {

        // Implement death logic here, such as playing death animation or removing the object
        
        if(gameObject.layer==6)
        {
            //主塔死亡並觸發動畫
            GetComponent<Animator>().SetTrigger("crash");
            yield return new WaitForSeconds(1.2f);
            ButtonFunction.judge_defeat=1;
            //Time.timeScale=0f;
            
        }
        else if(gameObject.layer==8)
        {
            //主塔死亡並觸發動畫
            Debug.Log("敵方主堡爆掉");
            GetComponent<Animator>().SetTrigger("crash");
            yield return new WaitForSeconds(1.2f);
            ButtonFunction.judge_victory=1;
            //Time.timeScale=0f;
        }
        else 
        {
            Destroy(gameObject);
        }
        

    }
}
