using System;
using System.Collections.Generic;
using Commands;
using DataApplicationsContainer;
using Presenters;
using Presenters.IViews;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DTOs;
using Providers;

namespace Views.EndRound
{
    public class EndRoundView : MonoBehaviour, IEndRoundView
    {
        [SerializeField] private TextMeshProUGUI rivalNameText;
        [SerializeField] private TextMeshProUGUI playerNameText;
        [SerializeField] private TextMeshProUGUI roundText;
        [SerializeField] private TextMeshProUGUI letterText;
        [SerializeField] private List<PlayerRivalComparer> playerRivalCategoriesComparers;
        [SerializeField] private List<Image> playerWordsResults;
        [SerializeField] private List<Image> rivalWordsResults;
        [SerializeField] private PlayerRivalComparer playerRivalCorrectsComparer;
        [SerializeField] private PlayerRivalComparer playerRivalTimeLeftComparer;
        [SerializeField] private TextMeshProUGUI resultDescriptionText;
        [SerializeField] private GameObject homePageButton;
        [SerializeField] private GameObject goToEndMatchButton;
        [SerializeField] private GameObject goToNextTurnButton;
        [SerializeField] private ErrorWindow errorWindow;


        public event Action OnReturnHome;
        public event Action OnGoToEndMatch;
        public event Action OnGoToNextTurn;
        private EndRoundPresenter endRoundPresenter;
        IGatewayProvider gatewayProvider;

        private void OnDestroy()
        {
            gatewayProvider?.DisposeClient();
        }

        private void Start()
        {
            gatewayProvider = new GatewayProvider();
            endRoundPresenter = new EndRoundPresenter();
            endRoundPresenter.Initialize(this, gatewayProvider,new DataApplicationContainer(),
                     new ChangeSceneCommand("MAINMENU"), new ChangeSceneCommand("ENDMATCH"), new ChangeSceneCommand("STARTTURN"));

            playerRivalCorrectsComparer.SetComparerText("Total Corrects");
            playerRivalTimeLeftComparer.SetComparerText("Time Left");
        }

        public void ShowResult(RoundResultDTO roundResult)
        {

            playerNameText.text = roundResult.PlayerID;
            rivalNameText.text = roundResult.RivalID;
            roundText.text = "Round " + roundResult.RoundID;
            letterText.text = roundResult.Letter.ToString();
            

            for (int i = 0; i < roundResult.answersComparer.Count; i++)
            {
                SetAnswerComparerOfIndex(roundResult, i);
            }

            playerRivalCorrectsComparer.SetPlayerAndRivalTextsValues(
                roundResult.PlayerCorrectsCount.ToString(),
                roundResult.RivalCorrectsCount.ToString()
            );

            playerRivalTimeLeftComparer.SetPlayerAndRivalTextsValues(
                roundResult.PlayerTimeLeft.ToString("f"),
                roundResult.RivalTimeLeft.ToString("f")
            );

            ShowWinner(roundResult.IsPlayerTheWinner, roundResult.Tie, roundResult.RivalID);
        }

        private void SetAnswerComparerOfIndex(RoundResultDTO roundResult, int index)
        {
            playerRivalCategoriesComparers[index].SetComparerText(roundResult.answersComparer[index].CategoryName);

            playerRivalCategoriesComparers[index].SetPlayerAndRivalTextsValues(
                roundResult.answersComparer[index].PlayerWordResult.Word,
                roundResult.answersComparer[index].RivalWordResult.Word);
            playerWordsResults[index].overrideSprite = Resources.Load<Sprite>("Sprites/WordsResults/" +
                                                                          roundResult.answersComparer[index]
                                                                              .PlayerWordResult.IsCorrect);


            rivalWordsResults[index].overrideSprite = Resources.Load<Sprite>("Sprites/WordsResults/" +
                                                                         roundResult.answersComparer[index]
                                                                             .RivalWordResult.IsCorrect);

        }

        void ShowWinner(bool isPlayerTheWinner, bool isTie, string rivalID)
        {
            if (isPlayerTheWinner)
            {
                resultDescriptionText.text = "You are the Winner of the round, Congrats!";
            }
            else if (isTie)
            {
                resultDescriptionText.text = "Both players are the Winners of the round, Congrats!";
            }
            else
            {
                resultDescriptionText.text = rivalID + " is the Winner of the round!";
            }
        }
        
        public void ActiveReturnHomePage()
        {
            homePageButton.SetActive(true); 
        }
        public void ActiveGoToNextTurn()
        {
            goToNextTurnButton.SetActive(true);
        }
        
        public void ActiveGoToEndMatch()
        {
            goToEndMatchButton.SetActive(true);
        }
        
        public void ReturnHome()
        {
            OnReturnHome?.Invoke();
        }

        public void GoToEndMatch()
        {
            OnGoToEndMatch?.Invoke();
        }

        public void GoToNextTurn()
        {
            OnGoToNextTurn?.Invoke();
        }

        public void ShowErrorWindow(string message)
        {
            ActiveReturnHomePage();
            errorWindow.gameObject.SetActive(true);
            errorWindow.SetMessage(message);
        }
    }
}
