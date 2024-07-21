namespace Invitify.Models
{
    public class InviteesContactModel
    {
        public int EventId { get; set; }

        public List<SimpleContactModel> Invited { get; set; }

        public List<SimpleContactModel> NotInvited { get; set; }

        public int NumberOfInvitees { get; set; }

        public int NumberOfRegistrations { get; set; }
    }
}
