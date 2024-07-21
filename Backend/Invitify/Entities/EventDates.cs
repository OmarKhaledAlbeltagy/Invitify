namespace Invitify.Entities
{
    public class EventDates
    {
        public int Id { get; set; }

        public int EventtId { get; set; }

        public Eventt eventt { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<EventSchedule> eventDates { get; set; }
    }
}
