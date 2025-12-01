using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using InnoShop.ProductManagment.Domain.Models;
using InnoShop.ProductManagment.Infrastructure.Data;
using InnoShop.ProductManagment.Infrastructure.Repositories;

namespace ProductManagment.IntegrationTests.Repositories
{
    public class ProductRepositoryIntegrationTests
    {
        private ProductManagementDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ProductManagementDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ProductManagementDbContext(options);
        }

        [Fact]
        public async Task AddProductAsync_ShouldAddProduct()
        {
            var dbContext = CreateDbContext();
            var repository = new ProductRepository(dbContext);
            var product = new Product("Test", "Desc", Guid.NewGuid(), 99.99);

            await repository.AddProductAsync(product);

            var saved = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            saved.Should().NotBeNull();
            saved!.Name.Should().Be("Test");
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnProduct()
        {
            var dbContext = CreateDbContext();
            var repository = new ProductRepository(dbContext);
            var product = new Product("Test", "Desc", Guid.NewGuid(), 99.99);
            await repository.AddProductAsync(product);

            var result = await repository.GetProductByIdAsync(product.Id);

            result.Should().NotBeNull();
            result!.Id.Should().Be(product.Id);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldNotReturnDeleted()
        {
            var dbContext = CreateDbContext();
            var repository = new ProductRepository(dbContext);
            var product = new Product("Test", "Desc", Guid.NewGuid(), 99.99);
            await repository.AddProductAsync(product);
            await repository.DeleteProductAsync(product.Id);

            var result = await repository.GetProductByIdAsync(product.Id);

            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldUpdateProduct()
        {
            var dbContext = CreateDbContext();
            var repository = new ProductRepository(dbContext);
            var product = new Product("Original", "Desc", Guid.NewGuid(), 99.99);
            await repository.AddProductAsync(product);

            product.ChangeName("Updated");
            await repository.UpdateProductAsync(product);

            var updated = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            updated!.Name.Should().Be("Updated");
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldSoftDeleteProduct()
        {
            var dbContext = CreateDbContext();
            var repository = new ProductRepository(dbContext);
            var product = new Product("Test", "Desc", Guid.NewGuid(), 99.99);
            await repository.AddProductAsync(product);

            await repository.DeleteProductAsync(product.Id);

            var deleted = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            deleted!.IsDeleted.Should().BeTrue();
        }

        [Fact]
        public async Task RecoverProductAsync_ShouldRestoreProduct()
        {
            var dbContext = CreateDbContext();
            var repository = new ProductRepository(dbContext);
            var product = new Product("Test", "Desc", Guid.NewGuid(), 99.99);
            await repository.AddProductAsync(product);
            await repository.DeleteProductAsync(product.Id);

            await repository.RecoverProductAsync(product.Id);

            var recovered = await repository.GetProductByIdAsync(product.Id);
            recovered.Should().NotBeNull();
            recovered!.IsDeleted.Should().BeFalse();
        }
    }
}
