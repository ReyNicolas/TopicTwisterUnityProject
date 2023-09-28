using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputFieldLogic : MonoBehaviour, ITextContainer ,IPointerClickHandler
{
    [SerializeField] InputHandler inputHandler;
    [SerializeField] InputField inputField;

    void ITextContainer.AddString(string aString)
    {
        inputField.text += aString;
    }

    void ITextContainer.BackSpace()
    {
        if(inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Remove(inputField.text.Length - 1);
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        inputHandler.SetSelectedInpunt(this);
    }
}