namespace Invitify.Entities
{
    public class Contact
    {
        public int Id { get; set; }

        public string ContactName { get; set; }

        public bool? Gender { get; set; }

        public int StateId { get; set; }

        public State state { get; set; }

        public int? PhoneCodeId { get; set; }

        public Country PhoneCode { get; set; }

        public string? Address { get; set; }

        public string? MobileNumber { get; set; }

        public string? Email { get; set; }

        public int ContactTypeId { get; set; }

        public ContactType contactType { get; set; }

        public string? Notes { get; set; }
        
        public DateTime CreationDateTime { get; set; }

        public DateTime LastModifiedDateTime { get; set; }

        public string Guidd { get; set; } = Guid.NewGuid().ToString().Replace("-", string.Empty);

        public IEnumerable<Invitees> invitees { get; set; }

        public IEnumerable<Registration> registration { get; set; }
    }
}
