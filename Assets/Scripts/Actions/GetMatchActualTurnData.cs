using System.Threading.Tasks;
using DTOs;
using Gateways.Interfaces;

namespace Actions
{
    public class GetMatchActualTurnData
    {
        private IMatchGateway matchGateway;

        public GetMatchActualTurnData(IMatchGateway matchGateway)
        {
            this.matchGateway = matchGateway;
        }

        public async Task<DataApplication> Execute(string matchID)
        {
            return await matchGateway.GetMatchActualTurnData(matchID);
        }
    }
}