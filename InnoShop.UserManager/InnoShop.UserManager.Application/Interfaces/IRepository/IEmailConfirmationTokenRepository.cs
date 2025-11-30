using InnoShop.UserManager.Domain.Models;

namespace InnoShop.UserManager.Application.Interfaces.IRepository
{
    public interface IEmailConfirmationTokenRepository
    {
        Task<EmailConfirmationToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
        Task<EmailConfirmationToken?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<EmailConfirmationToken>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task AddAsync(EmailConfirmationToken token, CancellationToken cancellationToken = default);
        Task UpdateAsync(EmailConfirmationToken token, CancellationToken cancellationToken = default);
        Task DeleteAsync(EmailConfirmationToken token, CancellationToken cancellationToken = default);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
