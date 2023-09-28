using System.Collections.Generic;

namespace Presenters
{
    public interface IStartOfTurnView
    {
        void ShowCategoriesAndLetter(List<string> categories, char letter);
        void WaitThisTimeToChangeToTurn(float time);
        void ShowErrorWindow(string message);
    }
}