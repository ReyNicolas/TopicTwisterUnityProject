using System.Collections.Generic;
using System.Threading.Tasks;
using Actions;
using Commands;
using DataApplicationsContainer;
using DTOs;
using Providers;
using System;

namespace Presenters
{
   public class MainPagePresenter
    {
        IMainView mainView;
        private string playerID;
        private IDataApplicationContainer dataApplicationContainer;
        private ICommand GoToStartTurnCommand;
        private ICommand ReturnLoginMenuCommand;
        private ActionProvider actionProvider;

        public void Initialize(IMainView mainView,IGatewayProvider gatewayProvider, IDataApplicationContainer dataApplicationContainer, ICommand goToStartTurnCommand, ICommand returnLoginMenuCommand)
        {
            this.mainView = mainView;
            this.dataApplicationContainer = dataApplicationContainer;
            this.GoToStartTurnCommand = goToStartTurnCommand;
            this.ReturnLoginMenuCommand = returnLoginMenuCommand;
            actionProvider = new ActionProvider(gatewayProvider);
            mainView.OnStartMatch += StartNewMatch;
            mainView.OnReturnLogin += ReturnLoginMenu;
            mainView.OnUpdatePlayerStats += UpdatePlayerStats;
            GeneratePlayer();
            ShowPlayerNameAndVictoryPoints();
        }
        private void GeneratePlayer()
        {
            playerID = dataApplicationContainer.LoadData().PlayerID;
        }

        private void UpdatePlayerStats()
        {            
            ShowPlayerNameAndVictoryPoints();
        }
        
        private async void ShowPlayerNameAndVictoryPoints()
        {
            var playerStats = await actionProvider.Provide<GetPlayerStats>().Execute(playerID);
            mainView.ShowPlayerNameAndVictoryPoints(playerID, playerStats.victoryPoints);
        }
    
        private async void StartNewMatch()
        {
            try
            {
                List<string> playersIDs = await ConnectPlayers();
                string matchID = await actionProvider.Provide<CreateMatchWithThisConfig>().Execute(playersIDs, 3, 5, 60);
                await actionProvider.Provide<SendMatchChangeNotificationsToPlayers>().Execute(matchID);
                if (await actionProvider.Provide<IsPlayerTurn>().Execute(matchID,playerID))
                {
                    await SaveDataToStartMatch(matchID);
                    GoToStartTurnCommand.Execute();
                }
            }
            catch (Exception ex)
            {
                mainView.ShowErrorWindow("Error al crear nueva partida: " + ex.Message);
            }
            
        }
        
        async Task<List<string>> ConnectPlayers()
        {
            var rivalID = await actionProvider.Provide<FindNewRivalID>().Execute(playerID);
            mainView.ShowRivalFound(rivalID);
            return new List<string>() { playerID,rivalID};

        }        
        

        private async Task SaveDataToStartMatch(string matchID)
        {
            DataApplication dataApplication = await actionProvider.Provide<GetMatchActualTurnData>().Execute(matchID);
            dataApplicationContainer.SaveData(dataApplication.MatchID,dataApplication.RoundID,dataApplication.TurnID,
                dataApplication.NumberOfRounds,dataApplication.PlayerID);
        }
        
        private void ReturnLoginMenu()
        {
            dataApplicationContainer.SaveData("",0,0,0,"");
            ReturnLoginMenuCommand.Execute();
        }
    }
}