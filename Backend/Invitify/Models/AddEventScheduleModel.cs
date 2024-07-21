using Invitify.Entities;

namespace Invitify.Models
{
    public class AddEventScheduleModel
    {
        public int EventId { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }
    }
}
