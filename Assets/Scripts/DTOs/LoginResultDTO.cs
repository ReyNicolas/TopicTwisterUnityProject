namespace DTOs
{
    public class LoginResultDTO
    {
        public string ErrorMessage;
        public readonly bool SuccessAuthentication;

        public LoginResultDTO(string errorMessage, bool successAuthentication)
        {
            ErrorMessage = errorMessage;
            SuccessAuthentication = successAuthentication;
        }
    }
}