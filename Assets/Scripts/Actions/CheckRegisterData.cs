using System.Threading.Tasks;
using DTOs;
using Gateways.Interfaces;

namespace Actions
{
    public class CheckRegisterData
    {
        private IPlayerGateway playerGateway;
        public CheckRegisterData(IPlayerGateway playerGateway) { 
        
            this.playerGateway = playerGateway;
        }

        public async Task<LoginResultDTO> Execute(LoginDto loginDto)
        {
            return await playerGateway.CheckRegisterData(loginDto);
        }
    }
}