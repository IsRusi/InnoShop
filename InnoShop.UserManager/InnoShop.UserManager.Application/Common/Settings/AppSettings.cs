namespace InnoShop.UserManager.Application.Common.Settings
{
    public class AppSettings
    {
        public const string section = "AppSettings";
        public string FrontendUrl { get; init; }
        public string BackendUrl { get; init; }
    }
}