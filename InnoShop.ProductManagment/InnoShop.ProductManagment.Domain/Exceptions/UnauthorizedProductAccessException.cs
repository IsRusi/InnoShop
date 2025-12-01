namespace InnoShop.ProductManagment.Domain.Exceptions
{
    public class UnauthorizedProductAccessException : DomainException
    {
        public UnauthorizedProductAccessException(string message) : base(message)
        {
        }
    }
}