using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;
using TMPro;
using ServerMethod;
using System;

public class chargeControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject failed;
    public GameObject success;
    public int cardID;
    public GameObject check_page;
    private ServerMethod.Server ServerScript; // Server.cs
    
    // Start is called before the first frame update
    void Start()
    {
        ServerScript = FindObjectOfType<ServerMethod.Server>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Freesia()
    {
        cardID = 0;
        check_page.SetActive(true);
    }
    public void Bank()
    {
        cardID = 1;
        check_page.SetActive(true);
    }

    public void NO()
    {
        cardID = 0;
        check_page.SetActive(false);
    }
    public void YES()
    {
        StartCoroutine(Surver_Top_up((result) => 
        {
            Debug.Log(result);
            Debug.Log(cardID);
            if(result==true)
            {
                check_page.SetActive(false);
                StartCoroutine(Bank_animation(1f));
            }
            else
            {
                check_page.SetActive(false);
                StartCoroutine(Freesia_animation(1f));
            }
            cardID = 0;
        }));
    }
    private IEnumerator Surver_Top_up(Action<bool> callback)
    {
        IEnumerator coroutine = ServerScript.topUp(cardID);
        yield return StartCoroutine(coroutine);
        
        Return result = coroutine.Current as Return;
        bool bool_success = false;
        if(result!=null)
        {
            bool_success = result.success;
        }

        callback.Invoke(bool_success);
    }
    IEnumerator Freesia_animation(float delay)
    {
        failed.SetActive(false);
        failed.SetActive(true);
        yield return new WaitForSeconds(delay);
        failed.SetActive(false);
    }
    IEnumerator Bank_animation(float delay)
    {
        success.SetActive(false);
        success.SetActive(true);
        yield return new WaitForSeconds(delay);
        success.SetActive(false);
    }

    
}