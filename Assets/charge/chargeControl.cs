using System.Collections;
using UnityEngine;
using ServerMethod;
using System;
public class chargeControl : MonoBehaviour
{
    public GameObject failed;
    public GameObject success;
    public int cardID;
    public GameObject check_page;
    private ServerMethod.Server ServerScript; // Server.cs
    
    void Start(){
        ServerScript = FindObjectOfType<ServerMethod.Server>();
    }
    public void Freesia(){
        cardID = 0;
    }
    public void Bank(){
        cardID = 1;
    }
    public void NO(){
        cardID = 0;
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
        yield return new WaitForSeconds(delay);
        failed.SetActive(false);
    }
    IEnumerator Bank_animation(float delay) {
        success.SetActive(false);
        success.SetActive(true);
        yield return new WaitForSeconds(delay);
        success.SetActive(false);
    }
}