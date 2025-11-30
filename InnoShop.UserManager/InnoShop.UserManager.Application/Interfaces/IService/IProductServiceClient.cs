namespace InnoShop.UserManager.Application.Interfaces.IService
{
    public interface IProductServiceClient
    {
        Task DeactivateProductsAsync(Guid userId, CancellationToken cancellationToken = default);
        Task RecoverProductsAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
