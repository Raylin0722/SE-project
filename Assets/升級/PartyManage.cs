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
    static private int[] PartyMember = new int[6];
    private GameObject spawnedCharacter;
    
    public Image[] Pictures; // The all pictures in under level_up
    public Image[] Up_Pictures; // The all pictures in up level_up

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
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        Character_Location();

        for (int i = 0; i < 6; i++)
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
        Up_Update();
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

    //讀取partymember
    public int GetPartyMember()
    {
        return PartyMember[5];
    }
    public void SetPartyMemberValue(int partyIndex, int value)
    {
        //Debug.Log(PartyMember[0] + ", " + PartyMember[1] + ", " + PartyMember[2] + ", " + PartyMember[3] + ", " + PartyMember[4]+", " + PartyMember[5]);

        if (partyIndex >= 0 && partyIndex <5)
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
        else
        {
            PartyMember[partyIndex] = value;
        }
        Debug.Log(PartyMember[0] + ", " + PartyMember[1] + ", " + PartyMember[2] + ", " + PartyMember[3] + ", " + PartyMember[4]+", " + PartyMember[5]);

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


    private void Up_Update()
    {
        if(ServerScript.faction[0]==1)  return;
        // Show current page && Hide the others
        for(int i = 0; i<8; i++)
        {
            for(int j = 0; j<4 ; j++)
            {
                Up_Pictures[8*j+i].gameObject.SetActive(false);
            }
            
            if(i==0)
            {
                Up_Pictures[8*(ServerScript.faction[1]-2)+i].gameObject.SetActive(true);
            }
            else
            {
                Up_Pictures[32+i-1].gameObject.SetActive(false); // Close chain
                Up_Pictures[8*(ServerScript.faction[1]-2)+i].gameObject.SetActive(true);
                Up_Pictures[8*(ServerScript.faction[1]-2)+i].color = new Color(1f,1f,1f,1f);

                if(ServerScript.character[i-1]==0)
                {
                    Up_Pictures[32+i-1].gameObject.SetActive(true); // Open chain
                    Up_Pictures[8*(ServerScript.faction[1]-2)+i].color = new Color(0f,0f,0f,1f);
                }
            }
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
        Location B7 = new Location
        {
            position = new Vector2(-673f,-301f),
            offset = 270.0f
        };
        Location_List.Add(B7);
        Location S1 = new Location
        {
            position = new Vector2(-672f,-280f),
            offset = 270.0f
        };
        Location_List.Add(S1);
        Location S2 = new Location
        {
            position = new Vector2(-672f,-281f),
            offset = 270.0f
        };
        Location_List.Add(S2);
        Location S3 = new Location
        {
            position = new Vector2(-673f,-285f),
            offset = 270.0f
        };
        Location_List.Add(S3);
        Location S4 = new Location
        {
            position = new Vector2(-671f,-283f),
            offset = 270.0f
        };
        Location_List.Add(S4);
        Location S5 = new Location
        {
            position = new Vector2(-670f,-288f),
            offset = 270.0f
        };
        Location_List.Add(S5);
        Location S6 = new Location
        {
            position = new Vector2(-673f,-301f),
            offset = 270.0f
        };
        Location_List.Add(S6);
        Location S7 = new Location
        {
            position = new Vector2(-681.6f,-268.3f),
            offset = 270.0f
        };
        Location_List.Add(S7);
        Location M1 = new Location
        {
            position = new Vector2(-672f,-270f),
            offset = 270.0f
        };
        Location_List.Add(M1);
        Location M2 = new Location
        {
            position = new Vector2(-680.8f,-275.4f),
            offset = 270.0f
        };
        Location_List.Add(M2);
        Location M3 = new Location
        {
            position = new Vector2(-673f,-281.9f),
            offset = 270.0f
        };
        Location_List.Add(M3);
        Location M4 = new Location
        {
            position = new Vector2(-671f,-283f),
            offset = 270.0f
        };
        Location_List.Add(M4);
        Location M5 = new Location
        {
            position = new Vector2(-670f,-288f),
            offset = 270.0f
        };
        Location_List.Add(M5);
        Location M6 = new Location
        {
            position = new Vector2(-673f,-301f),
            offset = 270.0f
        };
        Location_List.Add(M6);
        Location M7 = new Location
        {
            position = new Vector2(-673f,-301f),
            offset = 270.0f
        };
        Location_List.Add(M7);
    }


}

