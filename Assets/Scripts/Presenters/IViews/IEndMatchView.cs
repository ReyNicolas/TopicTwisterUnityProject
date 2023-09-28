using System;
using DTOs;

namespace Presenters.IViews
{
    public interface IEndMatchView
    {
        void ShowResult(MatchResultDTO matchResult);
         event Action OnReturnHome;

        void ShowErrorWindow(string message);
    }
}