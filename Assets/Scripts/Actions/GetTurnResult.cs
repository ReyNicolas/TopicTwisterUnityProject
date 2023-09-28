using System.Threading.Tasks;
using DTOs;
using Gateways.Interfaces;

namespace Actions
{
    public class GetTurnResult
    {
        private ITurnGateway turnGateway;

        public GetTurnResult(ITurnGateway turnGateway)
        {
            this.turnGateway = turnGateway;
        }
        public async Task<TurnResultDTO> Execute(string matchID,int roundID,int turnID)
        {
            return await turnGateway.ReturnTurnResult(matchID, roundID, turnID);
        }
        
    }
}