using UnityEngine;
public class Bomb : MonoBehaviour{
    private SpriteRenderer spriteRenderer; // 使用 SpriteRenderer 來設置圖片
    public float startTime;
    private float duration = 3f;
    public bool startBomb = false;
    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>(); // 獲取 SpriteRenderer 組件
        if (spriteRenderer == null)Debug.LogError("SpriteRenderer component is missing on the object.");
        Color newColor = spriteRenderer.color;
        newColor.a = 1f; // 初始時透明度為1
        spriteRenderer.color = newColor;
        gameObject.SetActive(false); // 初始時物件不可見
    }
    void Update(){
        if (startBomb){
            float timePassed = Time.time - startTime;
            if (timePassed < duration){
                float alpha = 1f - (timePassed / duration);
                Color newColor = spriteRenderer.color;
                newColor.a = alpha;
                spriteRenderer.color = newColor; // 修改透明度
            }else{
                Start();
                gameObject.SetActive(false);
                startBomb = false;
            }
        }
    }
    public void Trigger(){
        startBomb = true;
        startTime = Time.time;
        gameObject.SetActive(true);
    }
}