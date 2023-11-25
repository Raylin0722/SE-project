using System.Collections;
using System.Collections.Generic;
using ServerMethod;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Charactor : MonoBehaviour
{
    public Image[] Pictures; // The all pictures in team

    private struct Location
    {
        public Vector3 position;
        public float offset;
    }
    private List<Location> Location_List = new List<Location>();

    // Server.cs
    private ServerMethod.Server ServerScript;
    private int[] Lineup;
    private int Faction;

    

    // Start is called before the first frame update
    void Start()
    {
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        Character_Location();
    }

    // Update is called once per frame
    void Update()
    {
        Update_Display(Faction,Lineup);
    }

    // Update Display images
    private void Update_Display(int Faction,int[] Lineup)
    {
        Faction = ServerScript.faction;
        Lineup = ServerScript.lineup;
        if(Faction==0)  return;
        for(int i = 0; i<Pictures.Length; i++)
        {
            Pictures[i].gameObject.SetActive(false);
        }
        for(int i = 0; i<Lineup.Length-1; i++)
        {
            Pictures[7*(Faction-1)+Lineup[i]-1].gameObject.SetActive(true);
        }
        for(int i = 0; i<Lineup.Length-1; i++)
        {
            Pictures[7*(Faction-1)+Lineup[i]-1].rectTransform.anchoredPosition = new Vector3(Location_List[7*(Faction-1)+Lineup[i]-1].position.x+i*Location_List[7*(Faction-1)+Lineup[i]-1].offset,Location_List[7*(Faction-1)+Lineup[i]-1].position.y,Location_List[7*(Faction-1)+Lineup[i]-1].position.z);
        }
    }

    // Character Display Definition
    private void Character_Location()
    {
        Location W1 = new Location
        {
            position = new Vector3(-317f,160f,0f),
            offset = 244.0f
        };
        Location_List.Add(W1);
        Location W2 = new Location
        {
            position = new Vector3(-300f,110f,0f),
            offset = 245.0f
        };
        Location_List.Add(W2);
        Location W3 = new Location
        {
            position = new Vector3(-310f,205f,0f),
            offset = 245.0f
        };
        Location_List.Add(W3);
        Location W4 = new Location
        {
            position = new Vector3(-320f,120f,0f),
            offset = 245.0f
        };
        Location_List.Add(W4);
        Location W5 = new Location
        {
            position = new Vector3(-325f,145f,0f),
            offset = 245.0f
        };
        Location_List.Add(W5);
        Location W6 = new Location
        {
            position = new Vector3(-320f,95f,0f),
            offset = 245.0f
        };
        Location_List.Add(W6);
        Location W7 = new Location
        {
            position = new Vector3(-310f,180f,0f),
            offset = 245.0f
        };
        Location_List.Add(W7);
        Location B1 = new Location
        {
            position = new Vector3(-317f,160f,0f),
            offset = 244.0f
        };
        Location_List.Add(B1);
        Location B2 = new Location
        {
            position = new Vector3(-290f,100f,0f),
            offset = 245.0f
        };
        Location_List.Add(B2);
        Location B3 = new Location
        {
            position = new Vector3(-320f,201f,0f),
            offset = 245.0f
        };
        Location_List.Add(B3);
        Location B4 = new Location
        {
            position = new Vector3(-313f,102f,0f),
            offset = 245.0f
        };
        Location_List.Add(B4);
        Location B5 = new Location
        {
            position = new Vector3(-328f,150f,0f),
            offset = 245.0f
        };
        Location_List.Add(B5);
        Location B6 = new Location
        {
            position = new Vector3(-319f,94f,0f),
            offset = 245.0f
        };
        Location_List.Add(B6);
        Location B7 = new Location
        {
            position = new Vector3(-307f,182f,0f),
            offset = 245.0f
        };
        Location_List.Add(B7);
        Location S1 = new Location
        {
            position = new Vector3(-317f,160f,0f),
            offset = 244.0f
        };
        Location_List.Add(S1);
        Location S2 = new Location
        {
            position = new Vector3(-287f,95f,0f),
            offset = 245.0f
        };
        Location_List.Add(S2);
        Location S3 = new Location
        {
            position = new Vector3(-304f,189f,0f),
            offset = 245.0f
        };
        Location_List.Add(S3);
        Location S4 = new Location
        {
            position = new Vector3(-304f,108f,0f),
            offset = 245.0f
        };
        Location_List.Add(S4);
        Location S5 = new Location
        {
            position = new Vector3(-364f,152f,0f),
            offset = 245.0f
        };
        Location_List.Add(S5);
        Location S6 = new Location
        {
            position = new Vector3(-320f,95f,0f),
            offset = 245.0f
        };
        Location_List.Add(S6);
        Location S7 = new Location
        {
            position = new Vector3(-310f,180f,0f),
            offset = 245.0f
        };
        Location_List.Add(S7);
    }
}
