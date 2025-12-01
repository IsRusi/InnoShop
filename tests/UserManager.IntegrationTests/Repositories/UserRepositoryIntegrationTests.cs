using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using InnoShop.UserManager.Domain.Models;
using InnoShop.UserManager.Infrastructure.Data;
using InnoShop.UserManager.Infrastructure.Repositories;

namespace UserManager.IntegrationTests.Repositories
{
    public class UserRepositoryIntegrationTests
    {
        private UserContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new UserContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddUser()
        {
            var dbContext = CreateDbContext();
            var repository = new UserRepository(dbContext);
            var user = new User { FirstName = "John", LastName = "Doe", Email = "john@example.com", Role = "User", PasswordHash = "hash" };

            await repository.AddAsync(user);

            var saved = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            saved.Should().NotBeNull();
            saved!.Email.Should().Be("john@example.com");
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnUser()
        {
            var dbContext = CreateDbContext();
            var repository = new UserRepository(dbContext);
            var user = new User { FirstName = "John", LastName = "Doe", Email = "john@example.com", Role = "User", PasswordHash = "hash" };
            await repository.AddAsync(user);

            var result = await repository.GetByEmailAsync("john@example.com");

            result.Should().NotBeNull();
            result!.Email.Should().Be("john@example.com");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser()
        {
            var dbContext = CreateDbContext();
            var repository = new UserRepository(dbContext);
            var user = new User { FirstName = "John", LastName = "Doe", Email = "john@example.com", Role = "User", PasswordHash = "hash" };
            await repository.AddAsync(user);

            var result = await repository.GetByIdAsync(user.Id);

            result.Should().NotBeNull();
            result!.Email.Should().Be("john@example.com");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateUser()
        {
            var dbContext = CreateDbContext();
            var repository = new UserRepository(dbContext);
            var user = new User { FirstName = "John", LastName = "Doe", Email = "john@example.com", Role = "User", PasswordHash = "hash" };
            await repository.AddAsync(user);

            user.FirstName = "Jane";
            await repository.UpdateAsync(user);

            var updated = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            updated!.FirstName.Should().Be("Jane");
        }

        [Fact]
        public async Task ExistsByEmailAsync_ShouldReturnTrue()
        {
            var dbContext = CreateDbContext();
            var repository = new UserRepository(dbContext);
            var user = new User { FirstName = "John", LastName = "Doe", Email = "john@example.com", Role = "User", PasswordHash = "hash" };
            await repository.AddAsync(user);

            var result = await repository.ExistsByEmailAsync("john@example.com");

            result.Should().BeTrue();
        }
    }
}
