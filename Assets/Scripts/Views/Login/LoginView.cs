using System;
using Commands;
using DataApplicationsContainer;
using DTOs;
using Presenters;
using Presenters.IViews;
using Providers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Login
{
    public class LoginView : MonoBehaviour, ILoginView
    {
        [SerializeField] private InputField playerNameText;
        [SerializeField] private InputField passwordText;
        [SerializeField] private TextMeshProUGUI ErrorMessageText;
        [SerializeField] private ErrorWindow errorWindow; 
        
        public event Action OnLogin;
        public event Action OnRegister;
        IGatewayProvider gatewayProvider = new GatewayProvider();

        private void OnDestroy()
        {
            gatewayProvider?.DisposeClient();
        }

        private void Start()
        {
            (new LoginPresenter()).Initialize(this,new DataApplicationContainer(),new ChangeSceneCommand("MAINMENU"),new ActionProvider(gatewayProvider));
        }

        public LoginDto GetLoginData()
        {
            return new LoginDto(playerNameText.text, passwordText.text);
        }

        public void ShowLoginResult(LoginResultDTO loginResult)
        {
            ErrorMessageText.text = loginResult.ErrorMessage;
        }

        public void ShowRegisterResult(LoginResultDTO registerResult)
        {
            ErrorMessageText.text = registerResult.ErrorMessage;
        }

        public void OnLoginInvoke()
        {
            OnLogin?.Invoke();
        }
        public void OnRegisterInvoke()
        {
            OnRegister?.Invoke();
        }

        public void ShowErrorWindow(string message)
        {
            errorWindow.gameObject.SetActive(true);
            errorWindow.SetMessage(message);
        }
    }
}