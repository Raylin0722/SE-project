using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class Shop : MonoBehaviour
{
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scene
    public GameObject Close; // Close Button
    public GameObject page_Shop; // the page which you want to close
    public GameObject Ordinary_Open; // Ordinary Open Button
    public GameObject Special_Open; // Special Open Button
    public Image White_Image; // White image
    public float Time_White = 2.0f; // the duration of White image

    // Start is called before the first frame update
    void Start()
    {
        White_Image.gameObject.SetActive(false); // Initialization ( close )
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When click < X > 
    public void Button_Close()
    {
        page_Shop.SetActive(false);
        ALL_Button.SetActive(true); // Open All button in Main_Scene
    }

    // When click NORMAL < OPEN > 
    public void Button_normal_OPEN()
    {
        White_Image.gameObject.SetActive(true);
        // You can write them separately or according to parameters
        StartCoroutine(Fade_Screen()); // the fading animation before the drawing
    }

    // When click SPECIAL < OPEN > 
    public void Button_secial_OPEN()
    {
        White_Image.gameObject.SetActive(true);
        // You can write them separately or according to parameters
        StartCoroutine(Fade_Screen()); // the fading animation before the drawing
    }

    // 
    private IEnumerator Fade_Screen()
    {
        float Elapsed_Time = 0f; // the time elpased now
        Color startColor = new Color(1f, 1f, 1f, 0f); // Transparent Color
        Color targetColor = White_Image.color;

        while (Elapsed_Time < Time_White)
        {
            White_Image.color = Color.Lerp(startColor, targetColor, Elapsed_Time / Time_White);
            Elapsed_Time = Elapsed_Time + Time.deltaTime;
            yield return null;
        }

        // the process of drawing game
        Thread.Sleep(2000); // (you have to delete Thread.Sleep(2000);)

        White_Image.gameObject.SetActive(false);
    }
}
