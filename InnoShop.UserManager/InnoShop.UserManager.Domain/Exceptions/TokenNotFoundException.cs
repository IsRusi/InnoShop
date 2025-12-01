namespace InnoShop.UserManager.Domain.Exceptions
{
    public class TokenNotFoundException : DomainException
    {
        public TokenNotFoundException(string message) : base(message)
        {
        }
    }
}