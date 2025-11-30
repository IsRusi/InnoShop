namespace InnoShop.UserManager.Domain.Exceptions
{
    public class UserAlreadyExistsException : DomainException
    {
        public UserAlreadyExistsException(string message = "User already exists")
            : base(message)
        {
        }
    }
}
