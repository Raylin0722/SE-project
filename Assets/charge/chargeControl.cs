using System.Collections;
using UnityEngine;
using ServerMethod;
using System;
using UnityEngine.UI;
public class chargeControl : MonoBehaviour{
    public GameObject failed;
    public GameObject success;
    public int cardID;
    public GameObject check_page;
    private ServerMethod.Server ServerScript; // Server.cs
    public Button close;
    void Start(){
        ServerScript = FindObjectOfType<ServerMethod.Server>();
    }
    public void Freesia(){
        cardID = 0;
        close.interactable = false;
    }
    public void Bank(){
        cardID = 1;
        close.interactable = false;
    }
    public void NO(){
        cardID = 0;
        close.interactable = true;
    }
    public void YES(){
        StartCoroutine(Surver_Top_up((result) => {
            if(result==true)    StartCoroutine(Bank_animation(1f));
            else    StartCoroutine(Freesia_animation(1f));
            cardID = 0;
        }));
    }
    private IEnumerator Surver_Top_up(Action<bool> callback){
        IEnumerator coroutine = ServerScript.topUp(cardID);
        yield return StartCoroutine(coroutine);
        Return result = coroutine.Current as Return;
        bool bool_success = false;
        if(result!=null)    bool_success = result.success;
        callback.Invoke(bool_success);
    }
    IEnumerator Freesia_animation(float delay) {
        failed.SetActive(false);
        failed.SetActive(true);
        close.interactable = false;
        yield return new WaitForSeconds(delay);
        failed.SetActive(false);
        close.interactable = true;
    }
    IEnumerator Bank_animation(float delay) {
        success.SetActive(false);
        success.SetActive(true);
        close.interactable = false;
        yield return new WaitForSeconds(delay);
        success.SetActive(false);
        close.interactable = true;
    }
}