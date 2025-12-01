using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Domain.Models;
using InnoShop.UserManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.UserManager.Infrastructure.Repositories
{
    public class RefreshTokenRepository(UserContext userContext) : IRefreshTokenRepository
    {
        public async Task AddAsync(RefreshToken token, CancellationToken cancellationToken = default)
        {
            await userContext.RefreshTokens.AddAsync(token, cancellationToken);
            await userContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(RefreshToken token, CancellationToken cancellationToken = default)
        {
            userContext.RefreshTokens.Remove(token);
            await userContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<RefreshToken?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await userContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Id == id, cancellationToken);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await userContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
        }

        public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await userContext.RefreshTokens
                .Where(rt => rt.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(RefreshToken token, CancellationToken cancellationToken = default)
        {
            userContext.RefreshTokens.Update(token);
            await userContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await userContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}