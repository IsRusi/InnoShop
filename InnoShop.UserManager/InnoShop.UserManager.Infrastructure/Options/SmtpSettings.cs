namespace InnoShop.UserManager.Infrastructure.Options
{
    public class SmtpSettings
    {
        public const string section = "Smtp";
        public string Host { get; init; }
        public int Port { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public string FromAddress { get; init; }
    }
}