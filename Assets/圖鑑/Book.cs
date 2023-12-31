using UnityEngine;
using UnityEngine.UI;
using System.Threading;
public class Book : MonoBehaviour{
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scen
    public GameObject page_Book; // the page which you want to close
    public GameObject[] pages; // The all page in Book
    public Image[] Pictures; // The all pictures in Book
    private int Current = 0; // The current position about your location in Book
    private ServerMethod.Server ServerScript; // Server.cs
    void Start() {
        if(MainMenu.message!=87)    ServerScript = FindObjectOfType<ServerMethod.Server>();
    }
    // Update is called once per frame
    void Update() {
        if(MainMenu.message==87) {
            ShowPage(Current);
            return;
        }    
        if(ServerScript.rankClear.Count!=0)     ShowPage(Current);
    }
    // When click < < >
    public void Button_Last() {
        if(Current>0) {
            Current = Current - 1;
            ShowPage(Current);
        }
    }
    // When click < BACK >
    public void Button_Back() {
        Current=0;
        ShowPage(0);
        page_Book.SetActive(false);
        ALL_Button.SetActive(true); // Open All button in Main_Scene
    }
    // When click < > >
    public void Button_Next() {
        if(Current<7) {
            Current = Current + 1;
            if(Current>=7)  Current = 6;
            ShowPage(Current);
        }
    }
    private void ShowPage(int Current) {
        // Show current page && Hide the others
        for(int i = 0; i<7; i++) {
            for(int j = 0; j<4 ; j++)   Pictures[7*j+i].gameObject.SetActive(false);
            if(i==Current) {
                pages[i].SetActive(true);
                pages[7].SetActive(false); // close empty page
                pages[8].SetActive(false); // close chain
                if(MainMenu.message==87) {
                    Pictures[7*(MainMenu.faction[1]-1-1)+i].gameObject.SetActive(true);
                    Pictures[7*(MainMenu.faction[1]-1-1)+i].material.color = new Color(1f,1f,1f,1f);
                }else {
                    Pictures[7*(ServerScript.faction[1]-1-1)+i].gameObject.SetActive(true);
                    Pictures[7*(ServerScript.faction[1]-1-1)+i].material.color = new Color(1f,1f,1f,1f);
                    if(ServerScript.character[i]==0) {
                        pages[7].SetActive(true); // open empty page
                        pages[8].SetActive(true); // open chain
                        Pictures[7*(ServerScript.faction[1]-1-1)+i].color = new Color(0f,0f,0f,1f);
                    }
                }
            }else pages[i].SetActive(false);
        }
    }
}