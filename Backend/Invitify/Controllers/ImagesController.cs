using Invitify.Context;
using Invitify.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Net.Http.Headers;

namespace Invitify.Controllers
{
    [EnableCors("allow")]
    [ApiController]
    [AllowAnonymous]
    public class ImagesController : ControllerBase
    {
        private readonly DbContainer db;

        public ImagesController(DbContainer db)
        {
            this.db = db;
        }

        [Route("[controller]/[Action]/{token}")]
        [HttpGet]
        public IActionResult GetRegistrationQRCode(string token)
        {
            Registration r = db.registration.Where(a => a.Guidd == token).FirstOrDefault();
            return File(r.Data, "image/png", "Registration QR Code" + ".png");
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetWhiteLogo()
        {
            Logo l = db.logo.Where(a => a.Description == "White").FirstOrDefault();
            return File(l.Data, "image/png", "WhiteLogo" + ".png");
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetTransparentLogo()
        {
            Logo l = db.logo.Where(a => a.Description == "Trans").FirstOrDefault();
            return File(l.Data, "image/png", "WhiteLogo" + ".png");
        }

        [Route("[controller]/[Action]/{x}")]
        [HttpGet]
        public IActionResult MailImages(int x)
        {
            byte[] data = { };
            string contenttype = "";
            string ext;

            switch (x)
            {
                case 1:
                    data = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Templates/images/bg_1.jpg");
                    contenttype = "image/jpeg";
                    ext = "jpg";
                    break;

                case 2:
                    data = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Templates/images/bg_1.jpg");
                    contenttype = "image/jpeg";
                    ext = "jpg";
                    break;

                case 3:
                    data = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Templates/images/bg_1.jpg");
                    contenttype = "image/jpeg";
                    ext = "jpg";
                    break;

                case 4:
                    data = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Templates/images/bg_1.jpg");
                    contenttype = "image/jpeg";
                    ext = "jpg";
                    break;
                case 5:
                    data = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Templates/images/bg_1.jpg");
                    contenttype = "image/jpeg";
                    ext = "jpg";
                    break;

                case 6:
                    data = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Templates/images/bg_1.jpg");
                    contenttype = "image/jpeg";
                    ext = "jpg";
                    break;

                case 7:
                    data = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Templates/images/bg_1.jpg");
                    contenttype = "image/jpeg";
                    ext = "jpg";
                    break;

                case 8:
                    data = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Templates/images/bg_1.jpg");
                    contenttype = "image/jpeg";
                    ext = "jpg";
                    break;

                case 9:
                    data = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Templates/images/bg_1.jpg");
                    contenttype = "image/jpeg";
                    ext = "jpg";
                    break;

                case 10:
                    data = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Templates/images/bg_1.jpg");
                    contenttype = "image/jpeg";
                    ext = "jpg";
                    break;
            }

            return File(data, contenttype, "image.png");
        }


        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetInvitationQrCode(int id)
        {
            Invitees inv = db.invitees.Find(id);
            Eventt ev = db.eventt.Find(inv.eventtId);
            Contact c = db.contact.Find(inv.ContactId);

            return File(inv.Data, "image/png", c.ContactName + " - "+ev.EventName + ".png");
        }


        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetSpeakerImage(int id)
        {
            EventSpeakers sp = db.eventSpeakers.Find(id);

            return File(sp.Data, sp.ContentType,sp.FullName+"."+sp.Extension);
        }

        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetSponsorImage(int id)
        {
            EventSponsors sp = db.eventSponsors.Find(id);
            return File(sp.Data, sp.ContentType, sp.SponsorName + "." + sp.Extension);
        }

        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetGalleryImage(int id)
        {
            EventGallery gal = db.eventGallery.Find(id);

            return File(gal.Data, gal.ContentType, "GalleryImage." + gal.Extension);
        }

        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult ShowGalleryImage(int id)
        {
            EventGallery gal = db.eventGallery.Find(id);
           
            return File(gal.Data, gal.ContentType);
        }
    }
}
