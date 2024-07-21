namespace Invitify.Models
{
    public class EventRegisterationModel
    {
        public int EventId { get; set; }

        public int ContactId { get; set; }

        public int PhoneCodeId { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
