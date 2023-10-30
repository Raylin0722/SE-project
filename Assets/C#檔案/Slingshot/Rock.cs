using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Assets.Scripts;

public class Rock : MonoBehaviour
{
    public RockState State
    {
        get;
        set;
    }
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        State = RockState.BeforeThrown;
    }
    public void OnThrow()
    {
        rb.isKinematic = false;
        State = RockState.Thrown;
    }
    
    // Update is called once per frame
      /*  private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("碰到"+collision.gameObject.tag);
        if (collision.gameObject.CompareTag("ground")) // 假设Ground是地面的标签
        {
            // 更改Rock的标签为"Player"
            gameObject.tag = "Player";
            //collision.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }*/
}



