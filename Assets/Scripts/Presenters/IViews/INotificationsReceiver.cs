using System;
using System.Collections.Generic;
using DTOs;

namespace Presenters.IViews
{
    public interface INotificationsReceiver
    {
        event Action<List<ForGameNotificationDTO>> OnNewForGameNotification;
    }
}