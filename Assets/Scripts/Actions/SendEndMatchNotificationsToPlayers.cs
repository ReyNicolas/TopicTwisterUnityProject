using System.Threading.Tasks;
using Gateways.Interfaces;

namespace Actions
{
    public class SendEndMatchNotificationsToPlayers
    {
        private IMatchGateway matchGateway;

        public SendEndMatchNotificationsToPlayers(IMatchGateway matchGateway)
        {
            this.matchGateway = matchGateway;
        }

        public async Task Execute(string matchID)
        {
            await matchGateway.SendEndMatchNotificationsToPlayers(matchID);
        }
    }
}