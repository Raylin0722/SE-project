using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ServerMethod;
using Unity.VisualScripting;
public class Level_up : MonoBehaviour
{
    //蔡松豪加的
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

    // Start is called before the first frame update
    void Start()
    {
        if(MainMenu.message!=87)    ServerScript = FindObjectOfType<ServerMethod.Server>();
        //
        contentRect = scrollRect.content;
        int itemCount = contentRect.childCount;
        distances = new float[itemCount];
    }

    // Update is called once per frame
    void Update()
    {
        Update_values(); // Update money and charactor level and money and bombs
        CalculateDistances();
        UpgradeIndex = FindNearestCenter();
    }

    // When click < BACK >
    public void Button_Back()
    {   
        page_Level_up.SetActive(false);
        ALL_Button.SetActive(true); // Open All button in Main_Scene
    }

    // When click < Upgrade >
    public void Button_Upgrade()
    {
        page_Check_upGrade.SetActive(true);
    }
    // when click <cancel upgrade>
    public void Cancel_Upgrade()
    {
        page_Check_upGrade.SetActive(false);
    }
    //when click <sure upgrade>
    public void Sure_Upgrade()
    {
        //升級主保
        if(UpgradeIndex==0)
        {
            if(MainMenu.message==87)
            {
                if(MainMenu.castleLevel==15)
                {
                    StartCoroutine(MaxGrade());
                    return;
                }
            }
            else if(ServerScript.castleLevel==15)
            {
                StartCoroutine(MaxGrade());
                return;
            }
        }
        else if(UpgradeIndex!=0)
        {
            if(MainMenu.message==87)
            {
                if(MainMenu.character[UpgradeIndex-1]==5)
                {
                    StartCoroutine(MaxGrade());
                    return;
                }
            }
            else if(ServerScript.character[UpgradeIndex-1]==5)
            {
                StartCoroutine(MaxGrade());
                return;
            }
        }
        
        if(MainMenu.message==87) 
        {
            if(UpgradeIndex==0)
            {
                MainMenu.castleLevel = MainMenu.castleLevel + 1;
                MainMenu.slingshotLevel = MainMenu.slingshotLevel + 1;
            }
            else
            {
                MainMenu.character[UpgradeIndex-1] = MainMenu.character[UpgradeIndex-1] + 1;
            }
            return;
        }   
        //算出來在更新到sever
        Debug.Log(UpgradeIndex);
        page_Check_upGrade.SetActive(false);
        StartCoroutine(Upgrade_Surver(UpgradeIndex));
    }
    //max grade
    IEnumerator MaxGrade()
    {
        MaxGradeFigure.SetActive(true);
        yield return new WaitForSeconds(1);
        MaxGradeFigure.gameObject.SetActive(false);
    }

    //Send the data to server
    private IEnumerator Upgrade_Surver(int UpgradeIndex)
    {
        if(UpgradeIndex!=0)
        {
            IEnumerator coroutine = ServerScript.updateCard(UpgradeIndex,0);
            yield return StartCoroutine(coroutine);
            Return result = coroutine.Current as Return;
            Debug.Log(result.success);
        }
        else
        {
            IEnumerator coroutine = ServerScript.updateCard(UpgradeIndex,1);
            yield return StartCoroutine(coroutine);
            Return result = coroutine.Current as Return;
            Debug.Log(result.success);
        }
    }
    // Update energy && money && tear
    public void Update_values()
    {
        money.text = MainMenu.money.ToString();
        int castleLevel = MainMenu.castleLevel;
        int[] character = MainMenu.character;
        int[] Props = MainMenu.props;
        if(MainMenu.message!=87)
        {  
            money.text = ServerScript.money.ToString();
            castleLevel = ServerScript.castleLevel;
            character = ServerScript.character;
            Props = ServerScript.props;
        }
        if(castleLevel>=15)
        {
            Level[0].fontSize = 15;
            Level[0].text = "MAX";
            Dollar[0].text = "-";
        }
        else
        {
            Level[0].text = castleLevel.ToString();
            Dollar[0].text = (Money[0]+500*(castleLevel-1)).ToString();
        }
        
        for(int i = 0; i<character.Length; i++)
        {
            Level[i+1].fontSize = 25;
            Level[i+1].text = character[i].ToString();
            Level[i+1].color = new Color(0f,0f,0f,1f);
            Dollar[i+1].color = new Color(0f,0f,0f,1f);
            if(character[i]==0)
            {
                Level[i+1].color = new Color(0f,0f,0f,0f);
                Dollar[i+1].color = new Color(0f,0f,0f,0f);
            }
            else if(character[i]==1)
            {
                Dollar[i+1].text = Money[i+1].ToString();
            }
            else if(character[i]>=5)
            {
                Level[i+1].fontSize = 15;
                Level[i+1].text = "MAX";
                Dollar[i+1].text = "-";
            }
            else
            {
                Dollar[i+1].text = (Money[i+1]*1.5*(character[i]-1)).ToString();
            }
        }

        // whether you have Bombs can use
        if(Props[1]==0)
        {
            Bombs[0].color = new Color(0f,0f,0f,1f);
            Bombs[1].gameObject.SetActive(false);
            Bomb_number.text = "x0";
        }
        else
        {
            Bombs[0].color = new Color(1f,1f,1f,1f);
            Bombs[1].gameObject.SetActive(true);
            Bomb_number.gameObject.SetActive(true);
            Bomb_number.text = "x" + Props[1].ToString();
        }
        int lineup_five = MainMenu.lineup[5];
        if(MainMenu.message!=87)    lineup_five = ServerScript.lineup[5];
        if(lineup_five==1)
        {
            props[0].gameObject.SetActive(true);
            props[1].gameObject.SetActive(false);
        }
        else
        {
            props[0].gameObject.SetActive(false);
            props[1].gameObject.SetActive(true);
        }
    }
    
    // Update Bomb
    public void Change()
    {   
        if(MainMenu.message==87)    
        {
            props[0].SetActive(0==MainMenu.lineup[5]-1);
            props[1].SetActive(1==MainMenu.lineup[5]-1);
            MainMenu.lineup[5] = MainMenu.lineup[5]%2 + 1;
            return;
        }
        props[0].SetActive(0==ServerScript.lineup[5]-1);
        props[1].SetActive(1==ServerScript.lineup[5]-1);
        ServerScript.lineup[5] = ServerScript.lineup[5]%2 + 1;
    }
    // Caculate who to upgrade
    void CalculateDistances()
    {
        for (int i = 0; i < contentRect.childCount; i++)
        {
            RectTransform item = contentRect.GetChild(i) as RectTransform;
            distances[i] = Mathf.Abs(MidPoint.transform.position.x - item.position.x);
        }
    }

    int FindNearestCenter()
    {
        float minDistance = Mathf.Min(distances);
        for (int i = 0; i < distances.Length; i++)
        {
            if (distances[i] == minDistance)
            {
                return i-1;
            }
        }
        return -1;
    }

    
/*
    // Character Data struct
    public struct Character_Data
    {
        public float Attack;
        public float Attack_rate;
        public float Energy;
        public float Energy_rate;
        public float Health;
        public float Health_rate;
        public float Move_Speed;
        public float Move_Speed_rate;
        public float Cool_Time;
        public float Cool_Time_rate;
        public float Attack_Range;
        public float Attack_Range_rate;
        public float Dollar;
        public float Dollar_rate;
    }
    private List<Character_Data> Character_Data_List = new List<Character_Data>();

    // Tower Data struct
    public struct Tower_Data
    {
        public float Health;
        public float Health_rate;
        public float Attack;
        public float Attack_rate;
        public float Initial_Energy;
        public float Initial_Energy_rate;
        public float Max_Energy;
        public float Max_Energy_rate;
        public float Increment;
        public float Increment_rate;
        public float Dollar;
        public float Dollar_rate;
    }

    // Character Data Definition and Tower Data Definition 
    private void Data_Definition()
    {
        Character_Data one = new Character_Data
        {
            Attack = 150.0f,
            Attack_rate = 1.6f,
            Energy = 150.0f,
            Energy_rate = 1.4f,
            Health = 700.0f,
            Health_rate = 1.2f,
            Move_Speed = 1.7f,
            Move_Speed_rate = 1.1f,
            Cool_Time = 4.77f,
            Cool_Time_rate = 1.15f,
            Attack_Range = 7.0f,
            Attack_Range_rate = 1.0f,
            Dollar = 400.0f,
            Dollar_rate = 1.5f
        };
        Character_Data_List.Add(one);
        Character_Data two = new Character_Data
        {
            Attack = 50.0f,
            Attack_rate = 1.7f,
            Energy = 70.0f,
            Energy_rate = 1.4f,
            Health = 400.0f,
            Health_rate = 1.2f,
            Move_Speed = 4.0f,
            Move_Speed_rate = 1.1f,
            Cool_Time = 3.0f,
            Cool_Time_rate = 1.15f,
            Attack_Range = 1.5f,
            Attack_Range_rate = 1.0f,
            Dollar = 300.0f,
            Dollar_rate = 1.5f
        };
        Character_Data_List.Add(two);
                Character_Data three = new Character_Data
        {
            Attack = 300f,
            Attack_rate = 1.3f,
            Energy = 250f,
            Energy_rate = 1.4f,
            Health = 1000f,
            Health_rate = 1.2f,
            Move_Speed = 2f,
            Move_Speed_rate = 1.1f,
            Cool_Time = 7f,
            Cool_Time_rate = 1.15f,
            Attack_Range = 3f,
            Attack_Range_rate = 1f,
            Dollar = 700f,
            Dollar_rate = 1.5f
        };
        Character_Data_List.Add(three);
        Character_Data four = new Character_Data
        {
            Attack = 200f,
            Attack_rate = 1.4f,
            Energy = 150f,
            Energy_rate = 1.4f,
            Health = 600f,
            Health_rate = 1.2f,
            Move_Speed = 3.1f,
            Move_Speed_rate = 1.1f,
            Cool_Time = 4.77f,
            Cool_Time_rate = 1.15f,
            Attack_Range = 5.5f,
            Attack_Range_rate = 1f,
            Dollar = 600f,
            Dollar_rate = 1.5f
        };
        Character_Data_List.Add(four);
        Character_Data five = new Character_Data
        {
            Attack = 150f,
            Attack_rate = 1.5f,
            Energy = 200f,
            Energy_rate = 1.4f,
            Health = 800f,
            Health_rate = 1.2f,
            Move_Speed = 2.7f,
            Move_Speed_rate = 1.1f,
            Cool_Time = 5.88f,
            Cool_Time_rate = 1.15f,
            Attack_Range = 4f,
            Attack_Range_rate = 1f,
            Dollar = 500f,
            Dollar_rate = 1.5f
        };
        Character_Data_List.Add(five);
        Character_Data six = new Character_Data
        {
            Attack = 130f,
            Attack_rate = 1.5f,
            Energy = 150f,
            Energy_rate = 1.4f,
            Health = 900f,
            Health_rate = 1.2f,
            Move_Speed = 1.3f,
            Move_Speed_rate = 1.1f,
            Cool_Time = 4f,
            Cool_Time_rate = 1.15f,
            Attack_Range = 2.5f,
            Attack_Range_rate = 1f,
            Dollar = 650f,
            Dollar_rate = 1.5f
        };
        Character_Data_List.Add(six);
        Character_Data seven = new Character_Data
        {
            Attack = 250f,
            Attack_rate = 1.4f,
            Energy = 200f,
            Energy_rate = 1.4f,
            Health = 800f,
            Health_rate = 1.2f,
            Move_Speed = 3f,
            Move_Speed_rate = 1.1f,
            Cool_Time = 4.5f,
            Cool_Time_rate = 1.15f,
            Attack_Range = 4.5f,
            Attack_Range_rate = 1f,
            Dollar = 750f,
            Dollar_rate = 1.5f
        };
        Character_Data_List.Add(seven);

        // Tower Data Definition
        Tower_Data Tower = new Tower_Data
        {
            Health = 3000.0f,
            Health_rate = 1.1f,
            Attack = 200.0f,
            Attack_rate = 1.05f,
            Initial_Energy = 200.0f,
            Initial_Energy_rate = 1.05f,
            Max_Energy = 100.0f,
            Max_Energy_rate = 1.0f,
            Increment = 200.0f,
            Increment_rate = 1.3f,
            Dollar = 200.0f,
            Dollar_rate = 1.06f
        };
        // Bomb usable
        BombUsable = (ServerScript.props[1] < 1) ? false : true;
    }
*/
    
}