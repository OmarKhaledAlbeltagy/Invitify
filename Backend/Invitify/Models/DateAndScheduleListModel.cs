namespace Invitify.Models
{
    public class DateAndScheduleListModel
    {
        public DateTime date { get; set; }

        public List<GetEventScheduleModel> schedule { get; set; }
    }
}
