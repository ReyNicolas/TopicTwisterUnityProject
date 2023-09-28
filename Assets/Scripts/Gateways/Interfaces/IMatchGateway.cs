using System.Threading.Tasks;
using DTOs;

namespace Gateways.Interfaces
{
    public interface IMatchGateway
    {
        Task<bool> CheckIfMatchIsOver(string matchID);
        Task<bool> EndThisMatch(string matchID);
        Task<MatchResultDTO> GetMatchResultForPlayer(string matchID,string playerID);
        Task<bool> InThisMatchIsThisPlayerTurn(string matchID, string playerID);
        Task<string> CreateMatch(MatchConfigurationDTO matchConfig);
        Task<DataApplication> GetMatchActualTurnData(string matchID);

        Task SendMatchChangeNotificationsToPlayers(string matchID);
        Task SendEndMatchNotificationsToPlayers(string matchID);


    }
}