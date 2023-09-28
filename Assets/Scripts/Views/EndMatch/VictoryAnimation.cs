using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class VictoryAnimation : MonoBehaviour
{
    [SerializeField] GameObject confeti;

  

    void Start()
    {
        StartMoveUpAnimation();
    }

    void StartMoveUpAnimation()
    {
        LeanTween.scale(confeti, Vector3.one, 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.moveLocal(confeti, new Vector3(0f, 0f, 0f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutExpo);
        
    }
}
