using Invitify.Entities;
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
    public class ContactController : ControllerBase
    {
        private readonly IContactRep rep;

        public ContactController(IContactRep rep)
        {
            this.rep = rep;
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllContact()
        {
            return Ok(rep.GetAllContact());
        }

        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult DeleteContact(int id)
        {
            return Ok(rep.DeleteContact(id));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddContact(AddContactModel obj)
        {
            return Ok(rep.AddContact(obj));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult EditContact(EditContactModel obj)
        {
            return Ok(rep.EditContact(obj));
        }
    }
}
