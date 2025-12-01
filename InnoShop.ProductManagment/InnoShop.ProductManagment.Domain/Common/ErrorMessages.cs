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
    }
}