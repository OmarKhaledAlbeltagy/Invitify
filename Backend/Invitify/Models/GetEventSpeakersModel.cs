using Invitify.Entities;

namespace Invitify.Models
{
    public class GetEventSpeakersModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string FullName { get; set; }

        public string? Description { get; set; }

        public int EventtId { get; set; }
    }
}
