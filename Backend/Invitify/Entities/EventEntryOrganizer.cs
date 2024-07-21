using Invitify.Privilage;

namespace Invitify.Entities
{
    public class EventEntryOrganizer
    {
        public int Id { get; set; }

        public int EventtId { get; set; }

        public Eventt eventt { get; set; }

        public string ExtendIdentityUserId { get; set; }

        public ExtendIdentityUser extendidentityuser { get; set; }
    }
}
