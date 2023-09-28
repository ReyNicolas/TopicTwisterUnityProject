using System.Threading.Tasks;
using Gateways.Interfaces;

namespace Actions
{
    public class IsEndOfThisMatch
    {
        private IMatchGateway matchGateway;

        public IsEndOfThisMatch(IMatchGateway matchGateway)
        {
            this.matchGateway = matchGateway;
        }

        public async Task<bool> Execute(string matchID)
        {
            return await matchGateway.CheckIfMatchIsOver(matchID);
        }
    }
}