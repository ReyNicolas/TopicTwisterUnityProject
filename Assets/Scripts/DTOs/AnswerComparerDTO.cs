using System;

namespace DTOs
{
    public class AnswerComparerDTO
    {
        public WordAndResultDTO PlayerWordResult;
        public WordAndResultDTO RivalWordResult;
        public string CategoryName;

        public AnswerComparerDTO(WordAndResultDTO playerWordResult, WordAndResultDTO rivalWordResult, string categoryName)
        {
            PlayerWordResult = playerWordResult;
            RivalWordResult = rivalWordResult;
            CategoryName = categoryName;
        }
    }
    [Serializable]
    public class RegisterPlayerData
    {
        public string id;
        public string password;

        public RegisterPlayerData(string id, string password)
        {
            this.id = id;
            this.password = password;
        }
    }
}