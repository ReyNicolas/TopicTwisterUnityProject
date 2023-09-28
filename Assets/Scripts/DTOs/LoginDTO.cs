using System;

namespace DTOs
{
    [Serializable]
    public class LoginDto
    {
        public string playerID;
        public string password;

        public LoginDto(string playerID, string password)
        {
            this.playerID = playerID;
            this.password = password;
        }
    }
}