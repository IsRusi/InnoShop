namespace InnoShop.ProductManagment.Domain.Common
{
    public static class ErrorMessages
    {
        public const string ProductNotFound = "Product not found";
        public const string UnauthorizedAccess = "You are not authorized to perform this action";
        public const string InvalidPrice = "Price must be greater than zero";
        public const string AlreadyDeleted = "Product deleted";
        public const string AlreadyUnDeleted = "Product not deleted";
        public const string AlreadyAvailable = "Product is avaiable";
        public const string AlreadyUnAvailable = "Product not avaiable";

        // Search validation messages
        public const string SearchTermTooLong = "Search term must not exceed 500 characters";
        public const string MinPriceNegative = "Minimum price cannot be negative";
        public const string MaxPriceNegative = "Maximum price cannot be negative";
        public const string PriceRangeInvalid = "Minimum price cannot be greater than maximum price";
        public const string UserIdEmpty = "UserId cannot be an empty GUID";
    }
}