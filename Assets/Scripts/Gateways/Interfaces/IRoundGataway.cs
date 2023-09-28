using System.Threading.Tasks;
using DTOs;

namespace Gateways.Interfaces
{
    public interface IRoundGateway
    {
        Task<bool> CheckIfRoundIsOver(string matchID,int roundID);
        Task<bool> EndThisRound(string matchID, int roundID);
        Task<RoundResultDTO> GetRoundResultForPlayer(string matchID, int roundID, string playerID);

    }
}