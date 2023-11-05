using UnityEngine;
using UnityEngine.UI;

public class FadeOutEffect : MonoBehaviour
{
    public Image image; // 引用 Image 组件
    public float fadeOutDuration = 10.0f; // 淡出时间

    private float timer = 0.0f;
    private bool isFadingOut = false;

    private void Start()
    {
        image.enabled = false; // 初始时不可见
    }

    private void Update()
    {
        if (isFadingOut)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeOutDuration;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1 - progress);

            if (timer >= fadeOutDuration)
            {
                isFadingOut = false;
                image.enabled = false; // 隐藏 Image 组件
            }
        }
    }

    public void StartCooldown()
    {
        isFadingOut = true;
        timer = 0.0f;
        image.enabled = true; // 立刻显示 Image 组件
    }
}
