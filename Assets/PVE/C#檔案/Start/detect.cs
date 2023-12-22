using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

public class detect : MonoBehaviour
{
    public GameObject GotoUpdatePageTip;
    string latestRelease;
    string apkVersion;
    public class data{
        public string tag_name;
    }
    
    IEnumerator Start()
    {
        latestRelease = "";
        apkVersion = "";
        UnityWebRequest githubRequest = UnityWebRequest.Get("https://api.github.com/repos/Raylin0722/SE-project/releases/latest");

        yield return githubRequest.SendWebRequest();

        if (githubRequest.result != UnityWebRequest.Result.Success)
        {
            SceneManager.LoadScene("MainMenu");
            Debug.Log("Error: " + githubRequest.error);
        }
        else
        {
            data releaseData = JsonUtility.FromJson<data>(githubRequest.downloadHandler.text);
            latestRelease = releaseData.tag_name;
            AndroidJavaObject currentActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        
            AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
            string packageName = currentActivity.Call<string>("getPackageName");
            AndroidJavaObject packageInfo = packageManager.Call<AndroidJavaObject>("getPackageInfo", packageName, 0);

            apkVersion = "v" + packageInfo.Get<string>("versionName");

            if (latestRelease == apkVersion)
            {
                SceneManager.LoadScene("MainMenu");
                Debug.Log("Versions match!");
            }
            else
            {
                GotoUpdatePageTip.SetActive(true);
                //Application.OpenURL("https://xmu310.github.io/update.html?currentVersion="+apkVersion+"&latestVersion="+latestRelease);
                //SceneManager.LoadScene("MainMenu");
                //Debug.Log("Versions do not match!");
            }
        }
    }
    public void GotoUpdatePage(){
        GotoUpdatePageTip.SetActive(false);
        Application.OpenURL("https://xmu310.github.io/update.html?currentVersion="+apkVersion+"&latestVersion="+latestRelease);
    }
    public void GoToGame(){
        SceneManager.LoadScene("MainMenu");
    }
}


