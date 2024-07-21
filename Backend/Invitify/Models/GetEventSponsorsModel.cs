using Invitify.Entities;

namespace Invitify.Models
{
    public class GetEventSponsorsModel
    {
        public int Id { get; set; }

        public int EventtId { get; set; }

        public string SponsorName { get; set; }
    }
}
