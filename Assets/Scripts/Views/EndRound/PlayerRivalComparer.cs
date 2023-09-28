using TMPro;
using UnityEngine;

namespace Views.EndRound
{
    public class PlayerRivalComparer : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI playerValueText;
        [SerializeField] TextMeshProUGUI rivalValueText;
        [SerializeField] TextMeshProUGUI comparerText;

        public void SetComparerText(string text)
        {
            comparerText.text = text;
        }

        public void SetPlayerAndRivalTextsValues(string playerText, string rivalText)
        {
            playerValueText.text = playerText;
            rivalValueText.text = rivalText;
        }

        
    }
}