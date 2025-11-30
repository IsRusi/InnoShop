namespace InnoShop.ProductManagment.Application.Common
{
    public static class ErrorMessages
    {
        public const string ProductNotFound = "Product not found";
        public const string UnauthorizedAccess = "You are not authorized to perform this action";
        public const string InvalidPrice = "Price must be greater than zero";
        public const string NameRequired = "Name is required";
        public const string NameTooShort = "Name must be at least 3 characters long";
        public const string NameTooLong = "Name cannot exceed 100 characters";
        public const string DescriptionRequired = "Description is required";
        public const string DescriptionTooLong = "Description cannot exceed 500 characters";
        public const string PriceRequired = "Price is required";
        public const string AlreadyActivated = "Product is already available";
        public const string AlreadyDeactivated = "Product is already unavailable";
        public const string AlreadyDeleted = "Product is already deleted";
        public const string AlreadyRecovered = "Product is already active";
    }
}
