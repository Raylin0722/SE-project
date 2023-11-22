using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public GameObject main_scene; // The all gameobject
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scene
    public GameObject User; // Player information in the upper left corner
    public GameObject Ranking_list; // Ranking list
    public GameObject page_Ranking_list; // the page for Ranking list
    public GameObject Shop; // Shop
    public GameObject page_Shop; // the page for Shop
    public GameObject Book; // Book
    public GameObject page_Book; // the page for Book
    public GameObject Level_up; // Prerpare for the war
    public GameObject page_Level_up; // the page for Level up
    public GameObject Starts; // Select level to PVE
    public GameObject page_Start; // the page for PVE
    public GameObject Setting; // Settings
    public GameObject page_Setting; // the page for Settings
    public GameObject Friends; // Friends
    public GameObject page_Friends; // the page for Friends
    public GameObject Top_up; // Top up(page or website?)
    [SerializeField] Text energy; // energy value
    [SerializeField] Text money; // money value
    [SerializeField] Text tear; // tear value
    public AudioSource Music_Main_Scene; // the Music in Main Scene
    private ServerMethod.Server ServerScript; // Server.cs


    private void Start()
    {
        ALL_Button.SetActive(true);
        page_Ranking_list.SetActive(false);
        page_Shop.SetActive(false);
        page_Book.SetActive(false);
        page_Level_up.SetActive(false);
        page_Setting.SetActive(false);
        page_Friends.SetActive(false);
        page_Start.SetActive(false);
        Play_Music();
        ServerScript = FindObjectOfType<ServerMethod.Server>();
    }

    // Update is called once per frame
    void Update()
    {
        Update_values(); // Update energy && money && tear
    }


    // Click < Ranking_list > 
    public void Button_Ranking_list()
    {
        page_Ranking_list.SetActive(true);
        //ALL_Button.SetActive(false); // Close All button in Main_Scene
    }

    // Click < Shop > 
    public void Button_Shop()
    {
        page_Shop.SetActive(true);
        ALL_Button.SetActive(false); // Close All button in Main_Scene
    }

    // Click < Book > 
    public void Button_Book()
    {
        page_Book.SetActive(true);
        ALL_Button.SetActive(false); // Close All button in Main_Scene
    }

    // Click < Level_up > 
    public void Button_Level_up()
    {
        page_Level_up.SetActive(true);
        ALL_Button.SetActive(false); // Close All button in Main_Scene
    }

    // Click < Start > 
    public void Button_Start()
    {
        page_Start.SetActive(true);
        ALL_Button.SetActive(false); // Close All button in Main_Scene
    }

    // Click < Settings > 
    public void Button_Setting()
    {
        page_Setting.SetActive(true);
    }

    // Click < Friends > 
    public void Button_Friends()
    {
        page_Friends.SetActive(true);
    }

    // Update energy && money && tear
    public void Update_values()
    {
        energy.text = ServerScript.energy.ToString() + "/30";
        money.text = ServerScript.money.ToString();
        tear.text = ServerScript.tear.ToString();
    }

    // Play Music
    private void Play_Music()
    {
        Music_Main_Scene.Play();
    }

    // Stop Music
    private void Stop_Music()
    {
        Music_Main_Scene.Stop();
    }
}