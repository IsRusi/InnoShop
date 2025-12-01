namespace InnoShop.UserManager.Application.Interfaces.IService
{
    public interface IPasswordHasher
    {
        string PasswordHash(string password);

        bool VerifyPasswordHash(string password, string hash);
    }
}