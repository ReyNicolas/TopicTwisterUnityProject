using System.Threading.Tasks;
using DTOs;
using Gateways.Interfaces;

namespace Actions
{
    public class ReturnRoundResultForPlayer
    {
        private IRoundGateway roundGateway;

        public ReturnRoundResultForPlayer(IRoundGateway roundGateway)
        {
            this.roundGateway = roundGateway;
        }

        public async Task<RoundResultDTO> Execute(string matchID,int roundID,string playerID)
        {
            return await roundGateway.GetRoundResultForPlayer(matchID, roundID, playerID);
        }
    }
}