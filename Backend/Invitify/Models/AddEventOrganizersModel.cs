namespace Invitify.Models
{
    public class AddEventOrganizersModel
    {
        public int EventId { get; set; }

        public List<string> UserIds { get; set; }
    }
}
