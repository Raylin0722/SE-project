using UnityEngine;
using UnityEngine.UI;
using System;
using ServerMethod;
public class USER : MonoBehaviour{
    public GameObject Change; // Change portrait Button
    public GameObject[] portraits; // The all portraits in USER
    private ServerMethod.Server ServerScript; // Server.cs
    public Image Experience_line;
    void Start() {
        int message = MainMenu.message;
        if(MainMenu.message!=87)   ServerScript = FindObjectOfType<ServerMethod.Server>();
    }
    void Update() {
        if(MainMenu.message==87)    for(int i = 0; i<portraits.Length; i++)    portraits[i].SetActive(i==4);
        else {
            if(ServerScript.faction.Length!=0)   Update_Display();
            int num = 0;
            for(int i = 2; i<6 ; i++)    if(ServerScript.faction[i]==1)      num = num + 1;
            if(num>1)       Change.gameObject.SetActive(true);
            else            Change.gameObject.SetActive(false);
        }
    }
    // When click < Change >
    public void Button_Change() {
        if(MainMenu.message==87) {
            MainMenu.faction[1] = MainMenu.faction[1] + 1;
            if(MainMenu.faction[1]>=6)     MainMenu.faction[1] = 2;
            Experience_line.fillAmount = (float)(MainMenu.exp[1] / (500 * Math.Pow(2.5,MainMenu.exp[0]-1)));
            return;
        }
        for(int i = ServerScript.faction[1] + 1; i<6; i++) {
            if(ServerScript.faction[i]==1) {
                ServerScript.faction[1] = i;
                StartCoroutine(ServerScript.updateFaction(ServerScript.faction[1]));
                Update_Display();
                return;
            }
        }
        for(int i = 2; i<=ServerScript.faction[1]; i++) {
            if(ServerScript.faction[i]==1) {
                ServerScript.faction[1] = i;
                StartCoroutine(ServerScript.updateFaction(ServerScript.faction[1]));
                Update_Display();
                return;
            }
        }
    }
    // Display your portraits
    private void Update_Display() {
        if(ServerScript.faction[0]==1)    return;
        for(int i = 0; i<portraits.Length; i++)     portraits[i].SetActive(i==ServerScript.faction[1]-2); // Show current page && Hide the others
        Experience_line.fillAmount = (float)(ServerScript.exp[1] / (500 * Math.Pow(2.5,ServerScript.exp[0]-1)));
    }
}