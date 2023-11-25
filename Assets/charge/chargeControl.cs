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
        failed.SetActive(true);
    }
    public void bank()
    {
        success.SetActive(true);
    }
}
