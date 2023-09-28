using System.Threading.Tasks;
using Gateways.Interfaces;

namespace Actions
{
    public class IsPlayerTurn
    {
        private IMatchGateway matchGateway;

        public IsPlayerTurn(IMatchGateway matchGateway)
        {
            this.matchGateway = matchGateway;
        }

        public async Task<bool> Execute(string matchID, string playerID)
        {
            return await matchGateway.InThisMatchIsThisPlayerTurn(matchID, playerID);
        }
    }
}