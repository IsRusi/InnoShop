namespace InnoShop.UserManager.Domain.Exceptions
{
    public class InvalidCredentialsException : DomainException
    {
        public InvalidCredentialsException(string message)
            : base(message)
        {
        }
    }
}
