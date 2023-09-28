using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actions;
using Commands;
using DataApplicationsContainer;
using DTOs;
using Providers;

namespace Presenters
{
    public class TurnPresenter
        {
            ITurnView turnView;
            private DataApplication dataApplication;
            private StartTurnDTO startTurnDTO;
            private IDataApplicationContainer dataApplicationContainer;
            private ICommand changeToMainSceneCommand;
            private ICommand changeToEndRoundSceneCommand;
            private ActionProvider actionProvider;
            
    
            public void Initialize(ITurnView turnView,IGatewayProvider gatewayProvider,IDataApplicationContainer dataApplicationContainer, ICommand changeToMainSceneCommand,
                ICommand changeToEndRoundSceneCommand)
            {
                actionProvider = new ActionProvider(gatewayProvider);
                this.changeToMainSceneCommand = changeToMainSceneCommand;
                this.changeToEndRoundSceneCommand = changeToEndRoundSceneCommand;
                this.turnView = turnView;
                this.dataApplicationContainer = dataApplicationContainer;
                this.turnView.OnEndTurn += EndTurn;
                this.turnView.OnReturnHome += ReturnHomePage;
                this.turnView.OnGoToEndRound += GoToEndRound;
                
                ObtainInformation();
                ShowPlayerName(dataApplication.PlayerID);
                SetStartTurnDataInView();
                
            }
            private void ObtainInformation()
            {
                dataApplication = dataApplicationContainer.LoadData();
    
            }
    
            private async void SetStartTurnDataInView()
            {
                try
                {                    
                     startTurnDTO = await actionProvider.Provide<GetStartTurnData>().Execute(dataApplication.MatchID,dataApplication.RoundID,dataApplication.PlayerID);
                    ShowCategoriesAndLetter(startTurnDTO.Categories, startTurnDTO.Letter);
                    ShowTime(startTurnDTO.Time);
                    ShowRound(startTurnDTO.RoundID, startTurnDTO.NumberOfRounds);
            }
                catch (Exception ex)
                {
                    turnView.ShowErrorWindow("Error al obtener informacion del turno: " + ex.Message);
                }


            
            }
            private void ShowPlayerName(string playerName)
            {
                turnView.ShowPlayerName(playerName);
            }
            
            private void ShowTime(float time)
            {
                turnView.SetTime(time);
            }
    
                
            private void ShowCategoriesAndLetter(List<string> categories, char letter)
            {
                turnView.ShowCategoriesAndLetter(categories, letter);
            }
    
            private void ShowRound(int roundID, int numberOfRounds)
            {
                turnView.ShowRound(roundID, numberOfRounds);
            }

            private async void EndTurn()
            {
                foreach (string category in startTurnDTO.Categories)
                {
                    await SetTurnAnswer(category);
                }
            try
            {
                await actionProvider.Provide<EndThisTurn>().Execute(dataApplication.MatchID, dataApplication.RoundID, dataApplication.TurnID, turnView.GetTimeLeft());
                await actionProvider.Provide<SendMatchChangeNotificationsToPlayers>().Execute(dataApplication.MatchID);

                TurnResultDTO turnResult = await actionProvider.Provide<GetTurnResult>().Execute(dataApplication.MatchID, dataApplication.RoundID,
                   dataApplication.TurnID);
                foreach (var answerDTO in turnResult.Answers)
                {
                    turnView.ShowAnswer(answerDTO);
                }
                CheckEndOfRound();
            }
            catch (Exception ex)
            {
                turnView.ShowErrorWindow("Error al cerrar turno y obtener resultado");
                }



            }

        private async Task SetTurnAnswer(string category)
        {
            string word = turnView.GetWordFromCategoryName(category);
            await actionProvider.Provide<SetThisTurnAnswer>().Execute(dataApplication.MatchID, dataApplication.RoundID, dataApplication.TurnID, word, category, startTurnDTO.Letter);
        }

        private async void CheckEndOfRound() 
            {
                if (await actionProvider.Provide<IsThisRoundOver>().Execute(dataApplication.MatchID, dataApplication.RoundID))
                {
                    turnView.ActiveGoToEndRound();
                }
                else
                { 
                    turnView.ActiveReturnHomePage();
                }
    
            }
            private void ReturnHomePage()
            {
                changeToMainSceneCommand.Execute();
            }
    
            private void GoToEndRound()
            {
                changeToEndRoundSceneCommand.Execute();
            }
            
        }
    
    
}