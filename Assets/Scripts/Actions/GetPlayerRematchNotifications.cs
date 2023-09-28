using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;
using Gateways.Interfaces;

namespace Actions
{
    public class GetPlayerRematchNotifications
    {
        private IPlayerGateway playerGateway;

        public GetPlayerRematchNotifications(IPlayerGateway playerGateway)
        {
            this.playerGateway = playerGateway;
        }

        public async Task<List<PlayerRematchNotificationDTO>> Execute(string playerID)
        {
            return await playerGateway.GetPlayerRematchNotifications(playerID);
        }
    }

    
}