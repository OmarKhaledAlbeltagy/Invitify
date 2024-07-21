using Invitify.Entities;

namespace Invitify.Models
{
    public class GetGalleryForEditModel
    {
        public int GalleryCount { get; set; }

        public long TotalSize { get; set; }

        public List<EventGallery> galleryList { get; set; }
    }
}
