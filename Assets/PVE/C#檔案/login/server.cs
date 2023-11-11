using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class data{
    public bool success;
    public string token;
    public int money;
    public int[] exp;
    public int[] character;
    public int[] lineup;
    public int tear;
    public int castleLevel;
    public int slingshotLevel;
    public int[] clearance;
    public int energy;
    public string updateTime;
    public int volume;
    public int backVolume;
    public bool shock;
    public bool remind;
    public string chestTime;
    public int faction;
    public int[] props;
}
public class server : MonoBehaviour
{
    public bool success;
    public string token;
    public int money;
    public int[] exp;
    public int[] character;
    public int[] lineup;
    public int tear;
    public int castleLevel;
    public int slingshotLevel;
    public int[] clearance;
    public int energy;
    public string updateTime;
    public int volume;
    public int backVolume;
    public bool shock;
    public bool remind;
    public string chestTime;
    public int faction;
    public int[] props;

    public void CallUpdate() {
        StartCoroutine(updateData());
    }

    public void CallOpenChest(bool type) {

    }
    
    IEnumerator updateData(){

        WWWForm form = new WWWForm();
        form.AddField("token", token);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:5000/updateData", form);
        
        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success){
            string response = www.downloadHandler.text;
            
            data playerData = JsonUtility.FromJson<data>(response);

            Debug.Log(response);
            Debug.Log(JsonUtility.ToJson(playerData));

            success = playerData.success;
            money = playerData.money;
            exp = playerData.exp;
            character = playerData.character;
            lineup = playerData.lineup;
            tear = playerData.tear;
            castleLevel = playerData.castleLevel;
            slingshotLevel = playerData.slingshotLevel;
            clearance = playerData.clearance;
            energy = playerData.energy;
            updateTime = playerData.updateTime;
            volume = playerData.volume;
            backVolume = playerData.backVolume;
            shock = playerData.shock;
            remind = playerData.remind;
            chestTime = playerData.chestTime;
            faction = playerData.faction;
            props = playerData.props;

        }
        else{ //非法token需跳回登入頁面
            //SceneManager.LoadScene("MainScene");
            Debug.Log("Error");
        }
    }
    
    IEnumerator openChest(bool type) {
        WWWForm form = new WWWForm();

        form.AddField("token", token);
        if(type)
            form.AddField("type", "True");
        else
            form.AddField("type", "False");

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:5000/updateData", form);
        
        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success){
            CallUpdate();
        }
        else{ // 非法操作

        }

    }

    

}
