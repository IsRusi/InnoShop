namespace InnoShop.UserManager.Infrastructure.Options
{
    public class SmtpSettings
    {
        public const string section = "Smtp";
        public required string Host { get; init; }
        public required int Port { get; init; }
        public required string Username { get; init; }
        public required string Password { get; init; }
        public required string FromAddress { get; init; }
    }
}
