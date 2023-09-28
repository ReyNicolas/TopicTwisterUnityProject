using System;
using System.Collections.Generic;
using DTOs;

namespace Presenters.IViews
{
    public interface IMatchesToPlayView
    {
        bool IsDestroyed { get; }
        event Action OnUpdateActualMatches;
        event Action<MatchInfoDTO> OnPlayTurn;
        void SetMatchesToPlay(List<MatchInfoDTO> matchesInfos);
        void ShowErrorWindow(string message);
    }
}