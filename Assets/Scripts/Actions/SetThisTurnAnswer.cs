using System.Threading.Tasks;
using DTOs;
using Gateways;
using Gateways.Interfaces;

namespace Actions
{
    public class SetThisTurnAnswer
    {
        private ITurnGateway turnGateway;
        public SetThisTurnAnswer(ITurnGateway turnGateway) { 
        
            this.turnGateway = turnGateway;
        }

        public async Task<bool> Execute(string matchID, int roundID, int turnID,string word,string categoryName, char letter)
        {
            AnswerPost answerPost = new AnswerPost(categoryName, word, letter);
            return await turnGateway.SetAnswerForTurn(matchID,roundID,turnID,answerPost);
        }
    }
}