using System.Threading.Tasks;
using Gateways.Interfaces;

namespace Actions
{
    public class IsThisRoundOver
    {
        private IRoundGateway roundGateway;
        public IsThisRoundOver(IRoundGateway roundGateway) { 
        
            this.roundGateway = roundGateway;
        }

        public async Task<bool> Execute(string matchID,int roundID)
        {
            return await roundGateway.CheckIfRoundIsOver(matchID, roundID);
        }
    }
}