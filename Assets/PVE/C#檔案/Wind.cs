using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wind : MonoBehaviour
{
    public static GameObject ColdWindObject;
    private SpriteRenderer spriteRenderer;
    private bool isActivated = false;
    private float startTime;
    private float duration = 3f; // 持续时间为3秒
    public bool who;//0是我方的風 1是敵人的風
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // 初始时不可见
    }

    void Update()
    {
        if (isActivated)
        {
            float timePassed = Time.time - startTime;
            if (timePassed < duration)
            {
                // 在淡化过程中，逐渐降低透明度
                float alpha = 1f - (timePassed / duration); // 计算透明度
                spriteRenderer.color = new Color(1f, 1f, 1f, alpha); // 更新透明度
            }
            else
            {
                isActivated = false;
                spriteRenderer.enabled = false; // 淡化结束后隐藏对象
            }
        }
    }
    public void ActivateWind()
    {
        isActivated = true;
        spriteRenderer.enabled = true; // 显示对象
        startTime = Time.time;
    }
}