namespace Invitify.Models
{
    public class EventEntryOrganizerListModel
    {
        public int EventId { get; set; }

        public List<SimpleUserModel> Assigned { get; set; }

        public List<SimpleUserModel> NotAssigned { get; set; }
    }
}
