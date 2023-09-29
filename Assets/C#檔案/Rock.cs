using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
  /*  public enum  RockState
    {
        BeforeThrown,
        Thrown
    }
    public enum SlingshotState
    {
        Idle,
        Pulling,
        Flying
    }
*/
namespace Assets.Scripts
{
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
    
}

}

