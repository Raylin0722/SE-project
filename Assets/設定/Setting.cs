using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public GameObject Close; // Close Button
    public GameObject page_Setting; // the page which you want to close
    public Slider Sound_effects; // Sound_effects Slider
    public Slider Background_music; // Background_music Slider
    public GameObject Vibration; // Vibration Button
    public GameObject picture_Vibration_ON; // ON in Vibration
    public GameObject picture_Vibration_OFF; // OFF in Vibration 
    public GameObject Notification; // Notification Button
    public GameObject picture_Notification_ON; // ON in Notification
    public GameObject picture_Notification_OFF; // OFF in Notification
    public GameObject Feedback; // Feedback Button
    public string URL_Feedback = "https://forms.gle/XSt1FpKRyCtY8qVU9"; // the website about About
    public GameObject About; // About Button
    public string URL_About = "https://stanlybaa.github.io/test2/"; // the website about About

    // Start is called before the first frame update
    void Start()
    {
        picture_Vibration_ON.SetActive(false);
        picture_Notification_ON.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When click < X > 
    public void Button_Close()
    {
        page_Setting.SetActive(false);
    }

    // When drag < Sound_effects >
    ///////////////////////////////////////////////////////////////////////////////////////

    // When drag < Background_music >
    ///////////////////////////////////////////////////////////////////////////////////////

    // When click < Vibration >
    public void Button_Vibration()
    {
        // 水平翻轉按紐
        Vector3 currentScale = Vibration.transform.localScale;
        currentScale.x = -currentScale.x;
        Vibration.transform.localScale = currentScale;

        if (picture_Vibration_ON.activeSelf)
        {
            picture_Vibration_ON.SetActive(false); // ON => 關閉
            picture_Vibration_OFF.SetActive(true); // OFF => 開啟
            Vector3 currentPosition = Vibration.transform.localPosition;
            currentPosition.x = currentPosition.x - 15; // 位置調整
            Vibration.transform.localPosition = currentPosition;
        }
        else
        {
            picture_Vibration_ON.SetActive(true); // ON => 開啟
            picture_Vibration_OFF.SetActive(false); // OFF => 關閉
            Vector3 currentPosition = Vibration.transform.localPosition;
            currentPosition.x = currentPosition.x + 15; // 位置調整
            Vibration.transform.localPosition = currentPosition;
        }
    }

    // When click < Notification >
    public void Button_Notification()
    {
        // 水平翻轉按紐
        Vector3 currentScale = Notification.transform.localScale;
        currentScale.x = -currentScale.x;
        Notification.transform.localScale = currentScale;

        if (picture_Notification_ON.activeSelf)
        {
            picture_Notification_ON.SetActive(false); // ON => 關閉
            picture_Notification_OFF.SetActive(true); // OFF => 開啟
            Vector3 currentPosition = Notification.transform.localPosition;
            currentPosition.x = currentPosition.x - 15; // 位置調整
            Notification.transform.localPosition = currentPosition;
        }
        else
        {
            picture_Notification_ON.SetActive(true); // ON => 開啟
            picture_Notification_OFF.SetActive(false); // OFF => 關閉
            Vector3 currentPosition = Notification.transform.localPosition;
            currentPosition.x = currentPosition.x + 15; // 位置調整
            Notification.transform.localPosition = currentPosition;
        }
    }

    // When click < Feedback >
    public void Button_Feedback()
    {
        Application.OpenURL(URL_Feedback); // Open Website
    }

    // When click < About >
    public void Button_About()
    {
        Application.OpenURL(URL_About); // Open Website
    }

}
