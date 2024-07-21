namespace Invitify.Entities
{
    public class EventSponsors
    {
        public int Id { get; set; }

        public int EventtId { get; set; }

        public Eventt eventt { get; set; }

        public string SponsorName { get; set; }

        public string? ContentType { get; set; }

        public string? Extension { get; set; }

        public byte[]? Data { get; set; }
    }
}
