using InnoShop.UserManager.Domain.Models;

namespace InnoShop.UserManager.Domain.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
    }
}
