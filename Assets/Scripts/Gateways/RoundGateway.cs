using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DTOs;
using Gateways.Interfaces;
using Newtonsoft.Json;


namespace Gateways
{
    public class RoundGateway: IRoundGateway
    {
        private HttpClient client;
        private string url;

        public RoundGateway(HttpClient client)
        {
            this.client = client;
            url = Urls.RoundUrl();
        }
        
       
        public async Task<bool> CheckIfRoundIsOver(string matchID,int roundID)
        {
            var response = await client.GetAsync($"{url}/{matchID}/{roundID}/CheckIfOver" );
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(content);
            }
            throw new Exception(response.RequestMessage.ToString());
        }
        
        
        public async Task<bool> EndThisRound(string matchID,int roundID)
        {
            var response = await client.GetAsync($"{url}/{matchID}/{roundID}/EndRound" );
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            throw new Exception(response.RequestMessage.ToString());
        }
        
        
        public async Task<RoundResultDTO> GetRoundResultForPlayer(string matchID,int roundID,string playerID)
        {
            var response = await client.GetAsync($"{url}/{matchID}/{roundID}/GetRoundResult?playerID={playerID}" );
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RoundResultDTO>(content);
            }
            throw new Exception(response.RequestMessage.ToString());
        }
    }
}