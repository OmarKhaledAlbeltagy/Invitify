namespace Invitify.Models
{
    public class AddBulkGalleryImagesModel
    {
        public int EventId { get; set; }

        public IFormFile[] files { get; set; }
    }
}
