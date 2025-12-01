namespace InnoShop.UserManager.Domain.Interfaces.IService
{
    public interface IEmailService
    {
        Task SendConfirmationCodeAsync(string toEmail, string code);

        Task SendResetCodeAsync(string toEmail, string tokenWithUserId, string link);
    }
}