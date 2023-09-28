using System;
using Commands;
using DataApplicationsContainer;
using Presenters;
using TMPro;
using UnityEngine;

namespace Views.HomePage
{
    public class MainView : MonoBehaviour, IMainView
    {
        [SerializeField] private TextMeshProUGUI playerNameText;
        [SerializeField] private TextMeshProUGUI victoryPointsText;
        [SerializeField] private MatchmakingView matchmakingView;
        [SerializeField] private NotificationsController notificationsController;
        [SerializeField] private ErrorWindow errorWindow;
        public event Action OnStartMatch;
        public event Action OnReturnLogin;
        public event Action OnUpdatePlayerStats;

        private MainPagePresenter presenter;
        

        void Start()
        {
            notificationsController.OnPlayerStatsUpdate += InvokeUpdatePlayerStats;
            presenter = new MainPagePresenter();
            presenter.Initialize(this,notificationsController.gatewayProvider, new DataApplicationContainer(),
                new ChangeSceneCommand("STARTTURN"), new ChangeSceneCommand("LOGINMENU"));
        }

        private void InvokeUpdatePlayerStats()
        {
            OnUpdatePlayerStats?.Invoke();
        }

        public void ShowPlayerNameAndVictoryPoints(string playerName, int victoryPoints)
        {
            playerNameText.text = playerName;
            victoryPointsText.text = victoryPoints.ToString();
        }

        public void StartNewMatch()
        {
            matchmakingView.gameObject.SetActive(true);
            matchmakingView.SetPlayer(playerNameText.text);
            OnStartMatch?.Invoke();
        }

        public void ReturnLoginMenu()
        {
            OnReturnLogin?.Invoke();
        }

        public void ShowRivalFound(string rivalID)
        {
            matchmakingView.SetRival(rivalID);
        }

        public void ShowErrorWindow(string message)
        {
            matchmakingView.gameObject.SetActive(false);
            errorWindow.gameObject.SetActive(true);
            errorWindow.SetMessage(message);
        }
    }
}
