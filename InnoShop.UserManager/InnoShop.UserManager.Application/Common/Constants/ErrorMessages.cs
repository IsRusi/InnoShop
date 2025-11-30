namespace InnoShop.UserManager.Application.Common.Constants
{
    public static class ErrorMessages
    {
        public const string IdMustBeGreaterThanZero = "Идентификатор должен быть больше нуля.";
        public const string NameIsRequired = "Имя является обязательным для заполнения.";
        public const string NameMaxLengthExceeded = "Длина имени не должна превышать {0} символов.";
        public const string EmailIsRequired = "Адрес электронной почты является обязательным для заполнения.";
        public const string IncorrectEmailFormat = "Указан некорректный формат адреса электронной почты.";
        public const string PasswordIsRequired = "Пароль является обязательным для заполнения.";
        public const string PasswordMustBeAtLeast = "Пароль должен быть не короче {0} символов.";
        public const string PasswordMustContainUpper = "Пароль должен содержать как минимум одну заглавную букву.";
        public const string PasswordMustContainLower = "Пароль должен содержать как минимум одну строчную букву.";
        public const string PasswordMustContainDigit = "Пароль должен содержать как минимум одну цифру.";
        public const string EmailAlreadyExists = "Указанный адрес электронной почты уже зарегистрирован в системе.";
        public const string EmailNotConfirmed = "Указанный адрес электронной почты не подтверждён.";
        public const string IncorrectPassword = "Введён неверный Email или пароль.";
        public const string CurrentPasswordIsRequired = "Текущий пароль является обязательным для заполнения.";
        public const string RefreshTokenIsRequired = "Необходимо предоставить refresh-токен.";
        public const string TokenIsRequired = "Необходимо предоставить токен.";
        public const string IncorrectToken = "Указан некорректный токен.";
        public const string TokenIsExpiredOrRevoked = "Токен просрочен или был отозван.";
        public const string UserNotFound = "Пользователь с указанными данными не найден.";
        public const string UserNotActive = "Пользователь не активен.";
        public const string UserIdIsRequired = "UserId является обязательным для заполнения.";
        public const string UserDataIsRequired = "Данные пользователя обязательны.";
        public const string RoleIsRequired = "Роль пользователя обязательна.";
        public const string ConfirmationTokenIsRequired = "Токен подтверждения обязателен.";
        public const string TokenNotFound = "Токен не найден.";
    }
}
