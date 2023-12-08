using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class CastleManager : MonoBehaviour
{
    [SerializeField] GameObject[] CatlePrefabs;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GameManage.currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
