namespace Invitify.Models
{
    public class EventRegistrationListModel
    {
        public string EventName { get; set; }

        public List<RegistrationListModel> registrations { get; set; }

        public List<RegistrationListModel> invitees { get; set; }
    }
}
