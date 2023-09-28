using System;
using System.Collections.Generic;
using DTOs;

public interface ITurnView
{
    void ShowCategoriesAndLetter(List<string> categoriesNames, char letter);
    void ShowRound(int roundId, int numberOfRounds);
    void ShowPlayerName(string playerName);

    void SetTime(float time);
   
    float GetTimeLeft();
    
    string GetWordFromCategoryName(string categoryName);
    void ShowAnswer(AnswerDTO answer);
    
    event Action OnEndTurn;
    event Action OnReturnHome;
    event Action OnGoToEndRound;
    void ActiveReturnHomePage();
    void ActiveGoToEndRound();
    void ShowErrorWindow(string message);
}