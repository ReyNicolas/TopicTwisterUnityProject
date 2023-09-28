using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Gateways.Interfaces
{
    public interface IPlayerGateway
    {
        Task<List<MatchResultDTO>> GetThisMatchHistory(string playerID);
        Task<List<MatchInfoDTO>> GetThisMatchesToPlay(string playerID);
        Task<List<ForGameNotificationDTO>> GetPlayerForGameNotification(string playerId);
        Task<List<PlayerRematchNotificationDTO>> GetPlayerRematchNotifications(string playerID);
        Task<PlayerStats> GetPlayerStats(string playerID);
        Task<LoginResultDTO> CheckLoginData(LoginDto loginDto);
        Task<string> FindNewRivalID(string playerID);
        Task RemovePlayerRematchNotification(PlayerRematchNotificationDTO playerRematchNotification);
        Task<LoginResultDTO> CheckRegisterData(LoginDto loginDto);
        Task SendRematchNotification(string senderPlayerID, string receiverPlayerID);
        Task RemovePlayerForGameNotification(string playerID, string notificationsId);
    }
}