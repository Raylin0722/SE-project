using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charactor : MonoBehaviour
{
    public Sprite[] Pictures; // The all pictures in team
    public Image[] Roles; // role_1 role_2 role_3 role_4 role_5
    public int[] Role_Indexes; // the indexes about Figures

    // Start is called before the first frame update
    void Start()
    {
        Update_Images(Role_Indexes);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Update images
    private void Update_Images(int[] Role_Indexes)
    {
        for (int i = 0; i < Roles.Length; i++)
        {
            Roles[i].sprite = Pictures[Role_Indexes[i]];
        }
    }
}
