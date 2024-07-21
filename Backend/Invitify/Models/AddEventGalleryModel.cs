using Invitify.Entities;

namespace Invitify.Models
{
    public class AddEventGalleryModel
    {
        public int[] EventId { get; set; }

        public IFormFile[] file { get; set; }
    }
}
