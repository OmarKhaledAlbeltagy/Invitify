using Invitify.Entities;

namespace Invitify.Models
{
    public class AddEventSponsorsModel
    {
        public int[] EventId { get; set; }

        public string[] SponsorName { get; set; }

        public IFormFile[]? file { get; set; }
    }
}
