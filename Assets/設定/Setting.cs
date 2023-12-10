using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

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
    public string URL_About = "https://xmu310.github.io"; // the website about About
    public GameObject[] number; // version number about LAST

    // Start is called before the first frame update
    void Start()
    {
        picture_Vibration_ON.SetActive(false);
        picture_Notification_ON.SetActive(false);
        Version();
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
        // Modify position
        Vector3 currentScale = Vibration.transform.localScale;
        currentScale.x = -currentScale.x;
        Vibration.transform.localScale = currentScale;

        if(picture_Vibration_ON.activeSelf)
        {
            picture_Vibration_ON.SetActive(false); // ON => false
            picture_Vibration_OFF.SetActive(true); // OFF => true
            Vector3 currentPosition = Vibration.transform.localPosition;
            currentPosition.x = currentPosition.x - 15;
            Vibration.transform.localPosition = currentPosition;
            Vibration_function();
        }
        else
        {
            picture_Vibration_ON.SetActive(true); // ON => true
            picture_Vibration_OFF.SetActive(false); // OFF => false
            Vector3 currentPosition = Vibration.transform.localPosition;
            currentPosition.x = currentPosition.x + 15;
            Vibration.transform.localPosition = currentPosition;
        }
    }

    // Vibration
    public void Vibration_function()
    {
        if(Application.platform==RuntimePlatform.Android)
        {
            Handheld.Vibrate();
        }
    }

    // When click < Notification >
    public void Button_Notification()
    {
        // Modify position
        Vector3 currentScale = Notification.transform.localScale;
        currentScale.x = -currentScale.x;
        Notification.transform.localScale = currentScale;

        if (picture_Notification_ON.activeSelf)
        {
            picture_Notification_ON.SetActive(false); // ON => false
            picture_Notification_OFF.SetActive(true); // OFF => true
            Vector3 currentPosition = Notification.transform.localPosition;
            currentPosition.x = currentPosition.x - 15;
            Notification.transform.localPosition = currentPosition;
        }
        else
        {
            picture_Notification_ON.SetActive(true); // ON => true
            picture_Notification_OFF.SetActive(false); // OFF => false
            Vector3 currentPosition = Notification.transform.localPosition;
            currentPosition.x = currentPosition.x + 15;
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

    // 
    public void Version()
    {
        // Read current version number
        if(Application.platform!=RuntimePlatform.Android) 
        {
            Debug.Log("你設定下面的版本號無法顯示，因為你不是用手機!!!!");
            return;    
        }
        AndroidJavaObject currentActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
        string packageName = currentActivity.Call<string>("getPackageName");
        AndroidJavaObject packageInfo = packageManager.Call<AndroidJavaObject>("getPackageInfo", packageName, 0);
        string apkVersion = "v" + packageInfo.Get<string>("versionName");
        
        string[] Version_Parts = apkVersion.Split('.');
        string LAST = Version_Parts[2];
        if(int.TryParse(LAST, out int LAST_number))
        {
            if(LAST_number<10)
            {
                number[LAST_number].gameObject.SetActive(true);
            }
            else if(LAST_number<100)
            {
                number[LAST_number/10].gameObject.SetActive(true);
                number[10+LAST_number%10].gameObject.SetActive(true);
            }
            else
            {
                number[LAST_number/100].gameObject.SetActive(true);
                number[10+(LAST_number/10)%10].gameObject.SetActive(true);
                number[20+LAST_number%10].gameObject.SetActive(true);
            }
        }
    }
}