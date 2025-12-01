namespace InnoShop.UserManager.Domain.Exceptions
{
    public class EmailNotConfirmedException : DomainException
    {
        public EmailNotConfirmedException(string message = "Email is not confirmed")
            : base(message)
        {
        }
    }
}