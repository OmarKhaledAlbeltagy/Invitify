namespace Invitify.Models
{
    public class EditEventSponsorModel
    {
        public int Id { get; set; }

        public string SponsorName { get; set; }

        public IFormFile? file { get; set; }
    }
}
