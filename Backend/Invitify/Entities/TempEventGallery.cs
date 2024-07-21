namespace Invitify.Entities
{
    public class TempEventGallery
    {
        public int Id { get; set; }

        public int TempEventtId { get; set; }

        public TempEventt tempEventt { get; set; }

        public string ContentType { get; set; }

        public string Extension { get; set; }

        public byte[] Data { get; set; }
    }
}
