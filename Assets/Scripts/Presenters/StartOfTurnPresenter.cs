using System;
using System.Threading.Tasks;
using Actions;
using DataApplicationsContainer;
using DTOs;
using Providers;


namespace Presenters
{
    public class StartOfTurnPresenter
    {
        private IStartOfTurnView startOfTurnView;
        private DataApplication dataApplication;
        private IDataApplicationContainer dataApplicationContainer;
        private ActionProvider actionProvider;
        
        public async void Initialize(IStartOfTurnView startOfTurnView,IGatewayProvider gatewayProvider, IDataApplicationContainer dataApplicationContainer, float timeToChangeView)
        {
            actionProvider = new ActionProvider(gatewayProvider);
            this.startOfTurnView = startOfTurnView;
            this.dataApplicationContainer = dataApplicationContainer;
            ObtainInformation();
            await SetStartTurnDataInView();
            SetWaitTimeToChangeToTurn(timeToChangeView);
        }
        
        private void ObtainInformation()
        {
            dataApplication =  dataApplicationContainer.LoadData();

        }
        private async Task SetStartTurnDataInView()
        {
            try
            {
                StartTurnDTO startTurnDto = await actionProvider.Provide<GetStartTurnData>().Execute(dataApplication.MatchID, dataApplication.RoundID,
                    dataApplication.PlayerID);
                startOfTurnView.ShowCategoriesAndLetter(startTurnDto.Categories, startTurnDto.Letter);
            }
            catch (Exception ex)
            {
                startOfTurnView.ShowErrorWindow("Error al obtener informacion del turno: " +ex.Message);
            }
        }

        private void SetWaitTimeToChangeToTurn(float time)
        {
            startOfTurnView.WaitThisTimeToChangeToTurn(time);
        }
    }
}