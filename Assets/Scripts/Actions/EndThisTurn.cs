using System.Threading.Tasks;
using Gateways.Interfaces;

namespace Actions
{
    public class EndThisTurn
    {
        private ITurnGateway turnGateway;
        public EndThisTurn(ITurnGateway turnGateway) { 
        
            this.turnGateway = turnGateway;
        }

        public async Task<bool> Execute(string matchID,int roundID,int turnID, float timeLeft)
        {
            return await turnGateway.EndThisTurn(matchID, roundID, turnID, timeLeft);
        }
    }
}