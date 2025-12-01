using InnoShop.UserManager.Domain.Models;

namespace InnoShop.UserManager.Application.Interfaces.IRepository
{
    public interface IPasswordResetTokenRepository
    {
        Task<PasswordResetToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);

        Task<PasswordResetToken?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<PasswordResetToken>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        Task AddAsync(PasswordResetToken token, CancellationToken cancellationToken = default);

        Task UpdateAsync(PasswordResetToken token, CancellationToken cancellationToken = default);

        Task DeleteAsync(PasswordResetToken token, CancellationToken cancellationToken = default);

        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}