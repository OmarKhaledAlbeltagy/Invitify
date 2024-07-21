namespace Invitify.Entities
{
    public class TempEventSchedule
    {
        public int Id { get; set; }

        public int TempEventDatesId { get; set; }

        public TempEventDates tempEventDates { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }
    }
}
