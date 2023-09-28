using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldHandler : MonoBehaviour
{
     [SerializeField] private List<InputField> inputFields = new List<InputField>();

    public void OnInputValueChanged()
    {
        foreach (var inputField in inputFields)
        {
            inputField.text = inputField.text.ToUpper();           
        }
    }
}
