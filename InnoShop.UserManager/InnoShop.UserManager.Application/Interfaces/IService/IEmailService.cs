namespace InnoShop.UserManager.Domain.Interfaces.IService
{
    public interface IEmailService
    {
        Task SendConfirmationCodeAsync(string toEmail, string code);
    }
}
