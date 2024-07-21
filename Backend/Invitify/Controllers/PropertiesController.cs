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
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertiesRep rep;

        public PropertiesController(IPropertiesRep rep)
        {
            this.rep = rep;
        }


        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult CheckLogo()
        {
            return Ok(rep.CheckLogo());
        }



        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult MigrationQrCodes()
        {
           return Ok(rep.MigrationQrCodes());
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddLogo([FromForm]AddLogoModel obj)
        {
            return Ok(rep.AddLogo(obj));
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllProperties()
        {
            return Ok(rep.GetAllProperties());
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult EditProperty(EditPropertyModel obj)
        {
            return Ok(rep.EditProperty(obj));
        }
    }
}
