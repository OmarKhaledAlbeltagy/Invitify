namespace Invitify.Models
{
    public class PropertiesModel
    {
      
        public static int EventsLimit { get; set; } = 5;

        public static int ContactsLimit { get; set; } = 100;

        public static string DashboardDomain { get; set; } = "https://dashboard.example.tech";

        public static string BackEndDomain { get; set; } = "https://api.com";

        public static string[] RegisteredDomains { get; set; } = { "https://example.com" };

        public static string TimeZone { get; set; } = "Egypt Standard Time";

        public static string EmailAddress { get; set; } = "example@abc.com";

        public static string EmailPassword { get; set; } = "Password";

        public static int EmailPort { get; set; } = 587;

        public static string SmtpServer { get; set; } = "SMTP Server";
    }
}
