namespace Invitify.Models
{
    public class RegistrationListModel
    {
        public int Id { get; set; }

        public int ContactId { get; set; }

        public string ContactName { get; set; }

        public string Email { get; set; }

        public string RegisteredEmail { get; set; }

        public string MobileNumber { get; set; }

        public string RegisteredMobileNumber { get; set; }
    }
}
