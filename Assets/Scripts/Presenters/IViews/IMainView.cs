using System;

public interface IMainView
{
    void ShowPlayerNameAndVictoryPoints(string playerName, int victoryPoints);
    event Action OnStartMatch;
    event Action OnReturnLogin;
    event Action OnUpdatePlayerStats;
    void ShowRivalFound(string rivalID);
    void ShowErrorWindow(string message);
}