using InnoShop.UserManager.Domain.Interfaces.IRepository;
using InnoShop.UserManager.Domain.Models;

namespace InnoShop.UserManager.Infrastructure.Repositories
{
    internal class UserRepository : IUserRepository
    {
        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
