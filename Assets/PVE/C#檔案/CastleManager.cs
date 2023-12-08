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
        switch (GameManage.currentLevel)
        {
            case 11:
                GameObject castle = Instantiate(CatlePrefabs[0], transform);
                castle.transform.position = new Vector3(15.0f, -1.0f, 0f);
                
                break;
            case 12:
                Instantiate(CatlePrefabs[1]);
                break;
            case 13:
                Instantiate(CatlePrefabs[2]);
                break;
            case 14:
                Instantiate(CatlePrefabs[3]);
                break;
            case 15:
                Instantiate(CatlePrefabs[4]);
                break;
            case 16:
                Instantiate(CatlePrefabs[4]);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
