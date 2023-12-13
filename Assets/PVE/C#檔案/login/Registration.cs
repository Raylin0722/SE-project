using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text;

public class Registration : MonoBehaviour {
    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    public Button submitButton;
    public TMP_Text warningText;
    public Image password_too_short;
    public Image user_existed;
    public Image user_not_found;
    public Image incorrect_password;
    public Image User_Low_Length;
    [SerializeField] string token;

    public GameObject page_Rigister; // the page of rigister
    public GameObject page_User_consent; // the page of user consent
    private bool bool_agree = false; // whether you agree to the user consent

    private void Start() {
        password_too_short.enabled = false;
        user_existed.enabled = false;
        user_not_found.enabled = false;
        incorrect_password.enabled = false;
    }
    public void CallRegister() {
        StartCoroutine(Register());
    }

    private void Update()
    {
        if(bool_agree==false)
        {
            page_Rigister.gameObject.SetActive(false);
            page_User_consent.gameObject.SetActive(true);
        }
        else
        {
            page_Rigister.gameObject.SetActive(true);
            page_User_consent.gameObject.SetActive(false);
        }

        if(nameField.text.Length>0)
        {
            if(nameField.text[nameField.text.Length-1]>=48 && nameField.text[nameField.text.Length-1]<=57)
            {
                nameField.text = nameField.text.Substring(0,nameField.text.Length-1);
            }
            
            char lastChar = nameField.text[nameField.text.Length - 1];
            if(char.IsLower(lastChar) && nameField.text.Length==1)
            {
                nameField.text = nameField.text.Substring(0, nameField.text.Length - 1) + char.ToUpper(lastChar);
            }
            if(char.IsUpper(lastChar) && nameField.text.Length>1)
            {
                nameField.text = nameField.text.Substring(0, nameField.text.Length - 1) + char.ToLower(lastChar);
            }
        }
        if(passwordField.text.Length>0)
        {
            passwordField.onValidateInput += ValidateInput;
        }
    }
    public void Agree()
    {
        bool_agree = true;
    }

    char ValidateInput(string text, int charIndex, char addedChar)
    {
        if((addedChar >= 'a' && addedChar <= 'z') || (addedChar >= 'A' && addedChar <= 'Z') || (addedChar >= '0' && addedChar <= '9'))
        {
            if(addedChar>='A' && addedChar<='Z')
            {
                addedChar = char.ToLower(addedChar);
            }
            return addedChar;
        }
        else
        {
            return '\0';
        }
    }

    IEnumerator Register() {
        if(nameField.text.Length<4)
        {
            User_Low_Length.gameObject.SetActive(true);
            submitButton.interactable = false;
            yield return new WaitForSeconds(2f);
            User_Low_Length.gameObject.SetActive(false);
            submitButton.interactable = true;
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("username", nameField.text);
            form.AddField("password", passwordField.text);

            UnityWebRequest www = UnityWebRequest.Post("https://pc167.csie.ntnu.edu.tw/register", form);
            // UnityWebRequest www = UnityWebRequest.Post("http://localhost:5000/register", form);
            
            yield return www.SendWebRequest();

            if(www.downloadHandler.text[0] == '0') {
                Debug.Log("User Created Successfully.");
                TokenManager.Instance.Token = www.downloadHandler.text.Split('\t')[1];
                TokenManager.Instance.Username = nameField.text;
                UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
            }else {
                Debug.Log("User Creation Failed. Error #" + www.downloadHandler.text);
                switch(www.downloadHandler.text[0]) {
                    case '1':
                        password_too_short.enabled = true;
                        submitButton.interactable = false;
                        yield return new WaitForSeconds(2f);
                        password_too_short.enabled = false;
                        submitButton.interactable = true;
                        break;
                    case '2':
                        user_existed.enabled = true;
                        submitButton.interactable = false;
                        yield return new WaitForSeconds(2f);
                        user_existed.enabled = false;
                        submitButton.interactable = true;
                        break;
                    case '3':
                        user_not_found.enabled = true;
                        submitButton.interactable = false;
                        yield return new WaitForSeconds(2f);
                        user_not_found.enabled = false;
                        submitButton.interactable = true;
                        break;
                    case '4':
                        incorrect_password.enabled = true;
                        submitButton.interactable = false;
                        yield return new WaitForSeconds(2f);
                        incorrect_password.enabled = false;
                        submitButton.interactable = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void GoToMain() {
        SceneManager.LoadScene("MainMenu");
    }
}
