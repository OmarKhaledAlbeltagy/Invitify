namespace Invitify.Entities
{
    public class Invitees
    {
        public int Id { get; set; }

        public int eventtId { get; set; }

        public Eventt eventt { get; set; }

        public int ContactId { get; set; }

        public Contact contact { get; set; }

        public byte[] Data { get; set; }

        public bool IsEmail { get; set; } = false;

        public bool IsSms { get; set; } = false;

        public DateTime CreationDateTime { get; set; }

        public bool? Accepted { get; set; } = null;
    }
}
