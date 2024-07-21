namespace Invitify.Entities
{
    public class TempEventDates
    {
        public int Id { get; set; }

        public int TempEventtId { get; set; }

        public TempEventt tempEventt { get; set; }

        public DateTime Date { get; set; }
    }
}
