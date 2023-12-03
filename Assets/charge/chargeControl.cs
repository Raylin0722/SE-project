using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargeControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject failed;
    [SerializeField] GameObject success;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void freesia()
    {
        //failed.SetActive(true);
        StartCoroutine(Freesia(1f));
    }
    public void bank()
    {
        //success.SetActive(true);
        StartCoroutine(Bank(1f));
    }

    IEnumerator Freesia(float delay)
    {
        failed.SetActive(false);
        failed.SetActive(true);
        yield return new WaitForSeconds(delay);
        failed.SetActive(false);
    }
    IEnumerator Bank(float delay)
    {
        success.SetActive(false);
        success.SetActive(true);
        yield return new WaitForSeconds(delay);
        success.SetActive(false);
    }
}