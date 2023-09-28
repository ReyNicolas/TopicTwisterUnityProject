using  System;
using DTOs;

namespace Presenters.IViews
{
    public interface IEndRoundView
    {
        void ShowResult(RoundResultDTO roundResult);
        void ActiveReturnHomePage();
        void ActiveGoToEndMatch();
        void ActiveGoToNextTurn();
        event Action OnReturnHome;
         event Action OnGoToEndMatch;
         event Action OnGoToNextTurn;

        void ShowErrorWindow(string message);
    }
}