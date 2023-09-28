using Gateways.Interfaces;

namespace Providers
{
    public interface IGatewayProvider
    {
        IPlayerGateway ProvidePlayerGateway(); 
        IRoundGateway ProvideRoundGateway();
        ITurnGateway ProvideTurnGateway();
        IMatchGateway ProvideMatchGateway();

        void DisposeClient();
    }

    
}