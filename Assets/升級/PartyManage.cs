using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PartyManage : MonoBehaviour
{
    public Canvas canvas;
    [SerializeField] GameObject[] Watermelonfigure;
    [SerializeField] GameObject[] Party1;
    [SerializeField] GameObject[] Party2;
    [SerializeField] GameObject[] Party3;
    [SerializeField] GameObject[] Party4;
    [SerializeField] GameObject[] Party5;
    private int index;
    static private int[] PartyMember = new int[5];
    private GameObject spawnedCharacter;


    //public Image[] Pictures; // The all pictures in under level_up

    private struct Location
    {
        public Vector2 position;
        public float offset;
    }
    private List<Location> Location_List = new List<Location>();
    
    // Server.cs
    private ServerMethod.Server ServerScript;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            PartyMember[i] = 0;
        }

        /*
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        Character_Location();
        for(int i = 0; i<Pictures.Length; i++)
        {
            Pictures[i].gameObject.SetActive(false);
        }
        for(int i = 0; i<5 ; i++)
        {
            PartyMember[i] = ServerScript.lineup[i] - 1;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            if (PartyMember[i] != 0)
            {
                switch (i)
                {
                    case 0:
                        ShowPartyByIndex(Party1, PartyMember[i] - 1);
                        break;
                    case 1:
                        ShowPartyByIndex(Party2, PartyMember[i] - 1);
                        break;
                    case 2:
                        ShowPartyByIndex(Party3, PartyMember[i] - 1);
                        break;
                    case 3:
                        ShowPartyByIndex(Party4, PartyMember[i] - 1);
                        break;
                    case 4:
                        ShowPartyByIndex(Party5, PartyMember[i] - 1);
                        break;
                }
            }
            else
            {
                switch (i)
                {
                    case 0:
                        HideAllParty(Party1);
                        break;
                    case 1:
                        HideAllParty(Party2);
                        break;
                    case 2:
                        HideAllParty(Party3);
                        break;
                    case 3:
                        HideAllParty(Party4);
                        break;
                    case 4:
                        HideAllParty(Party5);
                        break;
                }
            }
        }
    }

    public void SetPartyMemberValue(int partyIndex, int value)
    {
        Debug.Log(PartyMember[0] + ", " + PartyMember[1] + ", " + PartyMember[2] + ", " + PartyMember[3] + ", " + PartyMember[4]);

        if (partyIndex >= 0 && partyIndex < PartyMember.Length)
        {
            int tempt = PartyMember[partyIndex];
            for (int i = 0; i < 5; i++)
            {
                if (PartyMember[i] == value)
                {
                    PartyMember[i] = tempt;
                }
            }
            PartyMember[partyIndex] = value;
        }
        Debug.Log(PartyMember[0] + ", " + PartyMember[1] + ", " + PartyMember[2] + ", " + PartyMember[3] + ", " + PartyMember[4]);

    }
    // 根据传入的索引显示特定的 Party1，同时隐藏其他的 Party1
    public void ShowPartyByIndex(GameObject[] party, int index)
    {
        // 首先隱藏所有的 Party1
        HideAllParty(party);
        // 檢查索引是否有效
        if (index >= 0 && index < party.Length)
        {
            // 顯示特定索引的 Party1
            party[index].SetActive(true);
        }
        else
        {
            Debug.LogWarning("Invalid index for Party1");
        }
    }


    // 隐藏所有的 Party1
    void HideAllParty(GameObject[] party)
    {
        foreach (GameObject partyObject in party)
        {
            partyObject.SetActive(false);
        }
    }


    // Character Display Definition
    private void Character_Location()
    {
        Location W1 = new Location
        {
            position = new Vector2(-672f,-280f),
            offset = 270.0f
        };
        Location_List.Add(W1);
        Location W2 = new Location
        {
            position = new Vector2(-672f,-281f),
            offset = 270.0f
        };
        Location_List.Add(W2);
        Location W3 = new Location
        {
            position = new Vector2(-673f,-285f),
            offset = 270.0f
        };
        Location_List.Add(W3);
        Location W4 = new Location
        {
            position = new Vector2(-671f,-283f),
            offset = 270.0f
        };
        Location_List.Add(W4);
        Location W5 = new Location
        {
            position = new Vector2(-670f,-288f),
            offset = 270.0f
        };
        Location_List.Add(W5);
        Location W6 = new Location
        {
            position = new Vector2(-673f,-301f),
            offset = 270.0f
        };
        Location_List.Add(W6);
        Location W7 = new Location
        {
            position = new Vector2(-673f,-301f),
            offset = 270.0f
        };
        Location_List.Add(W7);
        Location B1 = new Location
        {
            position = new Vector2(-672f,-280f),
            offset = 270.0f
        };
        Location_List.Add(B1);
        Location B2 = new Location
        {
            position = new Vector2(-673f,-284f),
            offset = 270.0f
        };
        Location_List.Add(B2);
        Location B3 = new Location
        {
            position = new Vector2(-673f,-285f),
            offset = 270.0f
        };
        Location_List.Add(B3);
        Location B4 = new Location
        {
            position = new Vector2(-672f,-297.195f),
            offset = 270.0f
        };
        Location_List.Add(B4);
        Location B5 = new Location
        {
            position = new Vector2(-673f,-274.385f),
            offset = 270.0f
        };
        Location_List.Add(B5);
        Location B6 = new Location
        {
            position = new Vector2(-673f,-301f),
            offset = 270.0f
        };
        Location_List.Add(B6);
    }


}


    
/*
    bool IsMouseOverSpecificUI(string target)
    {
        // 检查是否有鼠标悬停在UI元素上
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        // 获取射线碰撞的所有UI元素
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // 检查射线碰撞的物体是否是特定UI物体
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.tag ==target)
            {
                return true;
            }
        }

        // 如果没有碰撞的UI元素或者碰撞的不是特定UI物体，返回 false
        return false;
    }
    public void SpawnWatermelon(int watermelonIndex)
    {
        // 在Canvas下创建水果角色
        GameObject watermelon = Instantiate(Watermelonfigure[watermelonIndex - 1], canvas.transform);

        // 保存生成的角色
        spawnedCharacter = watermelon;
        index = watermelonIndex;

        // 设置水果角色的RectTransform属性
        RectTransform rt = watermelon.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.zero;
        rt.pivot = new Vector2(0.5f, 0.5f);
    }
    public void watermelon1Product()
    {
        //GameObject Watermelon1=Instantiate(Watermelonfigure[0], transform);
        Debug.Log("我要拉了");
        index=1;
        SpawnWatermelon(index);
    }
    public void watermelon2Product()
    {
        //GameObject Watermelon1=Instantiate(Watermelonfigure[1], transform);
        index=2;
        SpawnWatermelon(index);
    }
    public void watermelon3Product()
    {
        //GameObject Watermelon1=Instantiate(Watermelonfigure[2], transform);
        index=3;
        SpawnWatermelon(index);
    }
    public void watermelon4Product()
    {
        //GameObject Watermelon1=Instantiate(Watermelonfigure[3], transform);
        index=4;
        SpawnWatermelon(index);
    }
    public void watermelon5Product()
    {
        //GameObject Watermelon1=Instantiate(Watermelonfigure[4], transform);
        index=5;
        SpawnWatermelon(index);
    }
        void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        //Debug.Log("mouse"+mousePos); 
        if (spawnedCharacter != null)
        {
            spawnedCharacter.transform.position = new Vector3(mousePos.x, mousePos.y + 1f, 0f);
            if (IsMouseOverSpecificUI("party_1"))
            {
                PartyMember[0]=index;
                Destroy(spawnedCharacter);

            }   
        }
    }
*/