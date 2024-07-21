namespace Invitify.Models
{
    public class AddToInviteesModel
    {
        public int EventId { get; set; }

        public List<int> ContactsId { get; set; }
    }
}
