namespace Invitify.Entities
{
    public class Registration
    {
        public int Id { get; set; }

        public int ContactId { get; set; }

        public Contact contact { get; set; }

        public int EventtId { get; set; }

        public Eventt eventt { get; set; }

        public string Email { get; set; }

        public int PhoneCodeId { get; set; }

        public Country PhoneCode { get; set; }

        public string PhoneNumber { get; set; }

        public string RegistrationCode { get; set; }

        public string Guidd { get; set; } = Guid.NewGuid().ToString().Replace("-", string.Empty);

        public DateTime CreationDateTime { get; set; }

        public byte[] Data { get; set; }
    }
}
