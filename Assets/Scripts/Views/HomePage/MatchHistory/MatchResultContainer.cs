using System;
using DTOs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class MatchResultContainer : MonoBehaviour
    {
        [SerializeField] Image image;
        [SerializeField] TextMeshProUGUI RivalIDText;
        [SerializeField] TextMeshProUGUI PlayerRoundsWonText;
        [SerializeField] TextMeshProUGUI RivalRoundsWonText;
        [SerializeField] TextMeshProUGUI ResultText;
        [SerializeField] private Button ViewResultButton;
        private MatchResultDTO matchResult;
        public event Action<MatchResultDTO> OnViewResult;
        public event Action<MatchResultDTO> OnRematch;
        public void Initialize(MatchResultDTO matchResult)
        {
            this.matchResult = matchResult;
            RivalIDText.text = matchResult.RivalID;
            PlayerRoundsWonText.text = matchResult.PlayerRoundsWon.ToString();
            RivalRoundsWonText.text = matchResult.RivalRoundsWon.ToString();
            if (matchResult.IsTie)
            {
                ResultText.text = "Tie";
            }

            else if (matchResult.IsPlayerTheWinner)
            {
                ResultText.text = "Won";
            }
            else
            {
                ResultText.text = "Lost";
            }
        }
        public void ViewResultButtonPress()
        {
            OnViewResult?.Invoke(matchResult);
        }
        public void RematchButtonPress()
        {
            OnRematch?.Invoke(matchResult);
        }
    }
}
    