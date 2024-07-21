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
    public class LandingController : ControllerBase
    {
        private readonly ILandingRep rep;

        public LandingController(ILandingRep rep)
        {
            this.rep = rep;
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AnonymousRegister(AnonymousRegistrationModel obj)
        {
            return Ok(rep.AnonymousRegister(obj));
        }


        [Route("[controller]/[Action]/{EventToken}/{ContactToken}")]
        [HttpGet]
        public IActionResult NotInterested(string EventToken, string ContactToken)
        {
            return Ok(rep.NotInterested(EventToken, ContactToken));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult EditRegistration(EditEventRegisterationModel obj)
        {
            return Ok(rep.EditRegistration(obj));
        }



        [Route("[controller]/[Action]/{EventToken}/{ContactToken}")]
        [HttpGet]
        public IActionResult GetRegistrationData(string EventToken, string ContactToken)
        {
            return Ok(rep.GetRegistrationData(EventToken, ContactToken));
        }


        [Route("[controller]/[Action]/{EventToken}/{ContactToken}")]
        [HttpGet]
        public IActionResult CheckRegisteration(string EventToken, string ContactToken)
        {
            return Ok(rep.CheckRegisteration(EventToken, ContactToken));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult NotInterested(NotInterestedModel obj)
        {
            return Ok(rep.NotInterested(obj));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult Register(EventRegisterationModel obj)
        {
            return Ok(rep.Register(obj));
        }


        [Route("[controller]/[Action]/{token}")]
        [HttpGet]
        public IActionResult GetContactByToken(string token)
        {
            return Ok(rep.GetContactByToken(token));
        }


        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetSocialMedia()
        {
            return Ok(rep.GetSocialMedia());
        }


        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetGalleryImages(int id)
        {
            return Ok(rep.GetGalleryImages(id));
        }


        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetEventSchedule(int id)
        {
            return Ok(rep.GetEventSchedule(id));
        }

        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetEventDates(int id)
        {
            return Ok(rep.GetEventDates(id));
        }

        [Route("[controller]/[Action]/{token}")]
        [HttpGet]
        public IActionResult GetEventByToken(string token)
        {
            return Ok(rep.GetEventByToken(token));
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetContactInfo()
        {
            return Ok(rep.GetContactInfo());
        }

        [Route("[controller]/[Action]/{token}")]
        [HttpGet]
        public IActionResult GetAllEventData(string token)
        {
            return Ok(rep.GetAllEventData(token));
        }
    }
}
