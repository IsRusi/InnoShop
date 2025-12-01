using InnoShop.UserManager.Application.Interfaces.IService;

namespace InnoShop.UserManager.Infrastructure.Clients
{
    public class ProductServiceClient : IProductServiceClient
    {
        private readonly HttpClient _httpClient;

        public ProductServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeactivateProductsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsync($"/api/products/users/{userId}/deactivate", null, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task RecoverProductsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsync($"/api/products/users/{userId}/recover", null, cancellationToken);
            response.EnsureSuccessStatusCode();
        }
    }
}