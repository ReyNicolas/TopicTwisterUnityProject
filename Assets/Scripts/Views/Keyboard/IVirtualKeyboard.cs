using System;
using UnityEngine;

public abstract class IVirtualKeyboard: MonoBehaviour
{
   public abstract event EventHandler<string> OnNewStringKey;
   public abstract event EventHandler OnBackSpace;
}
