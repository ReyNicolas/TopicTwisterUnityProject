using System;
using System.Collections.Generic;
using DTOs;

namespace Presenters.IViews
{
    public interface IRematchNotificationsView
    {
        void SetRematchNotifications(List<PlayerRematchNotificationDTO> rematches);
        event Action<PlayerRematchNotificationDTO> OnAcceptRematch;
        event Action<PlayerRematchNotificationDTO> OnDeclineRematch;
        event Action OnUpdateRematch;
        bool IsDestroyed { get; }
        void ShowErrorWindow(string message);
    }
}