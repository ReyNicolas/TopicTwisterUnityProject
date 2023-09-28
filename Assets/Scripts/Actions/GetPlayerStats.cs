using System;
using System.Threading.Tasks;
using DTOs;
using Gateways.Interfaces;

namespace Actions
{
    public class GetPlayerStats
    {
        private IPlayerGateway playerGateway;
        public GetPlayerStats(IPlayerGateway playerGateway) { 
        
            this.playerGateway = playerGateway;
        }

        public async Task<PlayerStats> Execute(string playerID)
        {
            return await playerGateway.GetPlayerStats(playerID);
        }
        
    }
    
    
}
