using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Common
{
    public class AppSetting
    {
        public string Lang { get; set; } = "EN";
        public string DisplayTimeFormat { get; set; } = "hh\\:mm";
        public string DisplayDateFormat { get; set; } = "dd/MM/yyyy";
        public string DisplayDateTimeFormat { get; set; } = "dd/MM/yyyy HH:mm:ss";
        public int PageSize { get; set; } = 50;
        public string SecretKey { get; set; } = String.Empty;
        public string EncryptDbPassword { get; set; } = String.Empty;
    }
}
