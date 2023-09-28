using System.Threading.Tasks;
using DTOs;
using Gateways.Interfaces;

namespace Actions
{
    public class CheckLoginData
    {
        private IPlayerGateway playerGateway;
        public CheckLoginData(IPlayerGateway playerGateway) { 
        
            this.playerGateway = playerGateway;
        }

        public async Task<LoginResultDTO> Execute(LoginDto loginDto)
        {
            return await playerGateway.CheckLoginData(loginDto);
        }
    }
}