using Invitify.Entities;
using Invitify.Models;

namespace Invitify.Repos
{
    public interface ILandingRep
    {
        ContactInfoModel GetContactInfo();

        GetEventModel GetAllEventData(string token);

        GetEventModel GetEventByToken(string token);

        List<string> GetEventDates(int id);

        List<EventScheduleLandingDatesModel> GetEventSchedule(int id);

        List<int> GetGalleryImages(int id);

        List<Properties> GetSocialMedia();

        Contact GetContactByToken(string token);

        int Register(EventRegisterationModel obj);

        bool NotInterested(NotInterestedModel obj);

        bool CheckRegisteration(string EventToken, string ContactToken);

        RegisterationDataModel GetRegistrationData(string EventToken, string ContactToken);

        bool EditRegistration(EditEventRegisterationModel obj);

        bool NotInterested(string EventToken, string ContactToken);

        dynamic AnonymousRegister(AnonymousRegistrationModel obj);
    }
}
