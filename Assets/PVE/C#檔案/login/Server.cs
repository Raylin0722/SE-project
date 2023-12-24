using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;
namespace ServerMethod{
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
        public int remainTime;
        public string updateTime;
        public int volume;
        public int backVolume;
        public bool shock;
        public bool remind;
        public string chestTime;
        public int[] faction;
        public int[] props;
    }
    public class chestReturn{
        public bool success;
        public int result;
        public int situation;
        public bool get;
        public int character;
    }
    public class Rank{
        public List<string> RankName;
        public List<string> RankClear;
        public List<int> Faction;

    }
    
    public class Return{
        public bool success;
    }


    public class Server : MonoBehaviour{
        public string username;
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
        public int remainTime;
        public string updateTime;
        public int volume;
        public int backVolume;
        public bool shock;
        public bool remind;
        public string chestTime;
        public int[] faction;
        public int[] props;
        public List<string> rankName = new List<string>();
        public List<string> rankClear = new List<string>();
        public List<int> rankFaction = new List<int>();
        [SerializeField] GameObject NetWorkWarning;
        void Awake(){
            if(MainMenu.message==87)    return;
            token = TokenManager.Instance.Token;
            username = TokenManager.Instance.Username;
            CallUpdateUserData();
            GameObject serverObj = GameObject.Find("Server");
            if(serverObj != null){
                DontDestroyOnLoad(serverObj);
                serverObj.tag = "DontDestroy";
            }
        }
        private void Start() {
            if(MainMenu.message==87)    return;
            StartCoroutine(autoUpdate());
        }
        public void CallUpdateUserData() {
            StartCoroutine(updateData());
        }
        public IEnumerator updateData(){
            WWWForm form = new WWWForm();
            form.AddField("token", token);
            UnityWebRequest www = UnityWebRequest.Post("https://pc167.csie.ntnu.edu.tw/updateData", form);
            www.timeout = 5;
            yield return www.SendWebRequest();
            if (!(www.result == UnityWebRequest.Result.ConnectionError) && !(www.result == UnityWebRequest.Result.ProtocolError)){
                string response = www.downloadHandler.text;
                data playerData = JsonUtility.FromJson<data>(response);
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
                remainTime = playerData.remainTime;
                updateTime = playerData.updateTime;
                volume = playerData.volume;
                backVolume = playerData.backVolume;
                shock = playerData.shock;
                remind = playerData.remind;
                chestTime = playerData.chestTime;
                faction = playerData.faction;
                props = playerData.props;
            }else{ //非法token需跳回登入頁面
                NetWorkWarning.SetActive(true);
                yield return new WaitForSeconds(3f);
                print("out of time!");
                GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroy");
                foreach (GameObject obj in dontDestroyObjects)Destroy(obj);
                SceneManager.LoadScene("MainMenu");
                Debug.Log("未連接至伺服器");
            }
            if(success == false){
                GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroy");
                foreach (GameObject obj in dontDestroyObjects)Destroy(obj);
                SceneManager.LoadScene("MainMenu");
                Debug.Log("Error2");
            }
            StartCoroutine(updateRank());
        }     
        public IEnumerator autoUpdate(){
            while (true){
                yield return new WaitForSecondsRealtime(300f);
                yield return StartCoroutine(updateData());
            }
        }
        public IEnumerator openChest(bool openType) {
            WWWForm form = new WWWForm();
            form.AddField("token", token);
            if(openType)form.AddField("openType", "True");
            else form.AddField("openType", "False");
            UnityWebRequest www = UnityWebRequest.Post("https://pc167.csie.ntnu.edu.tw/openChest", form);
            www.timeout = 5;
            yield return www.SendWebRequest();
            chestReturn result = new chestReturn();
            if (!(www.result == UnityWebRequest.Result.ConnectionError) && !(www.result == UnityWebRequest.Result.ProtocolError)){
                string response = www.downloadHandler.text;
                result = JsonUtility.FromJson<chestReturn>(response);
                CallUpdateUserData();
            }else{
                NetWorkWarning.SetActive(true);
                yield return new WaitForSeconds(3f);
                GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroy");
                foreach (GameObject obj in dontDestroyObjects)Destroy(obj);
                SceneManager.LoadScene("MainMenu");
                Debug.Log("未連接至伺服器");
            }
            yield return result;
        }
        public IEnumerator beforeGame(){
            WWWForm form = new WWWForm();
            form.AddField("token", token);
            UnityWebRequest www = UnityWebRequest.Post("https://pc167.csie.ntnu.edu.tw/beforeGame", form);
            www.timeout = 5;
            yield return www.SendWebRequest();
            Return result = new Return();
            if (!(www.result == UnityWebRequest.Result.ConnectionError) && !(www.result == UnityWebRequest.Result.ProtocolError)){
                string response = www.downloadHandler.text;
                result = JsonUtility.FromJson<Return>(response);
                CallUpdateUserData();
            }else{
                NetWorkWarning.SetActive(true);
                yield return new WaitForSeconds(3f);
                GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroy");
                foreach (GameObject obj in dontDestroyObjects)Destroy(obj);
                SceneManager.LoadScene("MainMenu");
                Debug.Log("未連接至伺服器");
            }
            yield return result;
        }
        public IEnumerator afterGame(bool clear, string target){
            WWWForm form = new WWWForm();
            form.AddField("token", token);
            form.AddField("target", target);
            if(clear)form.AddField("clear", "True");
            else form.AddField("clear", "False");
            Return result = new Return();
            UnityWebRequest www = UnityWebRequest.Post("https://pc167.csie.ntnu.edu.tw/afterGame", form);
            www.timeout = 5;
            yield return www.SendWebRequest();
            if (!(www.result == UnityWebRequest.Result.ConnectionError) && !(www.result == UnityWebRequest.Result.ProtocolError)){
                string response = www.downloadHandler.text;
                result = JsonUtility.FromJson<Return>(response);
                CallUpdateUserData();
            }else{
                NetWorkWarning.SetActive(true);
                yield return new WaitForSeconds(3f);
                GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroy");
                foreach (GameObject obj in dontDestroyObjects)Destroy(obj);
                SceneManager.LoadScene("MainMenu");
                Debug.Log("未連接至伺服器");
            }
            yield return result;
        }
        public IEnumerator updateCard(int target, int mode){
            WWWForm form = new WWWForm();
            form.AddField("token", token);
            form.AddField("target", target);
            form.AddField("mode", mode);
            UnityWebRequest www = UnityWebRequest.Post("https://pc167.csie.ntnu.edu.tw/updateCard", form);
            www.timeout = 5;
            yield return www.SendWebRequest();
            Return result = new Return();
            if (!(www.result == UnityWebRequest.Result.ConnectionError) && !(www.result == UnityWebRequest.Result.ProtocolError)){
                string response = www.downloadHandler.text;
                result = JsonUtility.FromJson<Return>(response);
                CallUpdateUserData();
            }else{
                NetWorkWarning.SetActive(true);
                yield return new WaitForSeconds(3f);
                GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroy");
                foreach (GameObject obj in dontDestroyObjects)Destroy(obj);
                SceneManager.LoadScene("MainMenu");
                Debug.Log("未連接至伺服器");
            }
            yield return result;
        }
        public IEnumerator updateLineup(int[] lineup){
            WWWForm form = new WWWForm();
            string lineupString = string.Join(",", lineup);
            lineupString = "["+lineupString+"]";
            form.AddField("lineup", lineupString);
            form.AddField("token", token);
            print(lineupString);
            UnityWebRequest www = UnityWebRequest.Post("https://pc167.csie.ntnu.edu.tw/updateLineup", form);
            www.timeout = 5;
            yield return www.SendWebRequest();
            Return result = new Return();
            if (!(www.result == UnityWebRequest.Result.ConnectionError) && !(www.result == UnityWebRequest.Result.ProtocolError)){
                string response = www.downloadHandler.text;
                result = JsonUtility.FromJson<Return>(response);
                CallUpdateUserData();    
            }else{
                NetWorkWarning.SetActive(true);
                yield return new WaitForSeconds(3f);
                GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroy");
                foreach (GameObject obj in dontDestroyObjects)Destroy(obj);
                SceneManager.LoadScene("MainMenu");
                Debug.Log("未連接至伺服器");
            }
            yield return result;
        }   
        public IEnumerator updateRank(){
            WWWForm form = new WWWForm();
            UnityWebRequest www = UnityWebRequest.Post("https://pc167.csie.ntnu.edu.tw/updateRank", form);
            www.timeout = 5;
            yield return www.SendWebRequest();
            if (!(www.result == UnityWebRequest.Result.ConnectionError) && !(www.result == UnityWebRequest.Result.ProtocolError)){
                string response = www.downloadHandler.text;
                rankName = new List<string>();
                rankClear = new List<string>();
                rankFaction = new List<int>();
                Rank rankReturn = JsonUtility.FromJson<Rank>(response);
                foreach(string name in rankReturn.RankName)rankName.Add(name);   
                foreach(string clear in rankReturn.RankClear)rankClear.Add(clear);
                foreach(int fac in rankReturn.Faction)rankFaction.Add(fac);
            }else{
                NetWorkWarning.SetActive(true);
                yield return new WaitForSeconds(3f);
                GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroy");
                foreach (GameObject obj in dontDestroyObjects)Destroy(obj);
                SceneManager.LoadScene("MainMenu");
                Debug.Log("未連接至伺服器");
            }
        }
        public IEnumerator updateFaction(int target){
            WWWForm form = new WWWForm();
            form.AddField("target", target);
            form.AddField("token", token);
            UnityWebRequest www = UnityWebRequest.Post("https://pc167.csie.ntnu.edu.tw/updateFaction", form);
            www.timeout = 5;
            yield return www.SendWebRequest();
            Return result = new Return();
            if (!(www.result == UnityWebRequest.Result.ConnectionError) && !(www.result == UnityWebRequest.Result.ProtocolError)){
                string response = www.downloadHandler.text;
                result = JsonUtility.FromJson<Return>(response);
                CallUpdateUserData();
            }else{
                NetWorkWarning.SetActive(true);
                yield return new WaitForSeconds(3f);
                GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroy");
                foreach (GameObject obj in dontDestroyObjects)Destroy(obj);   
                SceneManager.LoadScene("MainMenu");
                Debug.Log("未連接至伺服器");
            }
            yield return result;
        }
        public IEnumerator initFaction(int target){
            WWWForm form = new WWWForm();
            form.AddField("target", target);
            form.AddField("token", token);
            UnityWebRequest www = UnityWebRequest.Post("https://pc167.csie.ntnu.edu.tw/initFaction", form);
            www.timeout = 5;
            yield return www.SendWebRequest();
            Return result = new Return();
            if (!(www.result == UnityWebRequest.Result.ConnectionError) && !(www.result == UnityWebRequest.Result.ProtocolError)){
                string response = www.downloadHandler.text;
                result = JsonUtility.FromJson<Return>(response);
                CallUpdateUserData();
            }else{
                NetWorkWarning.SetActive(true);
                yield return new WaitForSeconds(3f);
                GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroy");
                foreach (GameObject obj in dontDestroyObjects)Destroy(obj);
                SceneManager.LoadScene("MainMenu");
                Debug.Log("未連接至伺服器");
            }
            yield return result;
        }
        public IEnumerator topUp(int cardID){ // cardID: 0 不能用 1 可以用
            WWWForm form = new WWWForm();
            form.AddField("token", token);
            form.AddField("cardID", cardID);
            UnityWebRequest www = UnityWebRequest.Post("https://pc167.csie.ntnu.edu.tw/topUp", form);
            www.timeout = 5;
            Return result = new Return();
            yield return www.SendWebRequest();
            if (!(www.result == UnityWebRequest.Result.ConnectionError) && !(www.result == UnityWebRequest.Result.ProtocolError)){
                string response = www.downloadHandler.text;
                result = JsonUtility.FromJson<Return>(response);
                CallUpdateUserData();
            }else{
                NetWorkWarning.SetActive(true);
                yield return new WaitForSeconds(3f);
                GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroy");
                foreach (GameObject obj in dontDestroyObjects)Destroy(obj);   
                SceneManager.LoadScene("MainMenu");
                Debug.Log("未連接至伺服器");
            }
            yield return result;
        }
        public IEnumerator setting(int volume, int backVolume, bool shock){
            WWWForm form = new WWWForm();
            form.AddField("token", token);
            form.AddField("volume", volume);
            form.AddField("backVolume", backVolume);
            if(shock == true)form.AddField("shock", "True");
            else form.AddField("shock", "False");
            UnityWebRequest www = UnityWebRequest.Post("https://pc167.csie.ntnu.edu.tw/setting", form);
            www.timeout = 5;
            Return result = new Return();
            yield return www.SendWebRequest();
            if (!(www.result == UnityWebRequest.Result.ConnectionError) && !(www.result == UnityWebRequest.Result.ProtocolError)){
                string response = www.downloadHandler.text;
                result = JsonUtility.FromJson<Return>(response);
                CallUpdateUserData();
            }else{
                NetWorkWarning.SetActive(true);
                yield return new WaitForSeconds(3f);
                GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroy");
                foreach (GameObject obj in dontDestroyObjects)Destroy(obj);
                SceneManager.LoadScene("MainMenu");
                Debug.Log("未連接至伺服器");
            }
            if(result.success == true)Debug.Log("Success!");
            else Debug.Log("Failed!");
            yield return result;
        }
        public void LogOut() {
            GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroy");
            foreach (GameObject obj in dontDestroyObjects)Destroy(obj);
            SceneManager.LoadScene("MainMenu");
        }
    }
}