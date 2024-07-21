namespace Invitify.Entities
{
    public class TempEventt
    {
        public int Id { get; set; }

        public string EventName { get; set; }

        public int StateId { get; set; }

        public State state { get; set; }

        public string Address { get; set; }

        public int? Participants { get; set; }

        public int? Speakers { get; set; }

        public string IframeLocation { get; set; }

        public string? About { get; set; }

        public IEnumerable<TempEventDates> eventDates { get; set; }

        public IEnumerable<TempEventSchedule> eventSchedule { get; set; }

        public IEnumerable<TempEventSpeakers> eventSpeakers { get; set; }

        public IEnumerable<TempEventSponsors> eventSponsors { get; set; }

        public IEnumerable<TempEventGallery> eventGallery { get; set; }

        public string Domain { get; set; }

        public int LastStep { get; set; }

        public DateTime CreationDateTime { get; set; }

        public bool AllowAnonymous { get; set; }
    }
}
