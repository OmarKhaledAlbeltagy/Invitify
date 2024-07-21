using Invitify.Models;
using Invitify.Privilage;
using Microsoft.AspNetCore.Identity;

namespace Invitify.Repos
{
    public class UserRep : IUserRep
    {
        private readonly SignInManager<ExtendIdentityUser> signInManager;
        private readonly UserManager<ExtendIdentityUser> userManager;

        public UserRep(SignInManager<ExtendIdentityUser> signInManager, UserManager<ExtendIdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<dynamic> Login(LoginModel obj)
         {


            return true;

        }
    }
}
