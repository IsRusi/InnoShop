using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Domain.Models;
using InnoShop.UserManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.UserManager.Infrastructure.Repositories
{
    public class PasswordResetTokenRepository(UserContext userContext) : IPasswordResetTokenRepository
    {
        public async Task AddAsync(PasswordResetToken token, CancellationToken cancellationToken = default)
        {
            await userContext.PasswordResetTokens.AddAsync(token, cancellationToken);
            await userContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(PasswordResetToken token, CancellationToken cancellationToken = default)
        {
            userContext.PasswordResetTokens.Remove(token);
            await userContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<PasswordResetToken?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await userContext.PasswordResetTokens.FirstOrDefaultAsync(prt => prt.Id == id, cancellationToken);
        }

        public async Task<PasswordResetToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await userContext.PasswordResetTokens.FirstOrDefaultAsync(prt => prt.Token == token, cancellationToken);
        }

        public async Task<IEnumerable<PasswordResetToken>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await userContext.PasswordResetTokens
                .Where(prt => prt.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(PasswordResetToken token, CancellationToken cancellationToken = default)
        {
            userContext.PasswordResetTokens.Update(token);
            await userContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await userContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}