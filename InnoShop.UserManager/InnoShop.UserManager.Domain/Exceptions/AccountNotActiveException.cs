namespace InnoShop.UserManager.Domain.Exceptions
{
    public class AccountNotActiveException : DomainException
    {
        public AccountNotActiveException(string message = "Account is not active")
            : base(message)
        {
        }
    }
}