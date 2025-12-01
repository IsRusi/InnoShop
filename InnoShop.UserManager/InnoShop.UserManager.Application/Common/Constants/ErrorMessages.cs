namespace InnoShop.UserManager.Application.Common.Constants
{
    public static class ErrorMessages
    {
        public const string IdMustBeGreaterThanZero = "Identifier must be greater than zero.";
        public const string NameIsRequired = "Name is required.";
        public const string NameMaxLengthExceeded = "Name length must not exceed {0} characters.";
        public const string EmailIsRequired = "Email address is required.";
        public const string IncorrectEmailFormat = "Invalid email address format.";
        public const string PasswordIsRequired = "Password is required.";
        public const string PasswordMustBeAtLeast = "Password must be at least {0} characters long.";
        public const string PasswordMustContainUpper = "Password must contain at least one uppercase letter.";
        public const string PasswordMustContainLower = "Password must contain at least one lowercase letter.";
        public const string PasswordMustContainDigit = "Password must contain at least one digit.";
        public const string EmailAlreadyExists = "The specified email address is already registered.";
        public const string EmailNotConfirmed = "The specified email address is not confirmed.";
        public const string IncorrectPassword = "Incorrect email or password.";
        public const string CurrentPasswordIsRequired = "Current password is required.";
        public const string RefreshTokenIsRequired = "Refresh token is required.";
        public const string TokenIsRequired = "Token is required.";
        public const string IncorrectToken = "Invalid token.";
        public const string TokenIsExpiredOrRevoked = "Token has expired or has been revoked.";
        public const string UserNotFound = "User with the specified data was not found.";
        public const string UserNotActive = "User is not active.";
        public const string UserIdIsRequired = "UserId is required.";
        public const string UserDataIsRequired = "User data is required.";
        public const string RoleIsRequired = "User role is required.";
        public const string ConfirmationTokenIsRequired = "Confirmation token is required.";
        public const string TokenNotFound = "Token not found.";
    }
}