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
    public class CountryController : ControllerBase
    {
        private readonly ICountryRep rep;

        public CountryController(ICountryRep rep)
        {
            this.rep = rep;
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllPhoneCode()
        {
            return Ok(rep.GetAllPhoneCode());
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllCountries()
        {
            return Ok(rep.GetAllCountries());
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllStates()
        {
            return Ok(rep.GetAllStates());
        }
    }
}
