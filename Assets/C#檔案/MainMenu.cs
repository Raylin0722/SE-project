using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour {
    public Button registerButton;
    public Button loginButton;
    public Button playButton;
    public TMP_Text playerDisplay;
    private void Start() {
        if(DBManager.LoggedIn) {
            playerDisplay.text = "Player: " + DBManager.username; 
        }
        registerButton.interactable = !DBManager.LoggedIn;
        loginButton.interactable = !DBManager.LoggedIn;
        playButton.interactable = DBManager.LoggedIn;
    }
    public void GoToRegister() {
        SceneManager.LoadScene(1);
    }

    public void GoToLogin() {
        SceneManager.LoadScene(2);
    }

    public void GoToGame() {
        SceneManager.LoadScene(3);
    }
}