using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class Registration : MonoBehaviour {
    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    public Button submitButton;
    [SerializeField] string token;

    public void CallRegister() {
        StartCoroutine(Register());
    }

    IEnumerator Register() {
        WWWForm form = new WWWForm();
        form.AddField("username", nameField.text);
        form.AddField("password", passwordField.text);

        UnityWebRequest www = UnityWebRequest.Post("https://pc167.csie.ntnu.edu.tw/register", form);
        // UnityWebRequest www = UnityWebRequest.Post("http://localhost:5000/register", form);
        
        yield return www.SendWebRequest();

        if(www.downloadHandler.text[0] == '0') {
            Debug.Log("User Created Successfully.");
            TokenManager.Instance.Token = www.downloadHandler.text.Split('\t')[1];
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }else {
            Debug.Log("User Creation Failed. Error #" + www.downloadHandler.text);
        }
    }

    public void VerifyInputs() {
        submitButton.interactable = (nameField.text.Length > 0 && passwordField.text.Length >= 8);
    }

    public void GoToMain() {
        SceneManager.LoadScene("MainMenu");
    }
}
