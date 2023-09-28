using System.Threading.Tasks;
using Gateways.Interfaces;

namespace Actions
{
    public class SendMatchChangeNotificationsToPlayers
    {
        private IMatchGateway matchGateway;

        public SendMatchChangeNotificationsToPlayers(IMatchGateway matchGateway)
        {
            this.matchGateway = matchGateway;
        }

        public async Task Execute(string matchID)
        {
            await matchGateway.SendMatchChangeNotificationsToPlayers(matchID);
        }
    }
}