using UnityEngine;
using UnityEngine.UI;
namespace Small_ranking_list_Method {
public class Small_ranking_list : MonoBehaviour
{
    private ServerMethod.Server ServerScript; // Server.cs
    public Text Rank_number; // the ranking number of the player(text)
    private int Rank_number_index = -1; // the ranking number of the player(int)
    public Image[] Faction; // the faction of the player
    public Text User_name; // the name of the player
    
    void Start() {
        ServerScript = FindObjectOfType<ServerMethod.Server>();
    }
    void Update() {
        if(Rank_number_index!=-1 && ServerScript.rankFaction.Count!=0)  Update_Ranking_player_information();
    }
    // Update the player's ranking information
    public void Update_Ranking_player_information() {   
        Rank_number.text = (Rank_number_index+1).ToString();
        User_name.text = ServerScript.rankName[Rank_number_index];
        for(int i = 0; i<5 ; i++)   Faction[i].gameObject.SetActive(false);
        if(ServerScript.rankFaction[Rank_number_index]==-1)     Faction[4].gameObject.SetActive(true);
        else    Faction[ServerScript.rankFaction[Rank_number_index]-2].gameObject.SetActive(true);
    }
    public void Set_tag(int index) {
        Rank_number_index = index;
    }
}
}