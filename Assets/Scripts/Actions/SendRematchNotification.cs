using System.Threading.Tasks;
using Gateways.Interfaces;

namespace Actions
{
    public class SendRematchNotification
    {
        private IPlayerGateway playerGateway;

        public SendRematchNotification(IPlayerGateway playerGateway)
        {
            this.playerGateway = playerGateway;
        }
        public async Task Execute(string senderPlayerID, string receiverPlayerID)
        {
             await playerGateway.SendRematchNotification(senderPlayerID,receiverPlayerID);
        }
    }
}