namespace Invitify.Models
{
    public class InvitationMailModel
    {
        public int[] InviteesId { get; set; }

        public string EmailSubject { get; set; }

        public string EmailBody { get; set; }

        public string ButtonText { get; set; }
    }
}
