using InnoShop.UserManager.Domain.Models;

namespace InnoShop.UserManager.Application.Interfaces.IRepository
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);

        Task<RefreshToken?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<RefreshToken>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        Task AddAsync(RefreshToken token, CancellationToken cancellationToken = default);

        Task UpdateAsync(RefreshToken token, CancellationToken cancellationToken = default);

        Task DeleteAsync(RefreshToken token, CancellationToken cancellationToken = default);

        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}