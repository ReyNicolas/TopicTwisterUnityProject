using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimatorEndRound : MonoBehaviour
{
    [SerializeField] List<GameObject> playerAnswerResults;
    [SerializeField] List<GameObject> rivalAnswerResults;
    [SerializeField] float horizontalDistanceFromCenter;

    private void Start()
    {
        foreach (GameObject playerAnswerResult in playerAnswerResults)
        {
            LeanTween.moveLocalX(playerAnswerResult, -horizontalDistanceFromCenter, 1f);
        }


        foreach (GameObject rivalAnswerResult in rivalAnswerResults)
        {
            LeanTween.moveLocalX(rivalAnswerResult, horizontalDistanceFromCenter, 1f);
        }
    }
}
