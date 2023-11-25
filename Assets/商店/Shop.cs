using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using ServerMethod;

public class Shop : MonoBehaviour
{
    //蔡松豪加的
    public GameObject Check_Page;
    public Text[] Texts;
    public Text[] Texts_Special;
    public GameObject Special_Bottom;
    [SerializeField] GameObject[] Results;

    private bool If_Special=false;
    //
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scene
    public GameObject Close; // Close Button
    public GameObject page_Shop; // the page which you want to close
    public GameObject Ordinary_Open; // Ordinary Open Button
    public GameObject Special_Open; // Special Open Button
    public Image White_Image; // White image
    private float Time_White = 1.0f; // the duration of the picture becomes larger
    public GameObject serverdata;
    [SerializeField] Text money; // money value
    [SerializeField] Text tear; // tear value
    private ServerMethod.Server ServerScript; // Server.cs

    // Start is called before the first frame update
    void Start()
    {
        White_Image.gameObject.SetActive(false); // Initialization ( close )
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        Conceal_Result();
    }

    // Update is called once per frame
    void Update()
    {
        Update_values(); // Update money && tear
    }

    // When click < X > 
    public void Button_Close()
    {
        page_Shop.SetActive(false);
        ALL_Button.SetActive(true); // Open All button in Main_Scene
    }

    // When click NORMAL < OPEN > 
    public void Button_normal_OPEN()
    {
        White_Image.gameObject.SetActive(true);
        // You can write them separately or according to parameters
        StartCoroutine(Fade_Screen(true)); // the fading animation before the drawing
    }

    // When click SPECIAL < OPEN > 
    public void Button_special_OPEN()
    {
        Debug.Log("我開了");
        White_Image.gameObject.SetActive(true);
        // You can write them separately or according to parameters
        StartCoroutine(Fade_Screen(false)); // the fading animation before the drawing
    }

    // the animation of drawing 
    private IEnumerator Fade_Screen(bool openType)
    {
        float Elapsed_Time = 0f; // the time elpased now
        Color startColor = new Color(1f, 1f, 1f, 0f); // Transparent Color
        Color targetColor = White_Image.color;
        Vector3 Start_Scale = new Vector3(0.1f, 0.1f, 0.1f); // min
        Vector3 End_Scale = new Vector3(1.7f, 1.7f, 1.7f); // max

        while(Elapsed_Time<Time_White)
        {
            White_Image.color = Color.Lerp(startColor, targetColor, Elapsed_Time / Time_White);
            White_Image.transform.localScale = Vector3.Lerp(Start_Scale, End_Scale, Elapsed_Time / Time_White);
            Elapsed_Time = Elapsed_Time + Time.deltaTime;
            yield return null;
        }

        IEnumerator coroutine = serverdata.GetComponent<Server>().openChest(openType);
        yield return StartCoroutine(coroutine);

        chestReturn result = coroutine.Current as chestReturn;

        if(result != null){
            //Debug.Log(result);
            Debug.Log(result.success);
            Debug.Log("0: money 1 : exp 2: taer 3: props 4: normalcharacter 5: rarecharacter \nresult: " + result.result);
            Debug.Log("角色代號 -1表示沒抽到角色: " + result.character);
            Debug.Log("是否抽到角色 若上一個有抽到這邊是false表示已擁有: " + result.get);
            Debug.Log("錯誤狀況(出現-1表示時間未到或淚水不足 出現-2請立即呼叫Raylin 感恩): " + result.situation);
        }
        //顯示抽獎
        Show_Result(result.result);
        yield return new WaitForSeconds(2f);
        Conceal_Result();
        Special_Bottom.SetActive(false);
        If_Special=false;
        //
        White_Image.gameObject.SetActive(false);
    }
    public void Show_Result(int result)
    {  
        if(If_Special==true)
        {
            switch(result)
            {
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
            }
        }
        else
        {
            switch(result)
            {
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
            }
        }
    }
    public void Conceal_Result()
    {
        for(int i=0 ;i<4 ; i++)
        {
            Results[i].SetActive(false);
        }
        for(int i=0 ; i<3 ; i++)
        {
            Texts[i].gameObject.SetActive(false);
            Texts_Special[i].gameObject.SetActive(false);
        }
    }

    // Update energy && money && tear
    public void Update_values()
    {
        money.text = ServerScript.money.ToString();
        tear.text = ServerScript.tear.ToString();
    }
    //Show Check Page  
    public void Show_CheckPage()
    {
        Check_Page.SetActive(true);
    }
    //show Special Button
    public void show_SpecialButton()
    {
        If_Special=true;
        Special_Bottom.SetActive(true);
    }
    //cancel open
    public void Cancel_Open()
    {
        Check_Page.SetActive(false);
    }
}
