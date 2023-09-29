using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class Game : MonoBehaviour
{
    public TMP_Text playerDisplay;
    public TMP_Text scoreDisplay;

    private void Awake() {
        if(DBManager.username == null) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        playerDisplay.text = "Player: " + DBManager.username; 
        scoreDisplay.text = "Score: " + DBManager.score; 
    }

    public void CallSaveData() {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData() {
        WWWForm form = new WWWForm();
        form.AddField("username", DBManager.username);
        form.AddField("score", DBManager.score);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:80/sqlconnect/savedata.php", form);
        yield return www.SendWebRequest();

        if(www.downloadHandler.text == "0") {
            Debug.Log("Game Saved");
        }else {
            Debug.Log("Save failed. Error #" + www.downloadHandler.text);
        }

        DBManager.LogOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void IncreaseScore() {
        DBManager.score++;
        scoreDisplay.text = "Score: " + DBManager.score; 
    }
}