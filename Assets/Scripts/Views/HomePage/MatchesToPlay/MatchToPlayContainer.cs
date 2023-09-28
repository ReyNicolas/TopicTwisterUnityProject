using System;
using DTOs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.HomePage
{
    public class MatchToPlayContainer : MonoBehaviour
    {
        [SerializeField] Image buttonImage;
        [SerializeField] Image containerImage;
        [SerializeField] TextMeshProUGUI RivalIDText;
        [SerializeField] TextMeshProUGUI PlayerRoundsWonText;
        [SerializeField] TextMeshProUGUI RivalRoundsWonText;
        [SerializeField] TextMeshProUGUI ActualRoundText;
        [SerializeField] private Button PlayTurnButton;
        [SerializeField] private TextMeshProUGUI PlayTurnButtonText;
        public event Action<MatchInfoDTO> OnPlayTurn;
        private MatchInfoDTO matchInfo;
        private string roundTextStart = "Round ";
        public void Initialize(MatchInfoDTO matchInfo)
        {
            RivalIDText.text = matchInfo.RivalID;
            PlayerRoundsWonText.text = matchInfo.PlayerRoundsWon.ToString();
            RivalRoundsWonText.text = matchInfo.RivalRoundsWon.ToString();
            ActualRoundText.text = roundTextStart + matchInfo.ActualRound;
            this.matchInfo = matchInfo;
            SetButton(matchInfo.IsPlayerTurn);
        }

        public void ButtonPress()
        {
            OnPlayTurn?.Invoke(matchInfo);
        }

        private void SetButton(bool isPLayerTurn)
        {
            if (isPLayerTurn)
            {
                SetButtonInteractable();
            }
            else
            {
                SetButtonLocked();
            }

        }

        private void SetButtonLocked()
        {
            buttonImage.overrideSprite = Resources.Load<Sprite>("Sprites/MainSprite/RivalTurn");
            PlayTurnButtonText.text = "RIVAL TURN";
            PlayTurnButton.interactable = false;
        }

        private void SetButtonInteractable()
        {
            buttonImage.overrideSprite = Resources.Load<Sprite>("Sprites/MainSprite/PlayTurn");
            PlayTurnButtonText.text = "PLAY TURN";
            PlayTurnButton.interactable = true;
        }
    }
    
}
