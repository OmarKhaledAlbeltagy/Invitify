namespace Invitify.Entities
{
    public class ContactType
    {
        public int Id { get; set; }

        public string ContactTypeName { get; set; }

        public bool IsAuto { get; set; } = false;

        public IEnumerable<Contact> contact { get; set; }
    }
}
