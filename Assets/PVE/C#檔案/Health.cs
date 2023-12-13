using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Assets.Scripts;
public class Health : MonoBehaviour{
    //ButtonFunction buttonFunction = new ButtonFunction(); 
    //public ButtonFunction buttonFunction;   
    public int maxHealth = 100;
    public int currentHealth=100;
    public int who;
    private int wholevel;
    public bool isEnemy;
    public bool isTower;
    [SerializeField] GameObject HpBar;
    private GameObject hpBarInstance;
    private ServerMethod.Server ServerScript;
    void Awake(){
        if (gameObject.layer == 8)CreateHpBar();
    }
    private void Start(){
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        if(isTower){
            wholevel=isEnemy?(GameManage.currentLevel/10):ServerScript.castleLevel;
            maxHealth+=(wholevel-1)*500;
        }else{
            wholevel=isEnemy?(GameManage.currentLevel/10):ServerScript.character[who];
            maxHealth=(int)((double)maxHealth*Math.Pow(1.2,wholevel-1));
        }
        currentHealth = maxHealth;
    }
    private void CreateHpBar(){
        // 創建 HP Bar 實例
        hpBarInstance = Instantiate(HpBar, Vector3.zero, Quaternion.identity);
        // 設置 HP Bar 為 UI 介面的子物體
        hpBarInstance.transform.SetParent(GameObject.Find("Canvas").transform, false); // 將 Canvas 換成你的實際 UI 介面的名稱
        RectTransform hpBarRectTransform = hpBarInstance.GetComponent<RectTransform>();
        hpBarRectTransform.anchoredPosition = new Vector2(814f, 237f);
        hpBarInstance.GetComponent<Image>().fillAmount = 1f; // 初始化填充量為滿
        // 設置 HP Bar 的其他屬性，例如位置、縮放等
    }
    private void UpdateHpBar(){
          // 更新 HP Bar 的顯示，例如填充量
        float fillAmount = (float)currentHealth / maxHealth;
        hpBarInstance.GetComponent<Image>().fillAmount = fillAmount;
    }
    public void TakeDamage(int damage){
        int currentFrame = Time.frameCount;
        //Debug.Log("Current Frame: " + currentFrame);
        Debug.Log(gameObject+" -50");
        if(gameObject.layer==6){
            currentHealth =(currentHealth-damage<=maxHealth) ? currentHealth-damage : maxHealth ;
            HpBar.GetComponent<Image>().fillAmount = (float)currentHealth/maxHealth;
        }else if(gameObject.layer==8){
            currentHealth =(currentHealth-damage<=maxHealth) ? currentHealth-damage : maxHealth ;
            UpdateHpBar();
        }else currentHealth =(currentHealth-damage<=maxHealth) ? currentHealth-damage : maxHealth ;
        if (currentHealth <= 0){
            Debug.Log("死了",gameObject);
            StartCoroutine(Die());
        }
    }
    private IEnumerator Die() {
        // Implement death logic here, such as playing death animation or removing the object
        float disappearDuration = 0.5f;     //消失的時間
        float timer = 0f;
        if(gameObject.layer==6){
            //主塔死亡並觸發動畫
            GetComponent<Animator>().SetTrigger("crash");
            yield return new WaitForSeconds(1.5f);
            ButtonFunction.judge_defeat=1;
            //Time.timeScale=0f;
        }else if(gameObject.layer==8){
            //主塔死亡並觸發動畫
            Debug.Log("敵方主堡爆掉");
            GetComponent<Animator>().SetTrigger("crash");
            yield return new WaitForSeconds(1.5f);
            ButtonFunction.judge_victory=1;
        }else{   
            yield return new WaitForEndOfFrame();// WaitForEndOfFrame();
            gameObject.tag = "Untagged";
            while (timer < disappearDuration){
                Color currentColor = GetComponentInChildren<SpriteRenderer>().color;              // 變透明
                currentColor.a = Mathf.Lerp(1f, 0f, timer / disappearDuration);
                GetComponentInChildren<SpriteRenderer>().color = currentColor;
                timer += Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}