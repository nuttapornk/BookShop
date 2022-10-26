namespace BookShop.WebUi.Models
{
    public class AppSetting
    {
        public string Lang { get; set; }
        public string DisplayTimeFormat { get; set; }
        public string DisplayDateFormat { get; set; }
        public string DisplayDateTimeFormat { get; set; }
        public int PageSize { get; set; }
        public string SecretKey { get; set; }
        public string EncryptDbPassword { get; set; }

    }
}
