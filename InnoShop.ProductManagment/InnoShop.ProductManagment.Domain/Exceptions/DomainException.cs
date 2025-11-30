namespace InnoShop.ProductManagment.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }

    public class ProductNotFoundException : DomainException
    {
        public ProductNotFoundException(string message) : base(message)
        {
        }
    }

    public class UnauthorizedProductAccessException : DomainException
    {
        public UnauthorizedProductAccessException(string message) : base(message)
        {
        }
    }

    public class InvalidPriceException : DomainException
    {
        public InvalidPriceException(string message) : base(message)
        {
        }
    }

    public class AlreadyDoneException : DomainException
    {
        public AlreadyDoneException(string message) : base(message)
        {
        }
    }
}
