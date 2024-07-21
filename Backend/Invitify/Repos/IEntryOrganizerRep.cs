using Invitify.Models;

namespace Invitify.Repos
{
    public interface IEntryOrganizerRep
    {
        int GetTodayEvent(string UserId);

        EventRegistrationListModel GetEventRegsitrations(int EventId);

        bool SendRegistrationEmailToRegisteredEmail(int RegId);

        bool SendRegistrationEmailToContactEmail(int RegId);

        bool SendInvitationEmail(int InvId);

        dynamic CheckCode(string code);

        bool SaveAttendance(string code, string UserId);
    }
}
