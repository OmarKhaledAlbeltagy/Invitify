using System.Security.Principal;

namespace Invitify.Models
{
    public class AnonymousRegistrationReturnModel
    {
        public int ContactId { get; set; }

        public int RegistrationId { get; set; }

        public string ContactToken { get; set; }
    }
}
