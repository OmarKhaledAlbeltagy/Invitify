using Invitify.Entities;

namespace Invitify.Models
{
    public class AddEventSpeakersModel
    {
        public int[] EventId { get; set; }

        public string[]? Title { get; set; }

        public string[] FullName { get; set; }

        public string[]? Description { get; set; }

        public IFormFile[]? file { get; set; }
    }
}
