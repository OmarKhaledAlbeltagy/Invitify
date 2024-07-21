namespace Invitify.Models
{
    public class GetEventModel
    {
        public int Id { get; set; }

        public string EventName { get; set; }

        public string StateName { get; set; }

        public int? StateId { get; set; }

        public string CountryName { get; set; }

        public int? CountryId { get; set; }

        public string Address { get; set; }

        public int? Participants { get; set; }

        public int? Speakers { get; set; }

        public string IframeLocation { get; set; }

        public string? About { get; set; }

        public string Domain { get; set; }

        public bool AllowAnonymous { get; set; }

        public string Guidd { get; set; }

        public string CreationDateTime { get; set; }

        public DateTime CreationDate { get; set; }

        public string LastModifiedDateTime { get; set; }

        public List<GetEventDatesModel>? dates { get; set; }

        public List<EventScheduleLandingDatesModel>? landingschedule { get; set; }

        public List<GetEventScheduleModel>? schedule { get; set; }

        public List<GetEventSpeakersModel>? speakerss { get; set; }

        public List<GetEventSponsorsModel>? sponsors { get; set; }

        public List<GetEventGalleryModel>? gallery { get; set; }

        public bool AllowDelete { get; set; }

        public string? StartDate { get; set; }
    }
}
