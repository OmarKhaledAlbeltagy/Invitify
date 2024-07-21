namespace Invitify.Models
{
    public class EditEventSpeakerModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string FullName { get; set; }

        public string? Description { get; set; }

        public IFormFile? file { get; set; }
    }
}
