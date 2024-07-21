namespace Invitify.Entities
{
    public class EventSpeakers
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string FullName { get; set; }

        public string? Description { get; set; }

        public string? ContentType { get; set; }

        public int EventtId { get; set; }

        public Eventt eventt { get; set; }

        public string? Extension { get; set; }

        public byte[]? Data { get; set; }
    }
}
