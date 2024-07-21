namespace Invitify.Models
{
    public class GetEventDatesModel
    {
        public int Id { get; set; }

        public string dateTime { get; set; }

        public DateTime dateTimeOrder { get; set; }

        public int EventId { get; set; }

        public bool AllowEdit { get; set; } = false;
    }
}
