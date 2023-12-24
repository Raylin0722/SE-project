using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class detect : MonoBehaviour{
    public GameObject GotoUpdatePageTip;
    string latestRelease;
    string apkVersion;
    public class data{
        public string tag_name;
    }
    public Text Your_version;
    public Text Latest_version;
    IEnumerator Start(){
        latestRelease = "";
        apkVersion = "";
        UnityWebRequest githubRequest = UnityWebRequest.Get("https://api.github.com/repos/Raylin0722/SE-project/releases/latest");
        yield return githubRequest.SendWebRequest();
        if (githubRequest.result != UnityWebRequest.Result.Success){
            SceneManager.LoadScene("MainMenu");
            Debug.Log("Error: " + githubRequest.error);
        }else{
            data releaseData = JsonUtility.FromJson<data>(githubRequest.downloadHandler.text);
            latestRelease = releaseData.tag_name;
            AndroidJavaObject currentActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
            string packageName = currentActivity.Call<string>("getPackageName");
            AndroidJavaObject packageInfo = packageManager.Call<AndroidJavaObject>("getPackageInfo", packageName, 0);
            apkVersion = "v" + packageInfo.Get<string>("versionName");
            if (latestRelease == apkVersion)SceneManager.LoadScene("MainMenu");else GotoUpdatePageTip.SetActive(true);
            Your_version.text = apkVersion;
            Latest_version.text = latestRelease;
        }
    }
    public void GotoUpdatePage(){
        Application.OpenURL("https://xmu310.github.io");
        SceneManager.LoadScene("MainMenu");
    }
    public void GoToGame(){
        SceneManager.LoadScene("MainMenu");
    }
}