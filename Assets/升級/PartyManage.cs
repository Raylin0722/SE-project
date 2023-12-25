using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PartyManage : MonoBehaviour{
    public Canvas canvas;
    public Image[] Pictures; // The all pictures in under level_up
    public Image[] Up_Pictures; // The all pictures in up level_up
    private struct Location{
        public Vector2 position;
        public float offset;
    }
    private List<Location> Location_List = new List<Location>();
    private ServerMethod.Server ServerScript; // Server.cs
    void Start() {
        if(MainMenu.message!=87)    ServerScript = FindObjectOfType<ServerMethod.Server>();
        Character_Location();
    }
    void Update() {
        // Show current page && Hide the others
        for(int i = 0; i<8; i++) {
            for(int j = 0; j<4 ; j++)    Up_Pictures[8*j+i].gameObject.SetActive(false);
            if(i==0) {
                if(MainMenu.message==87)    Up_Pictures[8*(MainMenu.faction[1]-2)+i].gameObject.SetActive(true);
                else if(MainMenu.message!=87)    Up_Pictures[8*(ServerScript.faction[1]-2)+i].gameObject.SetActive(true);
            }else {
                Up_Pictures[32+i-1].gameObject.SetActive(false); // Close chain
                if(MainMenu.message==87) {
                    Up_Pictures[8*(MainMenu.faction[1]-2)+i].gameObject.SetActive(true);
                    Up_Pictures[8*(MainMenu.faction[1]-2)+i].color = new Color(1f,1f,1f,1f);
                    continue;
                }
                Up_Pictures[8*(ServerScript.faction[1]-2)+i].gameObject.SetActive(true);
                Up_Pictures[8*(ServerScript.faction[1]-2)+i].color = new Color(1f,1f,1f,1f);
                if(ServerScript.character[i-1]==0) {
                    Up_Pictures[32+i-1].gameObject.SetActive(true); // Open chain
                    Up_Pictures[8*(ServerScript.faction[1]-2)+i].color = new Color(0f,0f,0f,1f);
                }
            }
        }
        for(int i = 0; i<7; i++)    for(int j = 0; j<4 ; j++)   Pictures[7*j+i].gameObject.SetActive(false);
        for(int i = 0; i<5 ; i++) {
            if(MainMenu.message==87) {
                Pictures[7*(MainMenu.faction[1]-2)+MainMenu.lineup[i]-1].gameObject.SetActive(true);
                Pictures[7*(MainMenu.faction[1]-2)+MainMenu.lineup[i]-1].rectTransform.anchoredPosition = new Vector2(Location_List[7*(MainMenu.faction[1]-2)+MainMenu.lineup[i]-1].position.x+i*Location_List[7*(MainMenu.faction[1]-2)+MainMenu.lineup[i]-1].offset,Location_List[7*(MainMenu.faction[1]-2)+MainMenu.lineup[i]-1].position.y);
                continue;
            }
            Pictures[7*(ServerScript.faction[1]-2)+ServerScript.lineup[i]-1].gameObject.SetActive(true);
            Pictures[7*(ServerScript.faction[1]-2)+ServerScript.lineup[i]-1].rectTransform.anchoredPosition = new Vector2(Location_List[7*(ServerScript.faction[1]-2)+ServerScript.lineup[i]-1].position.x+i*Location_List[7*(ServerScript.faction[1]-2)+ServerScript.lineup[i]-1].offset,Location_List[7*(ServerScript.faction[1]-2)+ServerScript.lineup[i]-1].position.y);
        }
    }
    // Character Display Definition
    private void Character_Location(){
        Location W1 = new Location {position = new Vector2(-672f,-280f),offset = 270.0f};
        Location_List.Add(W1);
        Location W2 = new Location {position = new Vector2(-672f,-281f),offset = 270.0f};
        Location_List.Add(W2);
        Location W3 = new Location {position = new Vector2(-673f,-285f),offset = 270.0f};
        Location_List.Add(W3);
        Location W4 = new Location {position = new Vector2(-671f,-284f),offset = 270.0f};
        Location_List.Add(W4);
        Location W5 = new Location {position = new Vector2(-670f,-288f),offset = 270.0f};
        Location_List.Add(W5);
        Location W6 = new Location {position = new Vector2(-673f,-301f),offset = 270.0f};
        Location_List.Add(W6);
        Location W7 = new Location {position = new Vector2(-666f,-282.8f),offset = 270.0f};
        Location_List.Add(W7);
        Location B1 = new Location {position = new Vector2(-672f,-280f),offset = 270.0f};
        Location_List.Add(B1);
        Location B2 = new Location {position = new Vector2(-673f,-284f),offset = 270.0f};
        Location_List.Add(B2);
        Location B3 = new Location {position = new Vector2(-673f,-285f),offset = 270.0f};
        Location_List.Add(B3);
        Location B4 = new Location {position = new Vector2(-672f,-297.195f),offset = 270.0f};
        Location_List.Add(B4);
        Location B5 = new Location {position = new Vector2(-673f,-274.385f),offset = 270.0f};
        Location_List.Add(B5);
        Location B6 = new Location {position = new Vector2(-665.8f,-296f),offset = 270.0f};
        Location_List.Add(B6);
        Location B7 = new Location {position = new Vector2(-670f,-281.9f),offset = 270.0f};
        Location_List.Add(B7);
        Location S1 = new Location {position = new Vector2(-671.5f,-274.5f),offset = 270.0f};
        Location_List.Add(S1);
        Location S2 = new Location {position = new Vector2(-673.3f,-280.5f),offset = 270.0f};
        Location_List.Add(S2);
        Location S3 = new Location {position = new Vector2(-673f,-296f),offset = 270.0f};
        Location_List.Add(S3);
        Location S4 = new Location {position = new Vector2(-674.7f,-284.6f),offset = 270.0f};
        Location_List.Add(S4);
        Location S5 = new Location {position = new Vector2(-673.1f,-269.3f),offset = 270.0f};
        Location_List.Add(S5);
        Location S6 = new Location {position = new Vector2(-673f,-299f),offset = 270.0f};
        Location_List.Add(S6);
        Location S7 = new Location {position = new Vector2(-681.6f,-268.3f),offset = 270.0f};
        Location_List.Add(S7);
        Location M1 = new Location {position = new Vector2(-672f,-270f),offset = 270.0f};
        Location_List.Add(M1);
        Location M2 = new Location {position = new Vector2(-680.8f,-275.4f),offset = 270.0f};
        Location_List.Add(M2);
        Location M3 = new Location {position = new Vector2(-673f,-281.9f),offset = 270.0f};
        Location_List.Add(M3);
        Location M4 = new Location {position = new Vector2(-651.8f,-281.8f),offset = 270.0f};
        Location_List.Add(M4);
        Location M5 = new Location {position = new Vector2(-665.5f,-281.9f),offset = 270.0f};
        Location_List.Add(M5);
        Location M6 = new Location {position = new Vector2(-673f,-301f),offset = 270.0f};
        Location_List.Add(M6);
        Location M7 = new Location {position = new Vector2(-664.5f,-286f),offset = 270.0f};
        Location_List.Add(M7);
    }
}