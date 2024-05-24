namespace Services.UserServices.RequestEntities
{
    public class RegisterRequestEntity
    {
        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public string PrivateKey { get; set; } = string.Empty;
    }
}
