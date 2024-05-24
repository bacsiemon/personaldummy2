namespace Services.UserServices.ResponseEntities
{
    public class LoginResponseEntity
    {
        public string? Username { get; set; }
        public string? Email { get; set; }

        public string? ErrorMessage { get; set; }

        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
