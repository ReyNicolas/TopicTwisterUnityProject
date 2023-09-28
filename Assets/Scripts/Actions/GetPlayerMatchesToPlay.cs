using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;
using Gateways;
using Gateways.Interfaces;

namespace Actions
{
    public class GetPlayerMatchesToPlay
    {
        private IPlayerGateway playerGateway;

        public GetPlayerMatchesToPlay(IPlayerGateway playerGateway)
        {
            this.playerGateway = playerGateway;
        }

        public async Task<List<MatchInfoDTO>> Execute(string playerID)
        {
            return await playerGateway.GetThisMatchesToPlay(playerID);
        }
    }
}