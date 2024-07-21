namespace Invitify.Entities
{
    public class Eventt
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

        public IEnumerable<EventDates> eventDates { get; set; }

        public IEnumerable<EventSpeakers> eventSpeakers { get; set; }

        public IEnumerable<EventSponsors> eventSponsors { get; set; }

        public IEnumerable<EventGallery> eventGallery { get; set; }

        public IEnumerable<Invitees> invitees { get; set; }

        public IEnumerable<Registration> registration { get; set; }

        public string Domain { get; set; }

        public string Guidd { get; set; } = Guid.NewGuid().ToString().Replace("-", string.Empty);

        public DateTime CreationDateTime { get; set; }

        public DateTime LastModifiedDateTime { get; set; }

        public bool AllowAnonymous { get; set; }
    }
}
