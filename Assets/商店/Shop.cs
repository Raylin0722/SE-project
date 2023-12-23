using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ServerMethod;
using System;
public class Shop : MonoBehaviour
{
    public GameObject Check_Page;
    public Text[] Texts;
    public Text[] Texts_Special;
    public GameObject Special_Bottom;
    [SerializeField] GameObject[] Results;
    [SerializeField] GameObject[] Characters;
    public GameObject MoneyRecharacter;
    public Text MoneyRecharacterNum;
    public Text InfoRecharacter;
    private bool If_Special=false;
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scene
    public GameObject page_Shop; // the page which you want to close
    public GameObject Ordinary_Box_close; // Ordinary Box with closing
    public GameObject Ordinary_Box_open; // Ordinary Box with openning
    public GameObject Special_Box_close; // Special Box with closing
    public GameObject Special_Box_open; // Special Box with openning
    public Image White_Image; // White image
    private float Time_White = 1.0f; // the duration of the picture becomes larger
    public GameObject serverdata;
    [SerializeField] Text money; // money value
    [SerializeField] Text tear; // tear value
    [SerializeField] Text chestRemaid;
    private ServerMethod.Server ServerScript; // Server.cs
    public GameObject hint; // the hint about ont open
    private Coroutine endCoroutine;
    public GameObject Transparent_Background;
    private int OLD_money;

    void Start() {
        White_Image.gameObject.SetActive(false); // Initialization ( close )
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        Conceal_Result();
    }
    void Update() {
        money.text = ServerScript.money.ToString(); //Update money
        tear.text = ServerScript.tear.ToString(); //Update tear
        Update_chest_time();
    }

    // When click < X > 
    public void Button_Close() {
        if(endCoroutine!=null) {
            StopCoroutine(endCoroutine);
            endCoroutine = null;
        }
        page_Shop.SetActive(false);
        ALL_Button.SetActive(true); // Open All button in Main_Scene
    }
    // When click NORMAL < OPEN > 
    public void Button_normal_OPEN() {
        Ordinary_Box_close.gameObject.SetActive(false); // Ordinary Box with closing
        Ordinary_Box_open.gameObject.SetActive(true); // Ordinary Box with openning
        If_Special = false;
        StartCoroutine(Fade_Screen(true)); // the fading animation before the drawing
    }
    // When click SPECIAL < OPEN > 
    public void Button_special_OPEN() {
        If_Special = true;
        Special_Box_close.gameObject.SetActive(false); // Special Box with closing
        Special_Box_open.gameObject.SetActive(true); // Special Box with openning
        StartCoroutine(Fade_Screen(false)); // the fading animation before the drawing
    }
    // the animation of drawing 
    private IEnumerator Fade_Screen(bool openType) {
        Transparent_Background.gameObject.SetActive(true);
        IEnumerator coroutine = serverdata.GetComponent<Server>().openChest(openType);
        yield return StartCoroutine(coroutine);

        chestReturn result = coroutine.Current as chestReturn;

        if(result != null){
            Debug.Log(result.success);
            Debug.Log("0: money 1 : exp 2: taer 3: props 4: normalcharacter 5: rarecharacter \nresult: " + result.result);
            Debug.Log("角色代號 -1表示沒抽到角色: " + result.character);
            Debug.Log("是否抽到角色 若上一個有抽到這邊是false表示已擁有: " + result.get);
            Debug.Log("錯誤狀況(出現-1表示時間未到或淚水不足 出現-2請立即呼叫Raylin 感恩): " + result.situation);
            if(result.success==true) {
                OLD_money = ServerScript.money;
                StartCoroutine(Fade_Screen_new(result.result,result.character));
            }
            else {
                If_Special = false;
                endCoroutine = StartCoroutine(Not_Opne_Hint(1f));
                yield return new WaitForSeconds(1f);
                Conceal_Result();
            }
        }
    }
    private IEnumerator Fade_Screen_new(int result,int character) {
        White_Image.gameObject.SetActive(true);
        float Elapsed_Time = 0f; // the time elpased now
        Color startColor = new Color(1f, 1f, 1f, 0f); // Transparent Color
        Color targetColor = White_Image.color;
        Vector3 Start_Scale = new Vector3(0.1f, 0.1f, 0.1f); // min
        Vector3 End_Scale = new Vector3(1.7f, 1.7f, 1.7f); // max

        while(Elapsed_Time<Time_White) {
            White_Image.color = Color.Lerp(startColor, targetColor, Elapsed_Time / Time_White);
            White_Image.transform.localScale = Vector3.Lerp(Start_Scale, End_Scale, Elapsed_Time / Time_White);
            Elapsed_Time = Elapsed_Time + Time.deltaTime;
        }
        yield return StartCoroutine(Fade_Screen_new_2(result,character));
    }
    private IEnumerator Fade_Screen_new_2(int result,int character) {
        StartCoroutine(Show_Result(result,character));
        yield return new WaitForSeconds(2f);
        Special_Bottom.SetActive(false);
        Conceal_Result();
    }

    public IEnumerator Show_Result(int result,int character_index) {  
        //特殊寶箱
        if(If_Special==true) {
            switch(result) {
                case 0:
                    Results[0].SetActive(true);
                    Texts_Special[0].gameObject.SetActive(true);
                    break;
                case 1:
                    Results[1].SetActive(true);
                    Texts_Special[1].gameObject.SetActive(true);
                    break;
                case 2:
                    Results[2].SetActive(true);
                    Texts_Special[2].gameObject.SetActive(true);
                    break;
                case 3:
                    Results[3].SetActive(true);
                    break;
                case 4:
                    Characters[7*(ServerScript.faction[1]-2)+character_index-1].SetActive(true);
                    if(ServerScript.character[character_index-1]!=0) {
                        MoneyRecharacter.SetActive(true);
                        InfoRecharacter.gameObject.SetActive(true);
                        yield return new WaitForSeconds(1f);
                        InfoRecharacter.gameObject.SetActive(false);
                        MoneyRecharacterNum.gameObject.SetActive(true);
                        MoneyRecharacterNum.text = (ServerScript.money-OLD_money).ToString();
                    }
                    break;
                case 5:
                    Characters[7*(ServerScript.faction[1]-2)+character_index-1].SetActive(true);
                    if(ServerScript.character[character_index-1]!=0) {
                        MoneyRecharacter.SetActive(true);
                        InfoRecharacter.gameObject.SetActive(true);
                        yield return new WaitForSeconds(1f);
                        InfoRecharacter.gameObject.SetActive(false);
                        MoneyRecharacterNum.gameObject.SetActive(true);
                        MoneyRecharacterNum.text = (ServerScript.money-OLD_money).ToString();
                    }
                    break;
            }
        }
        //普通寶箱
        else {
            switch(result) {
                case 0:
                    Results[0].SetActive(true);
                    Texts[0].gameObject.SetActive(true);
                    break;
                case 1:
                    Results[1].SetActive(true);
                    Texts[1].gameObject.SetActive(true);
                    break;
                case 2:
                    Results[2].SetActive(true);
                    Texts[2].gameObject.SetActive(true);
                    break;
                case 3:
                    Results[3].SetActive(true);
                    break;
                case 4:
                    Characters[7*(ServerScript.faction[1]-2)+character_index-1].SetActive(true);
                    if(ServerScript.character[character_index-1]!=0) {
                        MoneyRecharacter.SetActive(true);
                        InfoRecharacter.gameObject.SetActive(true);
                        yield return new WaitForSeconds(1f);
                        InfoRecharacter.gameObject.SetActive(false);
                        MoneyRecharacterNum.gameObject.SetActive(true);
                        MoneyRecharacterNum.text = (ServerScript.money-OLD_money).ToString();
                    }
                    break;
                case 5:
                    Characters[7*(ServerScript.faction[1]-2)+character_index-1].SetActive(true);
                    if(ServerScript.character[character_index-1]!=0) {
                        MoneyRecharacter.SetActive(true);
                        InfoRecharacter.gameObject.SetActive(true);
                        yield return new WaitForSeconds(1f);
                        InfoRecharacter.gameObject.SetActive(false);
                        MoneyRecharacterNum.gameObject.SetActive(true);
                        MoneyRecharacterNum.text = (ServerScript.money-OLD_money).ToString();
                    }
                    break;
            }
        }
    }
    public void Conceal_Result() {
        for(int i=0 ;i<4 ; i++)    Results[i].SetActive(false);
        for(int i=0 ; i<3 ; i++) {
            Texts[i].gameObject.SetActive(false);
            Texts_Special[i].gameObject.SetActive(false);
        }
        for(int i=0 ; i<28 ; i++)   Characters[i].SetActive(false);
        MoneyRecharacter.SetActive(false);
        MoneyRecharacterNum.gameObject.SetActive(false);
        Ordinary_Box_open.gameObject.SetActive(false);
        Special_Box_open.gameObject.SetActive(false);
        Ordinary_Box_close.gameObject.SetActive(true);
        Special_Box_close.gameObject.SetActive(true);
        White_Image.gameObject.SetActive(false);
        If_Special = false;
        Transparent_Background.gameObject.SetActive(false);
    }
    //show Special Button
    public void show_SpecialButton() {
        If_Special=true;
        Special_Bottom.SetActive(true);
    }
    //cancel open
    public void Cancel_Open() {
        Special_Bottom.SetActive(false);
        Check_Page.SetActive(false);
    }
    public void Update_chest_time(){
        DateTime nowTime = DateTime.Now;
        DateTime canOpenTime = DateTime.Parse(ServerScript.chestTime);
        TimeSpan timeDifference = canOpenTime - nowTime;
        
        if(timeDifference.TotalSeconds < 0f){
            chestRemaid.text = "00:00";
        }
        else{
            int days = timeDifference.Days;
            int hours = timeDifference.Hours + 24 * days;
            int minutes = timeDifference.Minutes;
            chestRemaid.text = hours.ToString().PadLeft(2, '0') + ":" + minutes.ToString().PadLeft(2, '0');
        }
    }
    IEnumerator Not_Opne_Hint(float delay) {
        hint.SetActive(false);
        hint.SetActive(true);
        yield return new WaitForSeconds(delay);
        hint.SetActive(false);
    }
}