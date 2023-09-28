using System.Threading.Tasks;
using Gateways.Interfaces;

namespace Actions
{
    public class EndThisMatch
    {
        private IMatchGateway matchGateway;

        public EndThisMatch(IMatchGateway matchGateway)
        {
            this.matchGateway = matchGateway;
        }

        public async Task<bool> Execute(string matchID)
        {
            return await matchGateway.EndThisMatch(matchID);
        }
    }
}