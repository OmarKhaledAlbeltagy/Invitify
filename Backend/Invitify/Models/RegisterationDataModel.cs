namespace Invitify.Models
{
    public class RegisterationDataModel
    {
        public int Id { get; set; }

        public int ContactId { get; set; }

        public string ContactName { get; set; }

        public string Email { get; set; }

        public string PhoneCode { get; set; }

        public int PhoneCodeId { get; set; }

        public string MobileNumber { get; set; }
    }
}
