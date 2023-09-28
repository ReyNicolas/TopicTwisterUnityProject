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
    public class EndMatchPresenter
    {
     
        private IEndMatchView view;
        private ICommand changeToMainSceneCommand;
        private DataApplication dataApplication;
        private ActionProvider actionProvider;

        public void Initialize(IEndMatchView view,IGatewayProvider gatewayProvider,IDataApplicationContainer applicationContainer, ICommand changeToMainSceneCommand)
        {
            this.view = view;
            this.view.OnReturnHome += ReturnHomePage;
            this.changeToMainSceneCommand = changeToMainSceneCommand;
            dataApplication = applicationContainer.LoadData();
            actionProvider = new ActionProvider(gatewayProvider);
            ShowInView();
        }

        private async Task ShowInView()
        {
            try
            {
                await actionProvider.Provide<EndThisMatch>().Execute(dataApplication.MatchID);
                await actionProvider.Provide<SendEndMatchNotificationsToPlayers>().Execute(dataApplication.MatchID);
                MatchResultDTO matchResultDto = await actionProvider.Provide<ReturnMatchResultForPlayer>().Execute(dataApplication.MatchID, dataApplication.PlayerID);
                view.ShowResult(matchResultDto);
            }
            catch (Exception ex)
            {
                view.ShowErrorWindow("Error al obtener resultado de partida: " + ex.Message);
            }
        }
        
        private void ReturnHomePage()
        {
            changeToMainSceneCommand.Execute();
        }

    }
}