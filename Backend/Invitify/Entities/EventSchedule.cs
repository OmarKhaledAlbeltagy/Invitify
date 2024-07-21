namespace Invitify.Entities
{
    public class EventSchedule
    {
        public int Id { get; set; }

        public int eventDatesId { get; set; }

        public EventDates eventDates { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }
    }
}
