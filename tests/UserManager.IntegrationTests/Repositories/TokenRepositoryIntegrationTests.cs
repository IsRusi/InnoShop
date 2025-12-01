using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using InnoShop.UserManager.Domain.Models;
using InnoShop.UserManager.Infrastructure.Data;
using InnoShop.UserManager.Infrastructure.Repositories;

namespace UserManager.IntegrationTests.Repositories
{
    public class TokenRepositoryIntegrationTests
    {
        private UserContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new UserContext(options);
        }

        [Fact]
        public async Task RefreshTokenRepository_AddAsync_ShouldAddToken()
        {
            var dbContext = CreateDbContext();
            var repository = new RefreshTokenRepository(dbContext);
            var userId = Guid.NewGuid();
            var token = RefreshToken.Create(userId, "test_token", DateTime.UtcNow.AddDays(7));

            await repository.AddAsync(token);

            var saved = await dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Id == token.Id);
            saved.Should().NotBeNull();
            saved!.IsRevoked.Should().BeFalse();
        }

        [Fact]
        public async Task RefreshTokenRepository_GetByTokenAsync_ShouldReturnToken()
        {
            var dbContext = CreateDbContext();
            var repository = new RefreshTokenRepository(dbContext);
            var userId = Guid.NewGuid();
            var token = RefreshToken.Create(userId, "test_token", DateTime.UtcNow.AddDays(7));
            await repository.AddAsync(token);

            var result = await repository.GetByTokenAsync("test_token");

            result.Should().NotBeNull();
            result!.Token.Should().Be("test_token");
        }

        [Fact]
        public async Task RefreshTokenRepository_UpdateAsync_ShouldRevokeToken()
        {
            var dbContext = CreateDbContext();
            var repository = new RefreshTokenRepository(dbContext);
            var userId = Guid.NewGuid();
            var token = RefreshToken.Create(userId, "test_token", DateTime.UtcNow.AddDays(7));
            await repository.AddAsync(token);

            token.Revoke();
            await repository.UpdateAsync(token);

            var updated = await dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Id == token.Id);
            updated!.IsRevoked.Should().BeTrue();
        }

        [Fact]
        public async Task PasswordResetTokenRepository_AddAsync_ShouldAddToken()
        {
            var dbContext = CreateDbContext();
            var repository = new PasswordResetTokenRepository(dbContext);
            var userId = Guid.NewGuid();
            var token = PasswordResetToken.Create(userId, "reset_token", DateTime.UtcNow.AddHours(1));

            await repository.AddAsync(token);

            var saved = await dbContext.PasswordResetTokens.FirstOrDefaultAsync(t => t.Id == token.Id);
            saved.Should().NotBeNull();
        }

        [Fact]
        public async Task PasswordResetTokenRepository_GetByTokenAsync_ShouldReturnToken()
        {
            var dbContext = CreateDbContext();
            var repository = new PasswordResetTokenRepository(dbContext);
            var userId = Guid.NewGuid();
            var token = PasswordResetToken.Create(userId, "reset_token", DateTime.UtcNow.AddHours(1));
            await repository.AddAsync(token);

            var result = await repository.GetByTokenAsync("reset_token");

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task EmailConfirmationTokenRepository_AddAsync_ShouldAddToken()
        {
            var dbContext = CreateDbContext();
            var repository = new EmailConfirmationTokenRepository(dbContext);
            var userId = Guid.NewGuid();
            var token = EmailConfirmationToken.Create(userId, "confirm_token", DateTime.UtcNow.AddHours(24));

            await repository.AddAsync(token);

            var saved = await dbContext.EmailConfirmationTokens.FirstOrDefaultAsync(t => t.Id == token.Id);
            saved.Should().NotBeNull();
        }

        [Fact]
        public async Task EmailConfirmationTokenRepository_GetByTokenAsync_ShouldReturnToken()
        {
            var dbContext = CreateDbContext();
            var repository = new EmailConfirmationTokenRepository(dbContext);
            var userId = Guid.NewGuid();
            var token = EmailConfirmationToken.Create(userId, "confirm_token", DateTime.UtcNow.AddHours(24));
            await repository.AddAsync(token);

            var result = await repository.GetByTokenAsync("confirm_token");

            result.Should().NotBeNull();
        }
    }
}
