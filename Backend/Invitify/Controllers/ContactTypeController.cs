using Invitify.Entities;
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
    public class ContactTypeController : ControllerBase
    {
        private readonly IContactTypeRep rep;

        public ContactTypeController(IContactTypeRep rep)
        {
            this.rep = rep;
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllContactType()
        {
            return Ok(rep.GetAllContactType());
        }

        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult DeleteContactType(int id)
        {
            return Ok(rep.DeleteContactType(id));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult EditContactType(ContactType obj)
        {
            return Ok(rep.EditContactType(obj));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddContactType(ContactType obj)
        {
            return Ok(rep.AddContactType(obj));
        }
    }
}
