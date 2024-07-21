using Invitify.Models;

namespace Invitify.Repos
{
    public interface ITempEventRep
    {

        List<string> GetTempDates(int EventId);

        EventCookieModel AddTempEventGeneralInfo(AddEventModel obj);

        bool AddTempEventSponsorInfo(AddEventSponsorsModel obj);

        bool AddTempEventGalleryInfo(AddEventGalleryModel obj);

        bool AddTempEventDatesInfo(AddEventDatesModel obj);

        bool AddTempEventScheduleInfo(List<AddEventScheduleModel> list);

        bool AddTempEventSpeakersInfo(AddEventSpeakersModel obj);

        ContinueEventCreationModel CheckEventsCreation();

        bool DeleteTemp();

        string[] GetDomains();

        bool MigrateEvent(int TempEventId);
    }
}
