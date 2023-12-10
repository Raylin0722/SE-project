using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Wind : MonoBehaviour{
    public static GameObject ColdWindObject;
    private Image image; // 使用 Image 來設置顏色
    private bool isActivated = false;
    private float startTime;
    private float duration = 3f;
    public bool who;
    void Start(){
        image = GetComponent<Image>(); // 獲取 Image 組件
        Color newColor = image.color;
        newColor.a = 0f; // 初始時透明度為0
        image.color = newColor;
        //gameObject.SetActive(false); // 初始時物件不可見
    }
    void Update(){
        if (isActivated){
            float timePassed = Time.time - startTime;
            if (timePassed < duration){
                float alpha = 1f - (timePassed / duration);
                Color newColor = image.color;
                newColor.a = alpha;
                image.color = newColor; // 修改透明度
            }else{
                isActivated = false;
                Color newColor = image.color;
                newColor.a = 0f;
                image.color = newColor;
                //gameObject.SetActive(false); // 隱藏物件
            }
        }
    }
    public void ActivateWind(){
        isActivated = true;
        startTime = Time.time;
        //gameObject.SetActive(true); // 顯示物件
    }
}