namespace Invitify.Models
{
    public class EventScheduleLandingDatesModel
    {
        public DateTime dateTime { get; set; }

        public string dateTimeString { get; set; }

        public List<EventScheduleLandingTopicsModel> Topics { get; set; }
    }
}
