using System;
using System.Collections.Generic;
using DTOs;

namespace Presenters.IViews
{
    public interface IMatchHistoryView
    {
        void SetMatchesResults(List<MatchResultDTO> matchesResults);
        event Action OnUpdateMatchHistory;
        event Action<MatchResultDTO> OnViewMatchResult;
        event Action<MatchResultDTO> OnPlayerRematch;
        bool IsDestroyed { get; }
    }
}