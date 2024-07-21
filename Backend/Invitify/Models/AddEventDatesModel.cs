using Invitify.Entities;

namespace Invitify.Models
{
    public class AddEventDatesModel
    {
        public int EventId { get; set; }

        public List<DateTime> dateTime { get; set; }
    }
}
