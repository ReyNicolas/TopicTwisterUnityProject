using System.Collections.Generic;
using Actions;
using Commands;
using DataApplicationsContainer;
using DTOs;
using Presenters.IViews;
using Providers;

namespace Presenters
{
    public class MatchHistoryPresenter
    {
        private string playerID;
        private IMatchHistoryView view;
        private ActionProvider actionProvider;
        private IDataApplicationContainer dataApplicationContainer;
        private ICommand goToEndMatchCommand;
        public MatchHistoryPresenter(IMatchHistoryView view,IGatewayProvider gatewayProvider,
            IDataApplicationContainer dataApplicationContainer, ICommand goToEndMatchCommand)
        {
            this.dataApplicationContainer = dataApplicationContainer;
            actionProvider = new ActionProvider(gatewayProvider);
            this.view = view;
            this.view.OnUpdateMatchHistory += ReturnMatchesResults;
            this.view.OnViewMatchResult += ViewMatchResult;
            this.view.OnPlayerRematch += SendRematchNotification;
            playerID = dataApplicationContainer.LoadData().PlayerID;
            this.goToEndMatchCommand = goToEndMatchCommand;
            ReturnMatchesResults();
        }
       

        private async void ReturnMatchesResults()
        {
            List<MatchResultDTO> matchesResults =
                await actionProvider.Provide<GetPlayerMatchHistory>().Execute(playerID);
            if (view != null && !view.IsDestroyed)
            {
                view.SetMatchesResults(matchesResults);
            }

        }

        private void ViewMatchResult(MatchResultDTO matchResult)
        {
            dataApplicationContainer.SaveData(matchResult.MatchID,0,0,0,playerID); 
            goToEndMatchCommand.Execute();
        }

        private async void SendRematchNotification(MatchResultDTO matchResult)
        {
            await actionProvider.Provide<SendRematchNotification>().Execute(matchResult.PlayerID, matchResult.RivalID);
        }
    }
}