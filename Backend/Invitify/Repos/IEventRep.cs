using Invitify.Entities;
using Invitify.Models;

namespace Invitify.Repos
{
    public interface IEventRep
    {
        dynamic AddBulkGalleryImages(AddBulkGalleryImagesModel list);

        bool EditGalleryImage(EdiGalleryImageModel obj);

        GetGalleryForEditModel GetEventGallery(int id);

        bool AddSpeakertoEvent(EditEventSpeakerModel obj);

        bool AddSponsortoEvent(EditEventSponsorModel obj);

        bool DeleteSpeaker(int id);

        bool DeleteSponsor(int id);

        bool DeleteGalleryImage(int id);

        bool EditEventScheduleInfo(List<AddEventScheduleModel> list);

        List<DateAndScheduleListModel> GetEventScheduleForEdit(int id);

        bool AddDatesToEvent(AddEventDatesModel obj);

        bool EditEventDates(List<EditDatesModel> list);

        bool DeleteEventDate(int id);

        List<GetEventDatesModel> GetEventDatesforEdit(int id);

        GetEventiframeModel GetEventIFrame(int id);

        bool EditEventGeneralInfo(AddEventModel obj);

        bool EditEventIframeInfo(GetEventiframeModel obj);

        GetEventModel GetEventById(int id);

        bool DeleteEvent(int id);

        bool AddEvent(AddEventModel obj);

        bool AddEventt(AddEventModel obj);

        dynamic CheckEventLimit();

        List<GetEventModel> GetAllEvents();

        List<EventSpeakers> GetEventSpeakers(int id);

        bool EditSpeakerWithoutImage(EditEventSpeakerModel obj);

        bool EditSpeakerWithImage(EditEventSpeakerModel obj);

        bool EditSponsorWithImage(EditEventSponsorModel obj);

        bool EditSponsorWithoutImage(EditEventSponsorModel obj);

        List<EventSponsors> GetEventSponsors(int id);

        List<EventEntryOrganizerListModel> GetAllEventEntryOrganizers();

        bool AssignOrganizers(AddEventOrganizersModel obj);

        bool UnAssignOrganizers(AddEventOrganizersModel obj);
    }
}
