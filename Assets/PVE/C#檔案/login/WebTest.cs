using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebTest : MonoBehaviour {
    IEnumerator Start() {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:80/sqlconnect/server.py");
        yield return request.SendWebRequest();
        string[] webResults = request.downloadHandler.text.Split('\t');
        Debug.Log(webResults[0]);
        int webNumber = int.Parse(webResults[1]);
        webNumber *= 2;
        Debug.Log(webNumber);
    }
}
