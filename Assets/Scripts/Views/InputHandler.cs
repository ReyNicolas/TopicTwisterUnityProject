using System;
using UnityEngine;

public class InputHandler: MonoBehaviour
{
    [SerializeField]IVirtualKeyboard virtualKeyboard;
    ITextContainer selectedTextContainer;

    public void Start()
    {
        virtualKeyboard.OnBackSpace += BackSpace;
        virtualKeyboard.OnNewStringKey += AddStringToSelectedInput;
    }

    public void BackSpace(object sender, EventArgs e)
    {
        selectedTextContainer?.BackSpace();
    }

    public void AddStringToSelectedInput(object sender, string word)
    {
        selectedTextContainer?.AddString(word);
    }

    public void SetSelectedInpunt(ITextContainer textContainer)
    {
        selectedTextContainer = textContainer;
    }
}
