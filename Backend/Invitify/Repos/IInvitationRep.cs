using Invitify.Models;

namespace Invitify.Repos
{
    public interface IInvitationRep
    {
        bool AddToInvitees(AddToInviteesModel obj);

        List<InviteesContactModel> GetInviteesData();

        bool RemoveFromInvitees(AddToInviteesModel obj);

        List<SimpleContactModel> GetEventInvitees(int id);
    }
}
