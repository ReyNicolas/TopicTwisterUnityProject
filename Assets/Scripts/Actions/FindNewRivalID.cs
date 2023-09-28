using System.Threading.Tasks;
using Gateways.Interfaces;

namespace Actions
{
    public class FindNewRivalID
    {
        private IPlayerGateway playerGateway;

        public FindNewRivalID(IPlayerGateway playerGateway)
        {
            this.playerGateway = playerGateway;
        }
        public async Task<string> Execute(string playerID)
        {
            return await playerGateway.FindNewRivalID(playerID);
        }
    }
}