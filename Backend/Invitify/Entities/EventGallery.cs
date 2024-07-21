namespace Invitify.Entities
{
    public class EventGallery
    {
        public int Id { get; set; }

        public int EventtId { get; set; }

        public Eventt eventt { get; set; }

        public string ContentType { get; set; }

        public string Extension { get; set; }

        public byte[] Data { get; set; }
    }
}
