using UnityEngine;
using UnityEngine.UI;
namespace Ranking_player_information_Method {
public class Ranking_player_information : MonoBehaviour
{
    public Text Rank_number; // the ranking number of the player(text)
    public int Rank_number_index = 3; // the ranking number of the player(int)
    public Image[] Faction; // the faction of the player
    public Text User_name; // the name of the player
    public Text Max_Grade; // the max progress about PVE of the player
    public Image Myself;
    public GameObject[] Frame;
    private ServerMethod.Server ServerScript; // Server.cs
    
    void Start() {
        ServerScript = FindObjectOfType<ServerMethod.Server>();
        if(Myself.tag=="15")    Rank_number_index = -1;
    }

    void Update() {
        if(ServerScript.rankFaction.Count!=0)    Update_Ranking_player_information();
    }

    // Update the player's ranking information
    public void Update_Ranking_player_information() {   
        if(Rank_number_index>=3 && Myself.tag!="15")    Myself.rectTransform.anchoredPosition = new Vector3(2f,3085f-140*(Rank_number_index-3),0f);
        if(Myself.tag=="15") {
            string MyName = ServerScript.username;
            for(int i = 0; i<ServerScript.rankName.Count ; i++)     if(ServerScript.rankName[i]==MyName)    Rank_number_index = i;
            for(int i = 0; i<Frame.Length ;i++) {
                Frame[i].gameObject.SetActive(false);
                if(i==Rank_number_index)    Frame[i].gameObject.SetActive(true);
                else if(Rank_number_index>3)    Frame[3].gameObject.SetActive(true);
            }
        }

        Rank_number.text = (Rank_number_index+1).ToString();
        User_name.text = ServerScript.rankName[Rank_number_index];
        for(int i = 0; i<5 ; i++)   Faction[i].gameObject.SetActive(false);
        if(ServerScript.rankFaction[Rank_number_index]==-1)     Faction[4].gameObject.SetActive(true);
        else    Faction[ServerScript.rankFaction[Rank_number_index]-2].gameObject.SetActive(true);
        Max_Grade.text = ServerScript.rankClear[Rank_number_index];
    }
    public void Set_tag(int index) {
        Rank_number_index = index;
    }
}
}