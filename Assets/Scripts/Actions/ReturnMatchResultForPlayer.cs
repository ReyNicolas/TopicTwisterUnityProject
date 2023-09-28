using System.Threading.Tasks;
using DTOs;
using Gateways.Interfaces;

namespace Actions
{
    public class ReturnMatchResultForPlayer
    {
        private IMatchGateway matchGateway;

        public ReturnMatchResultForPlayer(IMatchGateway matchGateway)
        {
            this.matchGateway = matchGateway;
        }

        public async Task<MatchResultDTO> Execute(string matchID,string playerID)
        {
            return await matchGateway.GetMatchResultForPlayer(matchID,playerID);
        }
    }
}