using Invitify.Entities;

namespace Invitify.Models
{
    public class ContactModel
    {
        public int Id { get; set; }

        public int InviteesId { get; set; }

        public string ContactName { get; set; }

        public bool? Gender { get; set; }

        public int StateId { get; set; }

        public string StateName { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public string? Address { get; set; }

        public string? MobileNumber { get; set; }

        public string? Email { get; set; }

        public int ContactTypeId { get; set; }

        public string ContactTypeName { get; set; }

        public string? Notes { get; set; }

        public string CreationDateTime { get; set; }

        public string LastModifiedDateTime { get; set; }

        public bool CanDeleted { get; set; }

        public int? PhoneCodeId { get; set; }

        public string? PhoneCode { get; set; }
    }
}
