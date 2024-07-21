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
    public class EntryOrganizerController : ControllerBase
    {
        private readonly IEntryOrganizerRep rep;

        public EntryOrganizerController(IEntryOrganizerRep rep)
        {
            this.rep = rep;
        }

        [Route("[controller]/[Action]/{Code}/{UserId}")]
        [HttpGet]
        public IActionResult SaveAttendance(string Code, string UserId)
        {
            return Ok(rep.SaveAttendance(Code,UserId));
        }

        [Route("[controller]/[Action]/{code}")]
        [HttpGet]
        public IActionResult CheckCode(string code)
        {
            return Ok(rep.CheckCode(code));
        }

        [Route("[controller]/[Action]/{InvId}")]
        [HttpGet]
        public IActionResult SendInvitationEmail(int InvId)
        {
            return Ok(rep.SendInvitationEmail(InvId));
        }

        [Route("[controller]/[Action]/{RegId}")]
        [HttpGet]
        public IActionResult SendRegistrationEmailToContactEmail(int RegId)
        {
            return Ok(rep.SendRegistrationEmailToContactEmail(RegId));
        }


        [Route("[controller]/[Action]/{RegId}")]
        [HttpGet]
        public IActionResult SendRegistrationEmailToRegisteredEmail(int RegId)
        {
            return Ok(rep.SendRegistrationEmailToRegisteredEmail(RegId));
        }


        [Route("[controller]/[Action]/{EventId}")]
        [HttpGet]
        public IActionResult GetEventRegsitrations(int EventId)
        {
            return Ok(rep.GetEventRegsitrations(EventId));
        }


        [Route("[controller]/[Action]/{UserId}")]
        [HttpGet]
        public IActionResult GetTodayEvent(string UserId)
        {
            return Ok(rep.GetTodayEvent(UserId));
        }
    }
}
