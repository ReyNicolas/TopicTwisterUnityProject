using System;
using DTOs;

namespace Presenters.IViews
{
    public interface ILoginView
    {
        event Action OnLogin;
        event Action OnRegister;
        LoginDto GetLoginData();
        void ShowLoginResult(LoginResultDTO loginResult);
        void ShowRegisterResult(LoginResultDTO registerResult);
        void ShowErrorWindow(string messagev);
    }
}