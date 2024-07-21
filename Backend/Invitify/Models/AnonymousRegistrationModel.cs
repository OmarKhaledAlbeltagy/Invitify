namespace Invitify.Models
{
    public class AnonymousRegistrationModel
    {
        public string FullName { get; set; }

        public bool Gender { get; set; }

        public int StateId { get; set; }

        public int EventId { get; set; }

        public int PhoneCodeId { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }
    }
}
