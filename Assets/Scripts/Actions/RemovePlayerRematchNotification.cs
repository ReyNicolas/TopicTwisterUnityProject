using System.Threading.Tasks;
using DTOs;
using Gateways.Interfaces;

namespace Actions
{
    public class RemovePlayerRematchNotification
    {
        private IPlayerGateway playerGateway;

        public RemovePlayerRematchNotification(IPlayerGateway playerGateway)
        {
            this.playerGateway = playerGateway;
        }

        public async Task Execute(PlayerRematchNotificationDTO playerRematchNotification)
        {
            await playerGateway.RemovePlayerRematchNotification(playerRematchNotification);
        }
    }

    
}