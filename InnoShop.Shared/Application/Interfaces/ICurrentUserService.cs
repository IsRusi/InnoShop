namespace InnoShop.Shared.Application.Interfaces
{
    public interface ICurrentUserService
    {
        public Guid? UserId { get; }
        public string? Email { get; }
    }
}
