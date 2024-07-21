namespace Invitify.Models
{
    public class AdminRegistrationModel
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set;  } = "Entry Organizer";

        public string FullName { get; set; }

        public string Password { get; set; }
    }
}
