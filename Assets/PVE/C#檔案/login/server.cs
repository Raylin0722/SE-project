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

    
    IEnumerator updateData(){

        WWWForm form = new WWWForm();
        form.AddField("token", token);

        byte[] formDataBytes = form.data;

        // 将表单数据转换为字符串
        string formDataString = System.Text.Encoding.UTF8.GetString(formDataBytes);

        // 输出表单数据字符串
        Debug.Log(formDataString);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:5000/updateData", form);
        
        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success){
            string response = www.downloadHandler.text;

            Debug.Log(response);

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
        else{
            //SceneManager.LoadScene("MainScene");
        }

        UnityWebRequest aaa = UnityWebRequest.Get("http://localhost:5000/");
        yield return aaa.SendWebRequest();
        if(aaa.result == UnityWebRequest.Result.Success){
            Debug.Log("Susses");
        }

    }
    
   

}
