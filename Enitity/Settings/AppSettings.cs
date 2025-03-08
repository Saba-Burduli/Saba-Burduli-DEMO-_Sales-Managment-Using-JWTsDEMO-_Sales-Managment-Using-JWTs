using System;
using System.Collections.Generic;
using System.Text;

namespace SalesManagementSystem.DATA.Settings
{
    public class AppSettings : IAppSettings
    {
        public string? Secret { get; set; }
        public string? PasswordHashSecret { get; set; }
    }
}
