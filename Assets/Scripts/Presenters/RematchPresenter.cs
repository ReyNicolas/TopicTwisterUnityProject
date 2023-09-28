using System;
using System.Collections.Generic;
using Actions;
using DataApplicationsContainer;
using DTOs;
using Presenters.IViews;
using Providers;

namespace Presenters
{
    public class RematchPresenter
    {
        private string playerID;
        private IRematchNotificationsView view;
        private ActionProvider actionProvider;
        public RematchPresenter(IRematchNotificationsView view, IGatewayProvider gatewayProvider,
            IDataApplicationContainer dataApplicationContainer)
        {
            actionProvider = new ActionProvider(gatewayProvider);
            this.view = view;
            this.view.OnAcceptRematch += CreateNewMatch;
            this.view.OnDeclineRematch += DeleteRematchNotification;
            this.view.OnUpdateRematch += UpdateRematchNotifications;
            playerID = dataApplicationContainer.LoadData().PlayerID;
            UpdateRematchNotifications();
        }        

        private async void UpdateRematchNotifications()
        {
            List<PlayerRematchNotificationDTO> rematchNotifications =
                await actionProvider.Provide<GetPlayerRematchNotifications>().Execute(playerID);
            if (view != null && !view.IsDestroyed)
            {
                view.SetRematchNotifications(rematchNotifications);
            }
        }

        private async void CreateNewMatch(PlayerRematchNotificationDTO rematchNotification)
        {
            try
            {
               var matchID =  await actionProvider.Provide<CreateMatchWithThisConfig>().Execute(
                    new List<string>() { rematchNotification.playerID, rematchNotification.rivalID },
                    3,5,60);
                await actionProvider.Provide<SendMatchChangeNotificationsToPlayers>().Execute(matchID);
                DeleteRematchNotification(rematchNotification);
            }
            catch (Exception ex)
            {
                view.ShowErrorWindow("Error al crear partida por rematch: " + ex.Message);
            }
        }

        private async void DeleteRematchNotification(PlayerRematchNotificationDTO rematchNotification)
        {
            await actionProvider.Provide<RemovePlayerRematchNotification>().Execute(rematchNotification);
        }
    }
}