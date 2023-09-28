

using System;
using System.Threading.Tasks;
using DTOs;
using Gateways.Interfaces;

namespace Actions
{
    public class GetStartTurnData
    {
        private ITurnGateway turnGateway;

        public GetStartTurnData(ITurnGateway turnGateway)
        {

            this.turnGateway = turnGateway;
        }

        public async Task<StartTurnDTO> Execute(string matchID, int roundID, string playerID)
        {
            return await turnGateway.GetStartTurnData(matchID, roundID, playerID);
        }

    }
}
