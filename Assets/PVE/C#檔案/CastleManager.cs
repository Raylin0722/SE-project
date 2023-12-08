using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class CastleManager : MonoBehaviour
{
    [SerializeField] GameObject[] CatlePrefabs;
    private GameObject castle;
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log(GameManage.currentLevel);
        switch (GameManage.currentLevel)
        {
            case 11:
                castle = Instantiate(CatlePrefabs[0], transform);
                castle.transform.position = new Vector3(15.0f, -1.2f, 0f);
                break;
            case 12:
                castle = Instantiate(CatlePrefabs[1], transform);
                castle.transform.position = new Vector3(15.0f, -1.2f, 0f);
                break;
            case 13:
                castle = Instantiate(CatlePrefabs[2], transform);
                castle.transform.position = new Vector3(15.0f, -1.2f, 0f);
                break;
            case 14:
                castle = Instantiate(CatlePrefabs[3], transform);
                castle.transform.position = new Vector3(15.0f, -1.2f, 0f);
                break;
            case 15:
                castle = Instantiate(CatlePrefabs[4], transform);
                castle.transform.position = new Vector3(13.0f, -1.2f, 0f);
                break;
            case 16:
                castle = Instantiate(CatlePrefabs[4], transform);
                castle.transform.position = new Vector3(13.0f, -1.0f, 0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
