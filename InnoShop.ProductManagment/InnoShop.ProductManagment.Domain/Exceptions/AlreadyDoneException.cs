namespace InnoShop.ProductManagment.Domain.Exceptions
{
    public class AlreadyDoneException : DomainException
    {
        public AlreadyDoneException(string message) : base(message)
        {
        }
    }
}