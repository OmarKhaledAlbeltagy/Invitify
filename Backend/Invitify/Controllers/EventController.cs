using Invitify.Context;
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
    public class EventController : ControllerBase
    {
        private readonly IEventRep rep;

        public EventController(IEventRep rep)
        {
            this.rep = rep;
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult UnAssignOrganizers(AddEventOrganizersModel obj)
        {
            return Ok(rep.UnAssignOrganizers(obj));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AssignOrganizers(AddEventOrganizersModel obj)
        {
            return Ok(rep.AssignOrganizers(obj));
        }


        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllEventEntryOrganizers()
        {
            return Ok(rep.GetAllEventEntryOrganizers());
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddBulkGalleryImages([FromForm] AddBulkGalleryImagesModel obj)
        {
            return Ok(rep.AddBulkGalleryImages(obj));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult EditGalleryImage([FromForm] EdiGalleryImageModel obj)
        {
            return Ok(rep.EditGalleryImage(obj));
        }

        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult DeleteGalleryImage(int id)
        {
            return Ok(rep.DeleteGalleryImage(id));
        }


        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetEventGallery(int id)
        {
            return Ok(rep.GetEventGallery(id));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddSponsortoEvent([FromForm] EditEventSponsorModel obj)
        {
            return Ok(rep.AddSponsortoEvent(obj));
        }


        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult DeleteSponsor(int id)
        {
            return Ok(rep.DeleteSponsor(id));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult EditSponsorWithoutImage(EditEventSponsorModel obj)
        {
            return Ok(rep.EditSponsorWithoutImage(obj));
        }



        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult EditSponsorWithImage([FromForm] EditEventSponsorModel obj)
        {
            return Ok(rep.EditSponsorWithImage(obj));
        }


        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetEventSponsors(int id)
        {
            return Ok(rep.GetEventSponsors(id));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddSpeakertoEvent([FromForm] EditEventSpeakerModel obj)
        {
            return Ok(rep.AddSpeakertoEvent(obj));
        }


        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult DeleteSpeaker(int id)
        {
            return Ok(rep.DeleteSpeaker(id));
        }



        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult EditSpeakerWithImage([FromForm]EditEventSpeakerModel obj)
        {
            return Ok(rep.EditSpeakerWithImage(obj));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult EditSpeakerWithoutImage(EditEventSpeakerModel obj)
        {
            return Ok(rep.EditSpeakerWithoutImage(obj));
        }


        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetEventSpeakers(int id)
        {
            return Ok(rep.GetEventSpeakers(id));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult EditEventScheduleInfo(List<AddEventScheduleModel> list)
        {
            return Ok(rep.EditEventScheduleInfo(list));
        }

        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetEventScheduleForEdit(int id)
        {
            return Ok(rep.GetEventScheduleForEdit(id));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddDatesToEvent(AddEventDatesModel obj)
        {
            return Ok(rep.AddDatesToEvent(obj));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult EditEventDates(List<EditDatesModel> list)
        {
            return Ok(rep.EditEventDates(list));
        }



        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult DeleteEventDate(int id)
        {
            return Ok(rep.DeleteEventDate(id));
        }



        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetEventDatesforEdit(int id)
        {
            return Ok(rep.GetEventDatesforEdit(id));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult EditEventIframeInfo(GetEventiframeModel obj)
        {
            return Ok(rep.EditEventIframeInfo(obj));
        }

        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetEventIFrame(int id)
        {
            return Ok(rep.GetEventIFrame(id));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult EditEventGeneralInfo(AddEventModel obj)
        {
            return Ok(rep.EditEventGeneralInfo(obj));
        }

        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetEventById(int id)
        {
            return Ok(rep.GetEventById(id));
        }


        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult DeleteEvent(int id)
        {
            return Ok(rep.DeleteEvent(id));
        }


        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllEvents()
        {
            List<GetEventModel> x = rep.GetAllEvents();
            return Ok(x);
        }


        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult CheckEventLimit()
        {
         return Ok(rep.CheckEventLimit());
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddEvent([FromForm]AddEventModel obj)
        {
            //var x = rep.CheckEventLimit();

            // if (x == true)
            // {

            // }
            // else
            // {
            //     return BadRequest(x);
            // }

            // List<AddEventSpeakersModel> speakermodel = obj.eventSpeakers.Where(a => a.file.Length != 0).ToList();
            // int speakerlength = speakermodel.Count();
            // for (int i = 0; i < speakerlength; i++)
            // {
            //     if (speakermodel[i].file.Length > 5500000)
            //     {
            //         return BadRequest("Error: You have uploaded a speaker image that has size more than 5 MB");
            //     }
            // }

            // List<AddEventSponsorsModel> sponsormodel = obj.eventSponsors.Where(a => a.file.Length != 0).ToList();
            // int sponsorlength = sponsormodel.Count();
            // for (int i = 0; i < sponsorlength; i++)
            // {
            //     if (sponsormodel[i].file.Length > 5500000)
            //     {
            //         return BadRequest("Error: You have uploaded a sponsor image that has size more than 5 MB");
            //     }
            // }



            // int gallerylength = obj.eventGallery.Count();
            // if (gallerylength > 10)
            // {
            //     return BadRequest("Error: You can upload up to 10 images maximum for gallery images");
            // }

            // decimal gallerysize = 0;
            // for (int i = 0; i < gallerylength; i++)
            // {
            //     gallerysize = gallerysize + obj.eventGallery[i].Length;
            // }

            // if (gallerysize > 53000000)
            // {
            //     return BadRequest("Error: Gallery images files total size is more than 50 MB");
            // }

            //return Ok(rep.AddEventt(obj));

            return Ok();
        }

    }
}
