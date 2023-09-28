using System;


public class KeyboardVirtual :  IVirtualKeyboard
{
    public override event EventHandler<string> OnNewStringKey;
    public override event EventHandler OnBackSpace;


    public void OnKeyClick( string key)
    {
        OnNewStringKey?.Invoke(this, key);  
    }

    public void BackSpace()
    {
        OnBackSpace?.Invoke(this, EventArgs.Empty);
    }
    
}
