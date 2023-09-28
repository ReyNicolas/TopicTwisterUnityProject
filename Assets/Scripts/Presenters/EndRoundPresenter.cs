using System;
using System.Threading.Tasks;
using Actions;
using Commands;
using DataApplicationsContainer;
using DTOs;
using Presenters.IViews;
using Providers;

namespace Presenters
{
    public class EndRoundPresenter
    {
        private IEndRoundView view;
        private DataApplication dataApplication;
        private ICommand changeToMainSceneCommand;
        private ICommand changeToEndMatchSceneCommand;
        private ICommand changeToTurnSceneCommand;
        private IDataApplicationContainer dataApplicationContainer;
        private ActionProvider actionProvider;
        public async void Initialize(IEndRoundView view,IGatewayProvider gatewayProvider,IDataApplicationContainer dataApplicationContainer,
            ICommand changeToMainSceneCommand, ICommand changeToEndMatchSceneCommand,ICommand changeToTurnSceneCommand)//revisar
        {
            this.view = view;
            this.view.OnReturnHome += ReturnHomePage;
            this.view.OnGoToEndMatch += GoToEndMatch;
            this.view.OnGoToNextTurn += GoToNextTurn;
            this.changeToMainSceneCommand = changeToMainSceneCommand;
            this.changeToEndMatchSceneCommand = changeToEndMatchSceneCommand;
            this.changeToTurnSceneCommand = changeToTurnSceneCommand;
            actionProvider = new ActionProvider(gatewayProvider);
            this.dataApplicationContainer = dataApplicationContainer;
            dataApplication = dataApplicationContainer.LoadData();

            try
            {
                await ShowInView();
                await CheckEndOfMatch();
            }
            catch (Exception ex)
            {
                view.ShowErrorWindow("Error al mostrar resultado final de ronda: " + ex.Message);
            }

        }
        
        private async Task ShowInView()
        {
            await actionProvider.Provide<EndThisRound>().Execute(dataApplication.MatchID, dataApplication.RoundID);
            await actionProvider.Provide<SendMatchChangeNotificationsToPlayers>().Execute(dataApplication.MatchID);
            RoundResultDTO roundResult = await actionProvider.Provide<ReturnRoundResultForPlayer>().Execute(dataApplication.MatchID, dataApplication.RoundID,dataApplication.PlayerID);
            view.ShowResult(roundResult);
        }

        private async Task CheckEndOfMatch()
        {
            
            if (await actionProvider.Provide<IsEndOfThisMatch>().Execute(dataApplication.MatchID))
            {
                view.ActiveGoToEndMatch();
            }
            else
            {
                await CheckNexRoundTurn();
            }
            
        }

        
        private async Task CheckNexRoundTurn()
        {
            
            if (await actionProvider.Provide<IsPlayerTurn>().Execute(dataApplication.MatchID,dataApplication.PlayerID))
            {
                dataApplication = await actionProvider.Provide<GetMatchActualTurnData>().Execute(dataApplication.MatchID);
                
                dataApplicationContainer.SaveData(dataApplication.MatchID,dataApplication.RoundID,dataApplication.TurnID,
                    dataApplication.NumberOfRounds,dataApplication.PlayerID);
                view.ActiveGoToNextTurn();
                
            }
            else
            {
                view.ActiveReturnHomePage(); 
            }
            
        }
        
        private void ReturnHomePage()
        {
            changeToMainSceneCommand.Execute();
        }

        private void GoToEndMatch()
        {
            changeToEndMatchSceneCommand.Execute();
        }
        private void GoToNextTurn()
        {
            changeToTurnSceneCommand.Execute();
        }
        
    }
}