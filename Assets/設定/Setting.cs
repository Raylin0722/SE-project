using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ServerMethod;
using UnityEngine.Networking;
public class Setting : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    public GameObject Vibration; // Vibration Button
    public GameObject picture_Vibration_ON; // ON in Vibration
    public GameObject picture_Vibration_OFF; // OFF in Vibration 
    public GameObject Notification; // Notification Button
    public GameObject picture_Notification_ON; // ON in Notification
    public GameObject picture_Notification_OFF; // OFF in Notification
    public string URL_Feedback = "https://forms.gle/XSt1FpKRyCtY8qVU9"; // the website about About
    public string URL_About = "https://xmu310.github.io"; // the website about About
    public GameObject[] number; // version number about LAST
    private ServerMethod.Server ServerScript; // Server.cs

    void Start() {
        picture_Vibration_ON.SetActive(false);
        picture_Notification_ON.SetActive(false);
        Version();
        if(MainMenu.message!=87) {
            ServerScript = FindObjectOfType<ServerMethod.Server>();
            volumeSlider.value = ServerScript.backVolume;
            AudioListener.volume = volumeSlider.value;
            Button_Display();
        }
    }
    public void ChangeVolume() {
        AudioListener.volume = volumeSlider.value;
        if(MainMenu.message==87)    MainMenu.backVolume = (int)(volumeSlider.value*100);
        else {
            ServerScript.backVolume = (int)(volumeSlider.value*100);
            StartCoroutine(Setting_to_Server());
        }
    }
    // When click < Vibration >
    public void Button_Vibration() {
        if(MainMenu.message==87)    MainMenu.shock = !MainMenu.shock;
        else    ServerScript.shock = !ServerScript.shock;
        Button_Display();
        if(MainMenu.message==100)    StartCoroutine(Setting_to_Server());
    }
    public void Button_Display() {
        bool shock;
        if(MainMenu.message==87)    shock = MainMenu.shock;
        else    shock = ServerScript.shock;
        if(shock==false) {
            picture_Vibration_ON.SetActive(false); // ON => false
            picture_Vibration_OFF.SetActive(true); // OFF => true
            Vibration.transform.localScale = new Vector3(3.73134f,3.980096f,0f);
            Vibration.transform.localPosition = new Vector3(202.6f,96.7f,0f);
        }
        else {
            picture_Vibration_ON.SetActive(true); // ON => true
            picture_Vibration_OFF.SetActive(false); // OFF => false
            Vibration.transform.localScale = new Vector3(-3.73134f,3.980096f,0f);
            Vibration.transform.localPosition = new Vector3(217.6f,96.7f,0f);
        }
    }
    // Vibration
    public IEnumerator Setting_to_Server() {
        IEnumerator coroutine = ServerScript.setting(ServerScript.volume,ServerScript.backVolume,ServerScript.shock);
        yield return StartCoroutine(coroutine);
        Return result = coroutine.Current as Return;
        if(Application.platform==RuntimePlatform.Android && picture_Vibration_ON)   Handheld.Vibrate();
    }
    // When click < Notification >
    public void Button_Notification() {
        // Modify position
        Vector3 currentScale = Notification.transform.localScale;
        currentScale.x = -currentScale.x;
        Notification.transform.localScale = currentScale;

        if (picture_Notification_ON.activeSelf) {
            picture_Notification_ON.SetActive(false); // ON => false
            picture_Notification_OFF.SetActive(true); // OFF => true
            Vector3 currentPosition = Notification.transform.localPosition;
            currentPosition.x = currentPosition.x - 15;
            Notification.transform.localPosition = currentPosition;
        }
        else {
            picture_Notification_ON.SetActive(true); // ON => true
            picture_Notification_OFF.SetActive(false); // OFF => false
            Vector3 currentPosition = Notification.transform.localPosition;
            currentPosition.x = currentPosition.x + 15;
            Notification.transform.localPosition = currentPosition;
        }
    }
    // When click < Feedback >
    public void Button_Feedback() {
        Application.OpenURL(URL_Feedback); // Open Website
    }
    // When click < About >
    public void Button_About() {
        Application.OpenURL(URL_About); // Open Website
    }
    // Show current version 
    public void Version() {
        if(Application.platform!=RuntimePlatform.Android) {
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
        if(int.TryParse(LAST, out int LAST_number)) {
            if(LAST_number<10)    number[LAST_number].gameObject.SetActive(true);
            else if(LAST_number<100) {
                number[LAST_number/10].gameObject.SetActive(true);
                number[10+LAST_number%10].gameObject.SetActive(true);
            }
            else {
                number[LAST_number/100].gameObject.SetActive(true);
                number[10+(LAST_number/10)%10].gameObject.SetActive(true);
                number[20+LAST_number%10].gameObject.SetActive(true);
            }
        }
    }
}