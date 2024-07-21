using Invitify.Privilage;

namespace Invitify.Entities
{
    public class Attendance
    {
        public int Id { get; set; }

        public int ContactId { get; set; }

        public Contact contact { get; set; }

        public int EventtId { get; set; }

        public Eventt eventt { get; set; }

        public DateTime AttendanceDateTime { get; set; }

        public string ExtendIdentityUserId { get; set; }

        public ExtendIdentityUser extendidentityuser { get; set; }
    }
}
