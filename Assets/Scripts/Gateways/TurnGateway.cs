using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using Gateways.Interfaces;
using Newtonsoft.Json;

namespace Gateways
{
    public class TurnGateway: ITurnGateway
    {
        private HttpClient client;
        private string url;

        public TurnGateway(HttpClient client)
        {
            this.client = client;
            url = Urls.TurnUrl();
        }

        public async Task<StartTurnDTO> GetStartTurnData(string matchID, int roundID, string playerID)
        {
            var response = await client.GetAsync($"{url}/{matchID}/{roundID}/{playerID}/GetTurn");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<StartTurnDTO>(content);
            }

            throw new Exception(response.RequestMessage.ToString());
        }

        public async Task<bool> EndThisTurn(string matchID, int roundID, int turnID, float timeLeft)
        {
            var response = await client.GetAsync($"{url}/{matchID}/{roundID}/{turnID}/EndTurn?timeLeft={timeLeft.ToString(CultureInfo.InvariantCulture) }");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            throw new Exception(response.RequestMessage.ToString());
        }

        public async Task<bool> SetAnswerForTurn(string matchID, int roundID, int turnID, AnswerPost answer)
        {
            string data = JsonConvert.SerializeObject(answer);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{url}/{matchID}/{roundID}/{turnID}/SetAnswer", content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            throw new Exception(response.RequestMessage.ToString());
        }

        public async Task<TurnResultDTO> ReturnTurnResult(string matchID, int roundID, int turnID)
        {
            var response = await client.GetAsync($"{url}/{matchID}/{roundID}/{turnID}/GetTurnResult");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TurnResultDTO>(content);
            }
            throw new Exception(response.RequestMessage.ToString());
        }
    }

    
}