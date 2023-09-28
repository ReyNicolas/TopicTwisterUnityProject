using System;
using System.Collections.Generic;
using Commands;
using DataApplicationsContainer;
using DTOs;
using Presenters;
using Providers;
using TMPro;
using UnityEngine;

namespace Views
{
    public class TurnView : MonoBehaviour, ITurnView
    {
        [SerializeField] private List<CategoryContainer> categoriesContainers;
        [SerializeField] private TextMeshProUGUI letterText;
        [SerializeField] private TextMeshProUGUI roundText;
        [SerializeField] private TextMeshProUGUI playerNameText;

        [SerializeField] Timer timerScript; 
        [SerializeField] private GameObject keyboardGO;
        [SerializeField] private GameObject homePageButton;
        [SerializeField] private GameObject goToEndRoundButton;
        [SerializeField] private GameObject doneButton;
        [SerializeField] private ErrorWindow errorWindow;
        public event Action OnEndTurn;
        public event Action OnReturnHome;
        public event Action OnGoToEndRound;

        private TurnPresenter presenter;
        IGatewayProvider gatewayProvider = new GatewayProvider();

        private void OnDestroy()
        {
            gatewayProvider?.DisposeClient();
        }

        void Start()
        {
            presenter = new TurnPresenter();
            presenter.Initialize(this, gatewayProvider, new DataApplicationContainer(), new ChangeSceneCommand("MAINMENU"),
                new ChangeSceneCommand("ENDROUND") );
        }

        public void ShowPlayerName(string playerName)
        {
            playerNameText.text = playerName;
        }
        
        public void ShowRound(int roundId, int numberOfRounds)
        {
            roundText.text = $"{roundId} / {numberOfRounds}";
        }
        
        public void ShowCategoriesAndLetter(List<string> categoriesNames, char letter)
        {
            for (int i = 0; i < categoriesNames.Count; i++)
            {
                categoriesContainers[i].SetCategoryText(categoriesNames[i]);
            }

            letterText.text = letter.ToString();
        }

        public void SetTime(float time)
        {
            timerScript.StartTime(time, this);
        }

        public void EndTurn()
        {
            timerScript.StopTime();
            doneButton.SetActive(false);
            keyboardGO.SetActive(false);
            OnEndTurn?.Invoke();
        }

        public string GetWordFromCategoryName(string categoryName)
        {
            return categoriesContainers.Find(catCont => catCont.GetCategoryText() == categoryName).GetWordText();
        }

        public float GetTimeLeft()
        {
            return timerScript.GetTimeLeft();
        }

        public void ShowAnswer(AnswerDTO answer)
        {
            categoriesContainers.Find(catCont => catCont.GetCategoryText() == answer.CategoryName).ShowResult(answer.IsCorrect);

        }


        public void ActiveReturnHomePage()
        {
      
            homePageButton.SetActive(true);
        }
        
        public void ActiveGoToEndRound()
        {
            goToEndRoundButton.SetActive(true);
        }
        public void ReturnHome()
        {
            OnReturnHome?.Invoke();
        }

        public void GoToEndRound()
        {
            OnGoToEndRound?.Invoke();
        }

        public void ShowErrorWindow(string message)
        {
            keyboardGO.SetActive(false);
            errorWindow.gameObject.SetActive(true);
            errorWindow.SetMessage(message);
            ActiveReturnHomePage();
        }
    }
}