using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;
using Gateways.Interfaces;

namespace Actions
{
    public class GetPlayerMatchHistory
    {
        private IPlayerGateway playerGateway;

        public GetPlayerMatchHistory(IPlayerGateway playerGateway)
        {
            this.playerGateway = playerGateway;
        }

        public async Task<List<MatchResultDTO>> Execute(string playerID)
        {
            return await playerGateway.GetThisMatchHistory(playerID);
        }
    }
}