using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class TabBetween : MonoBehaviour {
    public TMP_InputField nextField;
    TMP_InputField myField;
    // Start is called before the first frame update
    void Start() {
        if(nextField == null) {
            Destroy(this);
            return;
        }
        myField = GetComponent<TMP_InputField>(); 
    }

    // Update is called once per frame
    void Update() {
        if(myField.isFocused && Input.GetKeyDown(KeyCode.Tab)) {
            nextField.ActivateInputField();
        } 
    }
}
