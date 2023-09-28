using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using Gateways.Interfaces;
using Newtonsoft.Json;

namespace Gateways
{
    public class PlayerGateway: IPlayerGateway
    {
        private HttpClient client;
        private string url;

        public PlayerGateway(HttpClient client)
        {
            this.client = client;
            url = Urls.PlayerUrl();
        }

        public async Task<List<MatchResultDTO>> GetThisMatchHistory(string playerID)
        {
            var response = await client.GetAsync($"{url}/{playerID}/GetMatchHistory");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MatchResultDTO>>(content);
            }
            else
            {
                throw new Exception(await response.Content?.ReadAsStringAsync());  
            }
        }
        public async Task<List<MatchInfoDTO>> GetThisMatchesToPlay(string playerID)
        {
            var response = await client.GetAsync($"{url}/{playerID}/GetMatchesToPlay");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MatchInfoDTO>>(content);
            }
            else
            {
                throw new Exception( await response.Content.ReadAsStringAsync());
            }
        }

       

        public async Task<List<ForGameNotificationDTO>> GetPlayerForGameNotification(string playerId)
        {
            var response = await client.GetAsync($"{url}/{playerId}/GetPlayerForGameNotifications");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ForGameNotificationDTO>>(content);
            }
            else
            {
                throw new Exception( await response.Content.ReadAsStringAsync());
            } 
        }


        
        public async Task<List<PlayerRematchNotificationDTO>> GetPlayerRematchNotifications(string playerId)
        {
            var response = await client.GetAsync($"{url}/{playerId}/GetPlayerRematchNotifications");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<PlayerRematchNotificationDTO>>(content);
            }
            else
            {
                throw new Exception( await response.Content.ReadAsStringAsync());
            } 
        }

        

        public async Task SendRematchNotification(string senderPlayerID,string receiverPlayerID)
        {
            var data = JsonConvert.SerializeObject(new RematchNotificationToSend(senderPlayerID,receiverPlayerID));
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{url}/{senderPlayerID}/SendRematchNotification", content);
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                return;
            }
            throw new Exception(response.RequestMessage.ToString());

        }

        public async Task<PlayerStats> GetPlayerStats(string playerID)
        {
            var response = await client.GetAsync($"{url}/{playerID}/GetPlayerStats");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PlayerStats>(content);
            }
            else
            {
                throw new Exception( await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<LoginResultDTO> CheckLoginData(LoginDto loginDto)
        {
            var response = await client.GetAsync($"{url}/CheckLogin?playerID="+loginDto.playerID+"&password="+loginDto.password);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoginResultDTO>(content);
            }
            else
            {
                throw new Exception();
            }
        }
        
        public async Task<string> FindNewRivalID(string playerID)
        {
            var response = await client.GetAsync($"{url}/{playerID}/FindNewRival");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<LoginResultDTO> CheckRegisterData(LoginDto loginDto)
        {
            var data = JsonConvert.SerializeObject(new RegisterPlayerData(loginDto.playerID, loginDto.password));
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url+"/RegisterPlayer",content);
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoginResultDTO>(jsonResponse);   
            }
            throw new Exception(response.RequestMessage.ToString()); 
            
        }

        public async Task RemovePlayerRematchNotification(PlayerRematchNotificationDTO playerRematchNotification)
        {
            
            var response = await client.
                DeleteAsync($"{url}/{playerRematchNotification.playerID}/RemovePlayerRematchNotification/{playerRematchNotification.rivalID}");
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                return;
            }
            throw new Exception(response.RequestMessage.ToString());
        }

        public async Task RemovePlayerForGameNotification(string playerID,string notificationId)
        {
            var response = await client.DeleteAsync($"{url}/{playerID}/RemovePlayerForGameNotification/{notificationId}");
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                return;
            }
            throw new Exception(response.RequestMessage.ToString());
        }
    }
    
}