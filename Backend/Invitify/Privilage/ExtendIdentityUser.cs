using Invitify.Entities;
using Microsoft.AspNetCore.Identity;

namespace Invitify.Privilage
{
    public class ExtendIdentityUser : IdentityUser
    {
        public string FullName { get; set; }

        public bool Active { get; set; } = true;
    }
}
