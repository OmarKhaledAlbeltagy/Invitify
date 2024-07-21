namespace Invitify.Models
{
    public class SimpleContactModel
    {
        public int Id { get; set; }

        public int ContactId { get; set; }

        public int? InviteesId { get; set; }

        public string ContactName { get; set; }

        public string? PhoneCode { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? StateName { get; set; }

        public string? CountryName { get; set; }

        public string? MobileNumber { get; set; }

        public string? Email { get; set; }

        public string? ContactTypeName { get; set; }

        public bool? IsEmail { get; set; }
    }
}
