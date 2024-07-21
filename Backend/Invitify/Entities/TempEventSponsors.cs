namespace Invitify.Entities
{
    public class TempEventSponsors
    {
        public int Id { get; set; }

        public int TempEventtId { get; set; }

        public TempEventt tempEventt { get; set; }

        public string SponsorName { get; set; }

        public string? ContentType { get; set; }

        public string? Extension { get; set; }

        public byte[]? Data { get; set; }
    }
}
