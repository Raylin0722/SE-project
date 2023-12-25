using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(TMP_InputField))]
public class TabBetween : MonoBehaviour {
    public TMP_InputField nextField;
    TMP_InputField myField;
    void Start() {
        if(nextField == null) {
            Destroy(this);
            return;
        }
        myField = GetComponent<TMP_InputField>(); 
    }
    void Update() {
        if(myField.isFocused && Input.GetKeyDown(KeyCode.Tab))nextField.ActivateInputField();
    }
}
