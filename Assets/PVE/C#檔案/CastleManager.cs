using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class CastleManager : MonoBehaviour{
    [SerializeField] GameObject[] CatlePrefabs;
    private GameObject castle;
    private float[] x=new float[12]{15.0f,15.0f,15.0f,15.0f,13.0f,13.0f,15.06f,15.73f,15.11f,15f,14.88f,14.88f};
    private float[] y=new float[12]{-1.2f,-1.2f,-1.2f,-1.2f,-1.2f,-1.0f,-0.84f,-0.7f,-1.07f,-1.08f,-1.13f,-1.13f};
    private int[] prefabnum=new int[12]{0,1,2,3,4,4,5,6,7,8,9,9};
    private int index;
    void Start(){
        Debug.Log(GameManage.currentLevel);
        index=GameManage.currentLevel;
        index=(index/10-1)*6+index%10-1;
        castle = Instantiate(CatlePrefabs[prefabnum[index]], transform);
        castle.transform.position = new Vector3(x[index], y[index], 0f);
    }
}