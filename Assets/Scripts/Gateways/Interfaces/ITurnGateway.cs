using System.Threading.Tasks;
using DTOs;

namespace Gateways.Interfaces
{
    public interface ITurnGateway
    {
        Task<StartTurnDTO> GetStartTurnData(string matchID, int roundID, string playerID);
        Task<bool> EndThisTurn(string matchID, int roundID, int turnID, float timeLeft);

        Task<bool> SetAnswerForTurn(string matchID, int roundID, int turnID, AnswerPost answer);

        Task<TurnResultDTO> ReturnTurnResult(string matchID, int roundID, int turnID);
    }
}