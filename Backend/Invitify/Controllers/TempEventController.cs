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
    public class TempEventController : ControllerBase
    {
        private readonly ITempEventRep rep;
        private readonly DbContainer db;

        public TempEventController(ITempEventRep rep,DbContainer db)
        {
            this.rep = rep;
            this.db = db;
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetDomains()
        {
            return Ok(rep.GetDomains());
        }


        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetTempDates(int id)
        {
            return Ok(rep.GetTempDates(id));
        }


        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult MigrateEvent(int id)
        {
            return Ok(rep.MigrateEvent(id));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddTempEventGalleryInfo([FromForm] AddEventGalleryModel list)
        {
            return Ok(rep.AddTempEventGalleryInfo(list));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddTempEventSponsorInfo([FromForm] AddEventSponsorsModel list)
        {
            var listlength = list.EventId.Length;

            for (int i = 0; i < listlength; i++)
            {
                if (list.file[i] != null)
                {
                    var filesize = list.file[i].Length;

                    if (filesize > 5500000)
                    {
                        return Ok("Error: You have uploaded a sponsor image that has size more than 5 MB\"");
                    }
                }
            }

            return Ok(rep.AddTempEventSponsorInfo(list));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddTempEventSpeakersInfo([FromForm] AddEventSpeakersModel list)
        {


            var listlength = list.EventId.Length;

            for (int i = 0; i < listlength; i++)
            {

              

                if (list.file[i] != null)
                {
                    var filesize = list.file[i].Length;

                    if (filesize > 5500000)
                    {
                        return Ok("Error: You have uploaded a speaker image that has size more than 5 MB");
                    }
                }
            }



            return Ok(rep.AddTempEventSpeakersInfo(list));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddTempEventScheduleInfo(List<AddEventScheduleModel> list)
        {
            return Ok(rep.AddTempEventScheduleInfo(list));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddTempEventDatesInfo(AddEventDatesModel obj)
        {
            return Ok(rep.AddTempEventDatesInfo(obj));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddTempEventGeneralInfo(AddEventModel obj)
        {
            int CurrentEvents = db.eventt.Count();
            int limit = PropertiesModel.EventsLimit;
            if (CurrentEvents >= limit)
            {
                return Ok(false);
            }

            return Ok(rep.AddTempEventGeneralInfo(obj));
        }


        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult CheckEventsCreation()
        {
            ContinueEventCreationModel x = rep.CheckEventsCreation();

            if (x.EventName == null)
            {
                return Ok(false);
            }
            else
            {
                return Ok(x);
            }

        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult DeleteTemp()
        {
            return Ok(rep.DeleteTemp());
        }
    }
}
