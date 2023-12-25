using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ServerMethod;
public class Level_up : MonoBehaviour{
    private float[] distances;
    public GameObject MidPoint;
    public ScrollRect scrollRect;
    public GameObject page_Check_upGrade;
    private int UpgradeIndex;
    private RectTransform contentRect;
    //
    public GameObject MaxGradeFigure;
    public GameObject ChangePropButtom;
    public GameObject[] props;
    public Image[] Bombs;
    public Text Bomb_number;
    //
    public GameObject ALL_Button; // ALL Button in Canvas of Main_Scene
    public GameObject Back; // Close Button
    public GameObject page_Level_up; // the page which you want to close
    public GameObject Upgrade; // Upgrade Button
    [SerializeField] Text money; // money value
    public Text[] Level; // The all level of pictures in ScrollView
    public Text[] Dollar; // The all dollar of pictures in ScrollView
    private int[] Money = new int[] {1200,400,300,700,600,500,650,750};   // the Money for upper charactor and tower
    private ServerMethod.Server ServerScript; // Server.cs
    void Start() {
        if(MainMenu.message!=87)    ServerScript = FindObjectOfType<ServerMethod.Server>();
        contentRect = scrollRect.content;
        int itemCount = contentRect.childCount;
        distances = new float[itemCount];
    }
    void Update() {
        Update_values(); // Update money and charactor level and money and bombs
        CalculateDistances();
        UpgradeIndex = FindNearestCenter();
    }
    // When click < BACK >
    public void Button_Back() {   
        page_Level_up.SetActive(false);
        ALL_Button.SetActive(true); // Open All button in Main_Scene
    }
    //when click <sure upgrade>
    public void Sure_Upgrade(){
        if(UpgradeIndex==0) {
            if(MainMenu.message==87) {
                if(MainMenu.castleLevel==15) {
                    StartCoroutine(MaxGrade());
                    return;
                }
            }else if(ServerScript.castleLevel==15) {
                StartCoroutine(MaxGrade());
                return;
            }
        }else if(UpgradeIndex!=0) {
            if(MainMenu.message==87) {
                if(MainMenu.character[UpgradeIndex-1]==5) {
                    StartCoroutine(MaxGrade());
                    return;
                }
            }else if(ServerScript.character[UpgradeIndex-1]==5) {
                StartCoroutine(MaxGrade());
                return;
            }
        }
        if(MainMenu.message==87) {
            if(UpgradeIndex==0) {
                MainMenu.castleLevel = MainMenu.castleLevel + 1;
                MainMenu.slingshotLevel = MainMenu.slingshotLevel + 1;
            }else MainMenu.character[UpgradeIndex-1] = MainMenu.character[UpgradeIndex-1] + 1;
            return;
        }   
        StartCoroutine(Upgrade_Surver(UpgradeIndex)); //算出來在更新到sever
    }
    IEnumerator MaxGrade() {
        MaxGradeFigure.SetActive(true);
        yield return new WaitForSeconds(1);
        MaxGradeFigure.gameObject.SetActive(false);
    }
    //Send the data to server
    private IEnumerator Upgrade_Surver(int UpgradeIndex) {
        if(UpgradeIndex!=0) {
            IEnumerator coroutine = ServerScript.updateCard(UpgradeIndex,0);
            yield return StartCoroutine(coroutine);
            Return result = coroutine.Current as Return;
            Debug.Log(result.success);
        }else {
            IEnumerator coroutine = ServerScript.updateCard(UpgradeIndex,1);
            yield return StartCoroutine(coroutine);
            Return result = coroutine.Current as Return;
            Debug.Log(result.success);
        }
    }
    // Update energy && money && tear
    public void Update_values(){
        money.text = MainMenu.money.ToString();
        int castleLevel = MainMenu.castleLevel;
        int[] character = MainMenu.character;
        int[] Props = MainMenu.props;
        if(MainMenu.message!=87) {  
            money.text = ServerScript.money.ToString();
            castleLevel = ServerScript.castleLevel;
            character = ServerScript.character;
            Props = ServerScript.props;
        }
        if(castleLevel>=15) {
            Level[0].fontSize = 15;
            Level[0].text = "MAX";
            Dollar[0].text = "-";
        }else {
            Level[0].text = castleLevel.ToString();
            Dollar[0].text = (Money[0]+500*(castleLevel-1)).ToString();
        }
        for(int i = 0; i<character.Length; i++) {
            Level[i+1].fontSize = 25;
            Level[i+1].text = character[i].ToString();
            Level[i+1].color = new Color(0f,0f,0f,1f);
            Dollar[i+1].color = new Color(0f,0f,0f,1f);
            if(character[i]==0) {
                Level[i+1].color = new Color(0f,0f,0f,0f);
                Dollar[i+1].color = new Color(0f,0f,0f,0f);
            }
            else if(character[i]==1)    Dollar[i+1].text = Money[i+1].ToString();
            else if(character[i]>=5) {
                Level[i+1].fontSize = 15;
                Level[i+1].text = "MAX";
                Dollar[i+1].text = "-";
            }
            else    Dollar[i+1].text = (Money[i+1]*1.5*(character[i]-1)).ToString();
        }
        // whether you have Bombs can use
        if(Props[1]<=0) {
            Bombs[0].color = new Color(0f,0f,0f,1f);
            Bombs[1].gameObject.SetActive(false);
            Bomb_number.text = "x0";
        }else {
            Bombs[0].color = new Color(1f,1f,1f,1f);
            Bombs[1].gameObject.SetActive(true);
            Bomb_number.gameObject.SetActive(true);
            Bomb_number.text = "x" + Props[1].ToString();
        }
        int lineup_five = 0;
        if(MainMenu.message!=87)    lineup_five = ServerScript.lineup[5];
        else        lineup_five = MainMenu.lineup[5];
        for(int i = 0; i<2 ; i++)   props[i].gameObject.SetActive(lineup_five==i+1);
    }
    public void Change() {   
        if(MainMenu.message==87) {
            props[0].SetActive(0==MainMenu.lineup[5]-1);
            props[1].SetActive(1==MainMenu.lineup[5]-1);
            MainMenu.lineup[5] = MainMenu.lineup[5]%2 + 1;
            return;
        }
        ServerScript.lineup[5] = ServerScript.lineup[5]%2 + 1;
    }
    // Caculate who to upgrade
    void CalculateDistances() {
        for (int i = 0; i < contentRect.childCount; i++) {
            RectTransform item = contentRect.GetChild(i) as RectTransform;
            distances[i] = Mathf.Abs(MidPoint.transform.position.x - item.position.x);
        }
    }
    int FindNearestCenter() {
        float minDistance = Mathf.Min(distances);
        for (int i = 0; i < distances.Length; i++)    if (distances[i] == minDistance)    return i-1;
        return -1;
    }
}