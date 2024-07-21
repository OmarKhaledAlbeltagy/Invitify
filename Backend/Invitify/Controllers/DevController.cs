using Invitify.Context;
using Invitify.CountryModels;
using Invitify.Entities;
using Invitify.Models;
using Invitify.Privilage;
using Invitify.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.VisualBasic.FileIO;
using MoreLinq;
using System.ComponentModel.DataAnnotations;

namespace Invitify.Controllers
{
    [EnableCors("allow")]
    [ApiController]
    [AllowAnonymous]
    public class DevController : ControllerBase
    {
        private readonly DbContainer db;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ExtendIdentityUser> userManager;
        private readonly ITimeRep ti;

        public DevController(DbContainer db,RoleManager<IdentityRole> roleManager,UserManager<ExtendIdentityUser> userManager,ITimeRep ti)
        {
            this.db = db;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.ti = ti;
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult x()
        {
            var x = PropertiesModel.RegisteredDomains.Where(a => a == "").GetEnumerator();  

            return Ok(x);

        }



        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult iframe()
        {
      
            string res = db.eventt.FirstOrDefault().IframeLocation;
            return Ok(res);

        }



        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetCurrentTime()
        {

            return Ok(ti.GetCurrentTime());

        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm]delmodel obj)
        {
            int indx = obj.filee.FileName.Split('.').Length - 1;
            string extension = obj.filee.FileName.Split('.')[indx];

            long fileSize = obj.filee.Length;
            string fileType = obj.filee.ContentType;
            if (fileSize > 0)
            {
                using (var stream = new MemoryStream())
                {
                    obj.filee.CopyTo(stream);
                    var bytes = stream.ToArray();

                    image i = new image();
                    i.Name = obj.Namee;
                    i.Data = bytes;
                    i.ContentType = fileType;
                    i.Extension = extension;
                    db.image.Add(i);

                }
            }

            db.SaveChanges();

            return Ok(true);
        }


        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetImage(int id)
        {
            var x = db.image.Find(id);

            return File(x.Data, x.ContentType);

        }


      


        [Route("[controller]/[Action]")]
        [HttpGet]
        public async Task<IActionResult> ConfigurePhoneCodes()
        {
            List<Country> c = db.country.ToList();
            foreach (var item in c)
            {
                if (item.PhoneCode[0] == '+')
                {
                    
                }
                else
                {
                    item.PhoneCode = '+' + item.PhoneCode;
                }
            }
            db.SaveChanges();
            return Ok(true);
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public async Task<IActionResult> ConfigureCountry()
        {
            List<Country> c = db.country.ToList();
            List<string> st = new List<string>();
            foreach (var item in c)
            {
                State s = db.state.Where(a => a.CountryId == item.Id).FirstOrDefault();

                if (s == null)
                {
                    State state = new State();
                    state.CountryId = item.Id;
                    state.StateName = item.CountryName;
                    db.state.Add(state);
                    //st.Add(item.CountryName);
                }
                db.SaveChanges();
            }

            return Ok(st);
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(AdminRegistrationModel obj)
        {
            ExtendIdentityUser check = userManager.FindByEmailAsync(obj.Email).Result;

            if (check != null)
            {
                return Ok("Email is already used");
            }

            else
            {
                ExtendIdentityUser user = new ExtendIdentityUser();
                user.Email = obj.Email;
                user.UserName = obj.Email;
                user.PhoneNumber = obj.PhoneNumber;
                user.FullName = obj.FullName;

                try
                {
                    var create = userManager.CreateAsync(user, obj.Password).Result;
                    if (create.Succeeded)
                    {
                       var addtorole = userManager.AddToRoleAsync(user, obj.Role).Result;

                        if (addtorole.Succeeded)
                        {
                            return Ok(true);
                        }
                    }
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
          

             
            }

            return BadRequest(false);
        }

        [Route("[controller]/[Action]/{r}")]
        [HttpGet]
        public async Task<IActionResult> AddRole(string r)
        {
            IdentityRole role = new IdentityRole();
            role.Name = r;
            var x = await roleManager.CreateAsync(role);
            if (x.Succeeded)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
          
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public async Task<IActionResult> country(List<CountryM> x)
        {
     
            foreach (var item in x)
            {
                Country c = new Country();
                c.CountryName = item.name;
                c.Iso = item.iso3;
                c.PhoneCode = item.phone_code;
                c.Latitude = item.latitude;
                c.Longitude = item.longitude;
                c.Emoji = item.emoji;
                db.country.Add(c);
                await db.SaveChangesAsync();

                foreach (var st in item.states)
                {
                    State s = new State();
                    s.StateName = st.name;
                    s.Latitude = st.latitude;
                    s.Longitude = st.longitude;
                    s.CountryId = c.Id;
                    db.state.Add(s);
                }
                await db.SaveChangesAsync();
            }

            return Ok(true);

        }
    }
}
