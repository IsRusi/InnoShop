
using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Domain.Models;
using InnoShop.UserManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.UserManager.Infrastructure.Repositories
{
    internal class UserRepository(UserContext userContext) : IUserRepository
    {
        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await userContext.Users.AddAsync(user, cancellationToken);
            await userContext.SaveChangesAsync(cancellationToken);
        }


        public async Task<bool> DeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            var affectedRows = await userContext.Users
        .Where(u => u.Id == user.Id)
        .ExecuteDeleteAsync(cancellationToken);
            return affectedRows > 0;
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
            => await userContext.Users.AnyAsync(user => user.Email.Equals(email), cancellationToken);


        public Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new Exception();
        }
        //=> userContext.Users.AnyAsync(user => );

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
        => await userContext.Users.ToListAsync(cancellationToken);

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await userContext.Users.FirstOrDefaultAsync(user => user.Email.Equals(email), cancellationToken);

        public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await userContext.Users.FirstOrDefaultAsync(user => user.Id == id, cancellationToken)!;

        public async Task<bool> UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            userContext.Users.Update(user);
            var affectedRows = await userContext.SaveChangesAsync(cancellationToken);
            return affectedRows > 0;
        }
    }
}
