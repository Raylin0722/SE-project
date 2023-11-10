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
    public Dictionary<string, string> character;
    public List<int> lineup;
    public int tear;
    public int castlelevel;
    public int slingshotlevel;
    public Dictionary<string, string> clearance;
    public Dictionary<string, string>energy;
    public Dictionary<string, string> setting;
    public string chesttime;
}


public class server : MonoBehaviour
{
    public bool success;
    public string token;
    public int money;
    public int[] exp;
    public Dictionary<string, string> character;
    public List<int> lineup;
    public int tear;
    public int castlelevel;
    public int slingshotlevel;
    public Dictionary<string, string> clearance;
    public Dictionary<string, string>energy;
    public Dictionary<string, string> setting;
    public string chesttime;

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

            success = playerData.success;
            money = playerData.money;
            exp = playerData.exp;
            character = playerData.character;
            lineup = playerData.lineup;
            tear = playerData.tear;
            castlelevel = playerData.castlelevel;
            slingshotlevel = playerData.slingshotlevel;
            clearance = playerData.clearance;
            energy = playerData.energy;
            setting = playerData.setting;
            chesttime = playerData.chesttime;

        }
        else{ //非法token需跳回登入頁面
            //SceneManager.LoadScene("MainScene");
        }


    }
    
    IEnumerator openChest(bool type) {
        WWWForm form = new WWWForm();

        form.AddField("token", token);
        form.AddField("type", type);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:5000/updateData", form);
        
        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success){
            string response = www.downloadHandler.text;
            
        }
        else{ // 非法操作

        }

    }

}
