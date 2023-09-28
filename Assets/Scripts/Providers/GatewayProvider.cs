using Gateways;
using Gateways.Interfaces;
using System.Net.Http;

namespace Providers
{
    public  class GatewayProvider : IGatewayProvider
    {
        private IPlayerGateway playerGateway;
        private IMatchGateway matchGateway;
        private IRoundGateway roundGateway;
        private ITurnGateway turnGateway;
        HttpClient client;

        public GatewayProvider()
        {
            client = new HttpClient();
            playerGateway = new PlayerGateway(client);
            matchGateway = new MatchGateway(client);
            roundGateway = new RoundGateway(client);
            turnGateway = new TurnGateway(client);
        }
        
        public IPlayerGateway ProvidePlayerGateway()
        {
            return playerGateway;
        }

        public IRoundGateway ProvideRoundGateway()
        {
            return roundGateway;
        }

        public ITurnGateway ProvideTurnGateway()
        {
            return turnGateway;
        }

        public IMatchGateway ProvideMatchGateway()
        {
            return matchGateway;
        }

        public void DisposeClient()
        {
            client?.CancelPendingRequests();
            client?.Dispose();              
        }
    }
}
