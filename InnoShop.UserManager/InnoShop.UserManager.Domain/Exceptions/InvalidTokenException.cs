namespace InnoShop.UserManager.Domain.Exceptions
{
    public class InvalidTokenException : DomainException
    {
        public InvalidTokenException(string message = "Invalid or expired token")
            : base(message)
        {
        }
    }
}
