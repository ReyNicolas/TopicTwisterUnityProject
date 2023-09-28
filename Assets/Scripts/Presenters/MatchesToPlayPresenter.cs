using System;
using System.Collections.Generic;
using Actions;
using Commands;
using DataApplicationsContainer;
using DTOs;
using Presenters.IViews;
using Providers;

namespace Presenters
{
    public class MatchesToPlayPresenter
    {
        private string playerID;
        private IMatchesToPlayView view;
        private ActionProvider actionProvider;
        private IDataApplicationContainer dataApplicationContainer;
        private ICommand goToTurnStartCommand;
        private ICommand goToEndRoundCommand;
        public MatchesToPlayPresenter(IMatchesToPlayView view, IGatewayProvider gatewayProvider,
            IDataApplicationContainer dataApplicationContainer, ICommand goToTurnStartCommand,ICommand goToEndRoundCommand)
        {
            actionProvider = new ActionProvider(gatewayProvider);
            this.view = view;
            this.view.OnUpdateActualMatches += ReturnMatchesToPlayInfo;
            this.view.OnPlayTurn += PlayThisMatch;
            this.dataApplicationContainer = dataApplicationContainer;
            playerID = dataApplicationContainer.LoadData().PlayerID;
            this.goToTurnStartCommand = goToTurnStartCommand;
            this.goToEndRoundCommand = goToEndRoundCommand;
            ReturnMatchesToPlayInfo();
        }
        
        
        
        private async void ReturnMatchesToPlayInfo()
        {
            if (view != null && !view.IsDestroyed)
            {
                List<MatchInfoDTO> matchesInfos= await actionProvider.Provide<GetPlayerMatchesToPlay>().Execute(playerID);
                if (view != null && !view.IsDestroyed) view.SetMatchesToPlay(matchesInfos);
            }
            
        }

        private async void PlayThisMatch(MatchInfoDTO matchInfo)
        {
            try
            {
                if (!await actionProvider.Provide<IsPlayerTurn>().Execute(matchInfo.MatchID, playerID))
                {
                    throw new Exception("Not Player Turn");
                }

                var actualTurnData = await actionProvider.Provide<GetMatchActualTurnData>().Execute(matchInfo.MatchID);
                if (actualTurnData.RoundID > 1)
                {
                    GoToLastEndRoundScene(actualTurnData);
                }
                else
                {
                    GotoStartTurnScene(actualTurnData);
                }
            }
            catch (Exception ex)
            {
                view.ShowErrorWindow("Error al obtener datos de partida: " + ex.Message);
            }

        }

        private void GotoStartTurnScene(DataApplication actualTurnData)
        {
            dataApplicationContainer.SaveData(actualTurnData.MatchID, actualTurnData.RoundID, actualTurnData.TurnID, actualTurnData.NumberOfRounds, playerID);
            goToTurnStartCommand.Execute();
        }

        private void GoToLastEndRoundScene(DataApplication actualTurnData)
        {
            var roundID = actualTurnData.RoundID - 1;
            dataApplicationContainer.SaveData(actualTurnData.MatchID, roundID, actualTurnData.TurnID, actualTurnData.NumberOfRounds, playerID);
            goToEndRoundCommand.Execute();
        }
    }

    
}