using DTOs;
using Actions;
using Commands;
using DataApplicationsContainer;
using Presenters.IViews;
using Providers;
using System;

namespace Presenters
{
    public class LoginPresenter
    {
        private ILoginView view;
        private IDataApplicationContainer dataApplicationsContainer;
        private ICommand changeToMainSceneCommand;
        private ActionProvider actionProvider;

        public void Initialize(ILoginView view, IDataApplicationContainer dataApplicationsContainer,
            ICommand changeToMainSceneCommand, ActionProvider actionProvider)
        {
            this.view = view;
            this.dataApplicationsContainer = dataApplicationsContainer;
            this.changeToMainSceneCommand = changeToMainSceneCommand;
            this.actionProvider = actionProvider;
            this.view.OnRegister += CheckRegisterData;
            this.view.OnLogin += CheckLoginData;
        }

        private async void CheckRegisterData()
        {
            try
            {
                var registerData = view.GetLoginData();
                var registerResult = await actionProvider.Provide<CheckRegisterData>().Execute(registerData);
                if (registerResult.SuccessAuthentication)
                {
                    GoToMainScene(registerData);
                }
                else
                {
                    view.ShowRegisterResult(registerResult);
                }
            }
            catch(Exception ex)
            {
                view.ShowErrorWindow("Error Registrando jugador: " + ex.Message);
            }

        }

       
        private async void CheckLoginData()
        {
            try { 

                var loginData = view.GetLoginData();
                var loginResult = await actionProvider.Provide<CheckLoginData>().Execute(loginData);
                if (loginResult.SuccessAuthentication)
                {
                    GoToMainScene(loginData);
                }
                else
                {
                    view.ShowLoginResult(loginResult);
                }
             }
            catch
            {
                view.ShowErrorWindow("Error al loguear jugador");
            }

}

        private void GoToMainScene(LoginDto registerData)
        {
            dataApplicationsContainer.SaveData("", 0, 0, 0, registerData.playerID);
            changeToMainSceneCommand.Execute();
        }

    }
}