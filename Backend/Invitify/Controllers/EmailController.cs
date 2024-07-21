using Invitify.Models;
using Invitify.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invitify.Controllers
{
    [EnableCors("allow")]
    [ApiController]
    [AllowAnonymous]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRep rep;

        public EmailController(IEmailRep rep)
        {
            this.rep = rep;
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult SendInvitationMail(InvitationMailModel obj)
        {
            return Ok(rep.SendInvitationMail(obj));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult SendTestMail(TestMailModel obj)
        {
            return Ok(rep.SendTestMail(obj));
        }
    }
}
