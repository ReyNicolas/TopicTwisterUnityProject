using System;
using System.Collections.Generic;
using DataApplicationsContainer;
using DTOs;
using Utils;
using Presenters.IViews;
using Providers;
using UnityEngine;



public class NotificationsController : MonoBehaviour
{
    public IGatewayProvider gatewayProvider = new GatewayProvider();
    private string playerID;
    //public event Action<List<ForGameNotificationDTO>> OnNewForGameNotification;
    public event Action OnMatchesToPlayUpdate;
    public event Action OnMatchesHistorialUpdate;
    public event Action OnRematchNotificationsUpdate;
    public event Action OnPlayerStatsUpdate;
    AllForGameNotificationsIDs allForGameNotificationsIDs = new AllForGameNotificationsIDs();

    private void Start()
    {
        playerID = new DataApplicationContainer().LoadData().PlayerID;
        UpdateForGameNotifications();
    }
    private void OnDestroy()
    {
        gatewayProvider.DisposeClient();
    }

    async void UpdateForGameNotifications()
    {
        while (true)
        {            
            var notifications = await gatewayProvider.ProvidePlayerGateway().GetPlayerForGameNotification(playerID);
            if (notifications.Count > 0)
            {
                foreach(var notification in notifications)
                {
                    if(notification.notificationID == allForGameNotificationsIDs.historialChange)
                    {
                        OnMatchesHistorialUpdate?.Invoke();
                        OnMatchesToPlayUpdate?.Invoke();
                    }
                    else if (notification.notificationID == allForGameNotificationsIDs.matchesChange)
                    {
                        OnMatchesToPlayUpdate?.Invoke();
                    }
                    else if(notification.notificationID == allForGameNotificationsIDs.playerStatsChange)
                    {
                        OnPlayerStatsUpdate?.Invoke();
                    } 
                    else if (notification.notificationID == allForGameNotificationsIDs.rematchNotifications)
                    {
                        OnRematchNotificationsUpdate?.Invoke();
                    }

                    await gatewayProvider.ProvidePlayerGateway().RemovePlayerForGameNotification(playerID,notification.notificationID);
                }
            }
            
        }
        
    }

}


