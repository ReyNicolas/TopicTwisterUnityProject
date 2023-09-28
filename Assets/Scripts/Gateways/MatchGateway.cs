using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using Gateways.Interfaces;
using Newtonsoft.Json;

namespace Gateways
{
    public class MatchGateway: IMatchGateway
    {
        private HttpClient client;
        private string url;

        public MatchGateway(HttpClient client)
        {
            this.client = client;
            url = Urls.MatchUrl();
        }

        public async Task<DataApplication> GetMatchActualTurnData(string matchID)
        {
            var response = await client.GetAsync(url +"/" + matchID + "/GetDataToStartTurn" );
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DataApplication>(content);
                }
                
                throw new Exception(response.RequestMessage.ToString());
               
        }
        
        public async Task<bool> CheckIfMatchIsOver(string matchID)
        {
            var response = await client.GetAsync(url +"/" + matchID + "/CheckIfOver" );
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(content);
            }
            throw new Exception(response.RequestMessage.ToString());
        }
        
        public async Task<bool> EndThisMatch(string matchID)
        {
            var response = await client.GetAsync(url +"/" + matchID + "/EndMatch" );
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            throw new Exception(response.RequestMessage.ToString());
        }
        
        public async Task<MatchResultDTO> GetMatchResultForPlayer(string matchID,string playerID)
        {
            var response = await client.GetAsync(url +"/" + matchID + "/GetResult?playerID=" +playerID );
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MatchResultDTO>(content);
            }
            throw new Exception(response.RequestMessage.ToString());
        }
        
        public async Task<bool> InThisMatchIsThisPlayerTurn(string matchID,string playerID)
        {
            var response = await client.GetAsync(url +"/" + matchID + "/IsThisPlayerTurn?playerID=" +  playerID );
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(content);
            }
            throw new Exception(response.RequestMessage.ToString());
        }
      
        
        public async Task<string> CreateMatch(MatchConfigurationDTO matchConfig)
        {
            
            string data = JsonConvert.SerializeObject(matchConfig);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url,content);
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<string>(jsonResponse);   
            }
            throw new Exception(response.RequestMessage.ToString());
            
        }

        public async Task SendMatchChangeNotificationsToPlayers(string matchID)
        {
            var response = await client.GetAsync(url + "/" + matchID + "/SendMatchChangeNotificationToPlayers");
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new Exception(response.RequestMessage.ToString());
        }

        public async Task SendEndMatchNotificationsToPlayers(string matchID)
        {
            var response = await client.GetAsync(url + "/" + matchID + "/SendEndMatchNotificationToPlayers");
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new Exception(response.RequestMessage.ToString());
        }
    }
}


