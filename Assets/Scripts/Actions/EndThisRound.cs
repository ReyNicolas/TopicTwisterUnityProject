using System.Threading.Tasks;
using Gateways.Interfaces;

namespace Actions
{
    public class EndThisRound
    {
        private IRoundGateway roundGateway;
        public EndThisRound(IRoundGateway roundGateway) { 
        
            this.roundGateway = roundGateway;
        }

        public async Task<bool> Execute(string matchID,int roundID)
        {
           return await roundGateway.EndThisRound(matchID, roundID);
        }
    }
}