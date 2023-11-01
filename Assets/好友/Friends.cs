using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Friends : MonoBehaviour
{
    public GameObject Close; // Close Button
    public GameObject page_Friends; // the page which you want to close
    public InputField Input_Field; // Search object(input field)
    public Image Image_Input_Field; // The object which you want to change pictures
    public Sprite Old_Image; // Old picture
    public Sprite New_Image; // New picture
    public Button Search; // Search Button

    // Start is called before the first frame update
    void Start()
    {
        Input_Field.onValueChanged.AddListener(Detect_Input); // Initialization, set the listener of the input box to the Detect_Input  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When click < X >
    public void Button_Close()
    {
        page_Friends.SetActive(false);
    }

    // When click < Input > 
    private void Detect_Input(string newValue)
    {
        if (!string.IsNullOrEmpty(newValue))
        {
            Image_Input_Field.sprite = New_Image; // input!=NULL => change new picture
        }
        else
        {
            Image_Input_Field.sprite = Old_Image; // input!=NULL => change old picture
        }
    }
}
