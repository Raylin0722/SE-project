using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ServerMethod;
using UnityEngine.Networking;
public class Setting : MonoBehaviour{
    [SerializeField] Slider volumeSlider;
    public GameObject Vibration; // Vibration Button
    public GameObject picture_Vibration_ON; // ON in Vibration
    public GameObject picture_Vibration_OFF; // OFF in Vibration 
    public string URL_Feedback = "https://forms.gle/XSt1FpKRyCtY8qVU9"; // the website about About
    public string URL_About = "https://xmu310.github.io"; // the website about About
    public GameObject[] number; // version number about LAST
    private ServerMethod.Server ServerScript; // Server.cs
    void Start() {
        Version();
        if(MainMenu.message!=87) {
            ServerScript = FindObjectOfType<ServerMethod.Server>();
            AudioListener.volume = volumeSlider.value = Mathf.Clamp01(ServerScript.backVolume/100f);
            Button_Display();
        }
    }
    public void ChangeVolume() {
        AudioListener.volume = volumeSlider.value;
        if(MainMenu.message==87)    MainMenu.backVolume = (int)(volumeSlider.value*100);
        else ServerScript.backVolume = (int)(volumeSlider.value*100);
    }
    // When click < Vibration >
    public void Button_Vibration() {
        if(MainMenu.message==87)    MainMenu.shock = !MainMenu.shock;
        else    ServerScript.shock = !ServerScript.shock;
        Button_Display();
    }
    public void Button_Display() {
        bool shock;
        if(MainMenu.message==87)    shock = MainMenu.shock;
        else    shock = ServerScript.shock;
        if(shock==false) {
            picture_Vibration_ON.SetActive(false); // ON => false
            picture_Vibration_OFF.SetActive(true); // OFF => true
            Vibration.transform.localScale = new Vector3(3.73134f,3.980096f,0f);
            Vibration.transform.localPosition = new Vector3(220.6f,30f,0f);
        }else {
            picture_Vibration_ON.SetActive(true); // ON => true
            picture_Vibration_OFF.SetActive(false); // OFF => false
            Vibration.transform.localScale = new Vector3(-3.73134f,3.980096f,0f);
            Vibration.transform.localPosition = new Vector3(235.6f,30f,0f);
            if(Application.platform==RuntimePlatform.Android && picture_Vibration_ON)   Handheld.Vibrate();
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
            }else {
                number[LAST_number/100].gameObject.SetActive(true);
                number[10+(LAST_number/10)%10].gameObject.SetActive(true);
                number[20+LAST_number%10].gameObject.SetActive(true);
            }
        }
    }
}