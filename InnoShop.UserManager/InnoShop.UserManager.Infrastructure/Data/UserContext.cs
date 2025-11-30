using InnoShop.UserManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.UserManager.Infrastructure.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        public DbSet<EmailConfirmationToken> EmailConfirmationTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User -> RefreshTokens relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.RefreshTokens)
                .WithOne(rt => rt.User)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure User -> PasswordResetTokens relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.PasswordResetTokens)
                .WithOne(prt => prt.User)
                .HasForeignKey(prt => prt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure User -> EmailConfirmationTokens relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.EmailConfirmationTokens)
                .WithOne(ect => ect.User)
                .HasForeignKey(ect => ect.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}