using System;
using Commands;
using DataApplicationsContainer;
using DTOs;
using Presenters;
using Presenters.IViews;
using Providers;
using TMPro;
using UnityEngine;

namespace Views.EndMatch
{
    public class EndMatchView : MonoBehaviour, IEndMatchView
    {
        [SerializeField] private TextMeshProUGUI playerNameText;
        [SerializeField] private TextMeshProUGUI rivalNameText;
        [SerializeField] private TextMeshProUGUI endMatchText;
        [SerializeField] private TextMeshProUGUI resultValuesText;
        [SerializeField] private TextMeshProUGUI resultMessageText;
        [SerializeField] private ErrorWindow errorWindow;
        public event Action OnReturnHome;
        private EndMatchPresenter endMatchPresenter;
        IGatewayProvider gatewayProvider;

        private void OnDestroy()
        {
            gatewayProvider?.DisposeClient();
        }

        private void Start()
        {
            gatewayProvider = new GatewayProvider();
            endMatchPresenter = new EndMatchPresenter();
            endMatchPresenter.Initialize(this, gatewayProvider ,new DataApplicationContainer(),
                new ChangeSceneCommand("MAINMENU"));
        }

        public void ShowResult(MatchResultDTO matchResult)
        {
            playerNameText.text = matchResult.PlayerID;
            rivalNameText.text = matchResult.RivalID;
            endMatchText.text = "End Match";
            resultValuesText.text = matchResult.PlayerRoundsWon.ToString()+
                                    " - " +matchResult.RivalRoundsWon.ToString();

            if (matchResult.IsPlayerTheWinner)
            {
                resultMessageText.text = "You are the Winner, Congrats!";
            }
            else if (matchResult.IsTie)
            {
                resultMessageText.text = "Both players are the Winners, Congrats!";
            }
            else
            {
                resultMessageText.text = matchResult.RivalID + " is the Winner!";
            }


        }
        
        public void ReturnHome()
        {
            OnReturnHome?.Invoke();
        }

        public void ShowErrorWindow(string message)
        {
            errorWindow.gameObject.SetActive(true);
            errorWindow.SetMessage(message);
        }
    }
}
