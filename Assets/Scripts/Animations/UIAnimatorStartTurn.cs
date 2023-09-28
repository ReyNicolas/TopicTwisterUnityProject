using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimatorStartTurn : MonoBehaviour
{
    [SerializeField] GameObject LetterIcon;
    [SerializeField] List<GameObject> Categories;

    public void Start()
    {
        LeanTween.moveLocal(LetterIcon, new Vector2(0, -120), 1f).setEase(LeanTweenType.easeOutBounce);

        foreach (GameObject category in Categories)
        {
            LeanTween.scale(category, Vector2.one, 1f).setEase(LeanTweenType.easeOutBounce);
        }
    }
}
