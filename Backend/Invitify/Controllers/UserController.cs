using Invitify.Models;
using Invitify.Privilage;
using Invitify.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Invitify.Controllers
{
    [EnableCors("allow")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IUserRep rep;
        private readonly UserManager<ExtendIdentityUser> userManager;
        private readonly SignInManager<ExtendIdentityUser> signInManager;

        public UserController(IUserRep rep, UserManager<ExtendIdentityUser> userManager, SignInManager<ExtendIdentityUser> signInManager)
        {
            this.rep = rep;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel obj)
        {
            var res = await signInManager.PasswordSignInAsync(obj.Email, obj.Password, true, false);

            if (res.Succeeded)
            {
                ExtendIdentityUser user = userManager.FindByEmailAsync(obj.Email).Result;
                string role = userManager.GetRolesAsync(user).Result.FirstOrDefault();
                CustomUserRole userrole = new CustomUserRole();
                userrole.FullName = user.FullName;
                userrole.UserEmail = user.Email;
                userrole.UserId = user.Id;
                userrole.RoleName = role;
                userrole.Active = user.Active;
                return Ok(userrole);
            }
            else
            {
                return Ok(0);
            }
       

        }

    }
}
