using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Domain.Models;
using InnoShop.UserManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.UserManager.Infrastructure.Repositories
{
    internal class EmailConfirmationTokenRepository(UserContext userContext) : IEmailConfirmationTokenRepository
    {
        public async Task AddAsync(EmailConfirmationToken token, CancellationToken cancellationToken = default)
        {
            await userContext.EmailConfirmationTokens.AddAsync(token, cancellationToken);
            await userContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(EmailConfirmationToken token, CancellationToken cancellationToken = default)
        {
            userContext.EmailConfirmationTokens.Remove(token);
            await userContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<EmailConfirmationToken?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await userContext.EmailConfirmationTokens.FirstOrDefaultAsync(ect => ect.Id == id, cancellationToken);
        }

        public async Task<EmailConfirmationToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await userContext.EmailConfirmationTokens.FirstOrDefaultAsync(ect => ect.Token == token, cancellationToken);
        }

        public async Task<IEnumerable<EmailConfirmationToken>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await userContext.EmailConfirmationTokens
                .Where(ect => ect.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(EmailConfirmationToken token, CancellationToken cancellationToken = default)
        {
            userContext.EmailConfirmationTokens.Update(token);
            await userContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await userContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
