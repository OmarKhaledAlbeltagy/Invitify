using Invitify.Models;

namespace Invitify.Repos
{
    public interface IEmailRep
    {
        bool SendTestMail(TestMailModel obj);

        bool SendInvitationMail(InvitationMailModel obj);
    }
}
