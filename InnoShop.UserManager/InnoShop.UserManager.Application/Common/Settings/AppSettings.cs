using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.UserManager.Application.Common.Settings
{
    public class AppSettings
    {
        public const string section = "AppSettings";
        public required string FrontendUrl { get; init; }
        public required string BackendUrl { get; init; }
    }
}
