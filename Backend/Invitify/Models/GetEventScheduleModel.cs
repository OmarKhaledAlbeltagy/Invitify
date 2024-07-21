using Invitify.Entities;

namespace Invitify.Models
{
    public class GetEventScheduleModel
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public int EventDatesId { get; set; }
    }
}
