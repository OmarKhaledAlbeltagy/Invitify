using Invitify.Context;
using Invitify.Entities;
using Invitify.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Invitify.Repos
{
    public class TempEventRep:ITempEventRep
    {
        private readonly DbContainer db;
        private readonly ITimeRep ti;

        public TempEventRep(DbContainer db, ITimeRep ti)
        {
            this.db = db;
            this.ti = ti;
        }

        public bool AddTempEventDatesInfo(AddEventDatesModel obj)
        {
            foreach (var item in obj.dateTime)
            {
                TempEventDates e = new TempEventDates();
                e.TempEventtId = obj.EventId;
                e.Date = item;
                db.tempEventDates.Add(e);
            }

            TempEventt te = db.tempEventt.Find(obj.EventId);
            te.LastStep = 2;
            db.SaveChanges();
            return true;
        }

        public bool AddTempEventGalleryInfo(AddEventGalleryModel obj)
        {
            if (obj.EventId[0] == 0 || obj.EventId[0] == null)
            {
                return false;
            }

            TempEventt e = db.tempEventt.Find(obj.EventId[0]);

            var length = obj.EventId.Length;


            for (int i = 0; i < length; i++)
            {
                int indx = obj.file[i].FileName.Split('.').Length - 1;
                string extension = obj.file[i].FileName.Split('.')[indx];
                string fileType = obj.file[i].ContentType;
                using (var stream = new MemoryStream())
                {
                    obj.file[i].CopyTo(stream);
                    var bytes = stream.ToArray();
                    TempEventGallery es = new TempEventGallery();
                    es.TempEventtId = e.Id;
                    es.Extension = extension;
                    es.ContentType = fileType;
                    es.Data = bytes;
                    db.tempEventGallery.Add(es);
                    
                }
            }
            e.LastStep = 6;
            db.SaveChanges();
            return true;

        }

        public EventCookieModel AddTempEventGeneralInfo(AddEventModel obj)
        {
            DateTime now = ti.GetCurrentTime();
            TempEventt e = new TempEventt();
            e.EventName = obj.EventName;
            e.StateId = obj.StateId;
            e.Address = obj.Address;
            e.Participants = obj.Participants;
            e.Speakers = obj.Speakers;
            e.IframeLocation= obj.IframeLocation;
            e.About = obj.About;
            e.Domain = obj.Domain;
            e.AllowAnonymous = obj.AllowAnonymous;
            e.CreationDateTime = now;
            e.LastStep = 1;
            db.tempEventt.Add(e);
            db.SaveChanges();


            EventCookieModel res = new EventCookieModel();
            res.Id = e.Id;
            res.EventName = e.EventName;
            return res;
        }

        public bool AddTempEventScheduleInfo(List<AddEventScheduleModel> list)
        {

            foreach (var item in list)
            {
                TempEventDates date = db.tempEventDates.Where(a => a.TempEventtId == item.EventId && a.Date.Date == item.From.Date).FirstOrDefault();
                TempEventSchedule tes = new TempEventSchedule();
                tes.Title = item.Title;
                tes.TempEventDatesId = date.Id;
                tes.Description = item.Description;
                tes.From = item.From;
                tes.To = item.To;
                db.tempEventSchedule.Add(tes);
            }

            TempEventt ev = db.tempEventt.Find(list[0].EventId);
            ev.LastStep = 3;

            db.SaveChanges();

            return true;
        }

        public bool AddTempEventSpeakersInfo(AddEventSpeakersModel obj)
        {
            if (obj.EventId[0] == 0 || obj.EventId[0] == null)
            {
                return false;
            }

            TempEventt e = db.tempEventt.Find(obj.EventId[0]);

            var length = obj.EventId.Length;


            for (int i = 0; i < length; i++)
            {

                if (obj.file[i] != null)
                {
                    int indx = obj.file[i].FileName.Split('.').Length - 1;
                    string extension = obj.file[i].FileName.Split('.')[indx];
                    string fileType = obj.file[i].ContentType;
                    using (var stream = new MemoryStream())
                    {
                        obj.file[i].CopyTo(stream);
                        var bytes = stream.ToArray();
                        TempEventSpeakers es = new TempEventSpeakers();
                        es.TempEventtId = e.Id;
                        es.Extension = extension;
                        es.ContentType = fileType;
                        es.Data = bytes;
                        es.Title = obj.Title[i];
                        es.FullName = obj.FullName[i];
                        es.Description = obj.Description[i];
                        db.tempEventSpeaker.Add(es);
                    }
                }

                else
                {
                    TempEventSpeakers es = new TempEventSpeakers();
                    es.TempEventtId = e.Id;
                    es.Title = obj.Title[i];
                    es.FullName = obj.FullName[i];
                    es.Description = obj.Description[i];
                    db.tempEventSpeaker.Add(es);
                }


            }

            e.LastStep = 4;
            db.SaveChanges();
            return true;


        }

        public bool AddTempEventSponsorInfo(AddEventSponsorsModel obj)
        {

            if (obj.EventId[0] == 0 || obj.EventId[0] == null)
            {
                return false;
            }

            TempEventt e = db.tempEventt.Find(obj.EventId[0]);

            var length = obj.EventId.Length;

            for (int i = 0; i < length; i++)
            {
                if (obj.file[i] != null)
                {
                    int indx = obj.file[i].FileName.Split('.').Length - 1;
                    string extension = obj.file[i].FileName.Split('.')[indx];

                    long fileSize = obj.file[i].Length;
                    string fileType = obj.file[i].ContentType;
                    using (var stream = new MemoryStream())
                    {
                        obj.file[i].CopyTo(stream);
                        var bytes = stream.ToArray();
                        TempEventSponsors es = new TempEventSponsors();
                        es.TempEventtId = e.Id;
                        es.Extension = extension;
                        es.ContentType = fileType;
                        es.Data = bytes;
                        es.SponsorName = obj.SponsorName[i];
                        db.tempEventSponsor.Add(es);
                    }
                }
                else
                {
                    TempEventSponsors es = new TempEventSponsors();
                    es.TempEventtId = e.Id;
                    es.SponsorName = obj.SponsorName[i];
                    db.tempEventSponsor.Add(es);
                }
            }
            e.LastStep = 5;
            db.SaveChanges();
            return true;
        }

        public ContinueEventCreationModel CheckEventsCreation()
        {
            ContinueEventCreationModel res = new ContinueEventCreationModel();
            TempEventt tempevent = db.tempEventt.FirstOrDefault();

            if (tempevent == null)
            {
                return res;
            }


            else
            {

                res.Id = tempevent.Id;
                res.EventName = tempevent.EventName;
                res.LastStep = tempevent.LastStep;
                res.CreationDateTime = tempevent.CreationDateTime.ToString("dd MMMM yyyy - hh:mm tt");

                return res;
            }


        }

        public bool DeleteTemp()
        {
            List<TempEventSchedule> esch = db.tempEventSchedule.ToList();
            List<TempEventDates> ed = db.tempEventDates.ToList();
            List<TempEventSpeakers> espe = db.tempEventSpeaker.ToList();
            List<TempEventSponsors> sespoch = db.tempEventSponsor.ToList();
            List<TempEventGallery> eg = db.tempEventGallery.ToList();
            List<TempEventt> e = db.tempEventt.ToList();

            foreach (var item in esch)
            {
                db.tempEventSchedule.Remove(item);
            }
            foreach (var item in ed)
            {
                db.tempEventDates.Remove(item);
            }
            foreach (var item in espe)
            {
                db.tempEventSpeaker.Remove(item);
            }
            foreach (var item in sespoch)
            {
                db.tempEventSponsor.Remove(item);
            }
            foreach (var item in eg)
            {
                db.tempEventGallery.Remove(item);
            }
            foreach (var item in e)
            {
                db.tempEventt.Remove(item);
            }
            db.SaveChanges();

            return true;

        }

        public string[] GetDomains()
        {
            return PropertiesModel.RegisteredDomains;
        }

        public List<string> GetTempDates(int EventId)
        {
            List<string> res = db.tempEventDates.Where(a=>a.TempEventtId == EventId).Select(a=>a.Date.Date.ToString("yyyy-MM-dd")).ToList();
            return res;
        }

        public bool MigrateEvent(int TempEventId)
        {
            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    DateTime now = ti.GetCurrentTime();
                    TempEventt tempEvent = db.tempEventt.Find(TempEventId);
                    Eventt eventt = new Eventt();
                    eventt.EventName = tempEvent.EventName;
                    eventt.StateId = tempEvent.StateId;
                    eventt.Address = tempEvent.Address;
                    eventt.Participants = tempEvent.Participants;
                    eventt.Speakers = tempEvent.Speakers;
                    eventt.IframeLocation= tempEvent.IframeLocation;
                    eventt.About = tempEvent.About;
                    eventt.Domain = tempEvent.Domain;
                    eventt.AllowAnonymous = tempEvent.AllowAnonymous;
                    eventt.CreationDateTime = now;
                    eventt.LastModifiedDateTime = now;
                    db.eventt.Add(eventt);
                    db.SaveChanges();


                    //eventDates
                    List<TempEventDates> tempEventDates = db.tempEventDates.Where(a => a.TempEventtId == tempEvent.Id).ToList();
                    List<EventDates> ed = new List<EventDates>();
                    foreach (var item in tempEventDates)
                    {
                        EventDates eventDates = new EventDates();
                        eventDates.EventtId = eventt.Id;
                        eventDates.Date = item.Date;
                        db.eventDates.Add(eventDates);
                        ed.Add(eventDates);
                    }
                    db.SaveChanges();


                    //EventSpeakers
                    List<TempEventSpeakers> tempEventSpeakers = db.tempEventSpeaker.Where(a => a.TempEventtId == tempEvent.Id).ToList();
                    foreach (var item in tempEventSpeakers)
                    {
                        EventSpeakers eventSpeakers = new EventSpeakers();
                        eventSpeakers.EventtId = eventt.Id;
                        eventSpeakers.FullName = item.FullName;
                        eventSpeakers.Title = item.Title;
                        eventSpeakers.Description = item.Description;
                        eventSpeakers.ContentType = item.ContentType;
                        eventSpeakers.Extension = item.Extension;
                        eventSpeakers.Data = item.Data;
                        db.eventSpeakers.Add(eventSpeakers);
                    }


                    //EventSponsors
                    List<TempEventSponsors> tempEventSponsors = db.tempEventSponsor.Where(a => a.TempEventtId == tempEvent.Id).ToList();
                    foreach (var item in tempEventSponsors)
                    {
                        EventSponsors eventSponsors = new EventSponsors();
                        eventSponsors.EventtId = eventt.Id;
                        eventSponsors.SponsorName = item.SponsorName;
                        eventSponsors.ContentType = item.ContentType;
                        eventSponsors.Extension = item.Extension;
                        eventSponsors.Data = item.Data;
                        db.eventSponsors.Add(eventSponsors);
                    }

                    //EventGallery
                    List<TempEventGallery> tempEventGalery = db.tempEventGallery.Where(a => a.TempEventtId == tempEvent.Id).ToList();
                    foreach (var item in tempEventGalery)
                    {
                        EventGallery eventGallery = new EventGallery();
                        eventGallery.EventtId = eventt.Id;
                        eventGallery.ContentType = item.ContentType;
                        eventGallery.Extension = item.Extension;
                        eventGallery.Data = item.Data;
                        db.eventGallery.Add(eventGallery);
                    }



                    //EventSchedule

                    List<TempEventSchedule> tempEventSchedule = new List<TempEventSchedule>();

                    foreach (var item in tempEventDates)
                    {
                        List<TempEventSchedule> tes = db.tempEventSchedule.Where(a => a.TempEventDatesId == item.Id).ToList();
                        tempEventSchedule.AddRange(tes);
                        foreach (var date in tes)
                        {
                            EventSchedule eventSchedule = new EventSchedule();
                            eventSchedule.Title = date.Title;
                            eventSchedule.Description = date.Description;
                            eventSchedule.From = date.From;
                            eventSchedule.To = date.To;
                            eventSchedule.eventDatesId = ed.Where(a => a.Date.Date == date.From.Date && a.EventtId == eventt.Id).FirstOrDefault().Id;
                            db.eventSchedule.Add(eventSchedule);
                        }
                    }




                    //DeletingTemp
                    foreach (var item in tempEventSchedule)
                    {
                        db.tempEventSchedule.Remove(item);
                    }
                    db.SaveChanges();
                    foreach (var item in tempEventGalery)
                    {
                        db.tempEventGallery.Remove(item);
                    }
                    foreach (var item in tempEventSponsors)
                    {
                        db.tempEventSponsor.Remove(item);
                    }
                    foreach (var item in tempEventSpeakers)
                    {
                        db.tempEventSpeaker.Remove(item);
                    }
                    foreach (var item in tempEventDates)
                    {
                        db.tempEventDates.Remove(item);
                    }
                    db.SaveChanges();

                    db.tempEventt.Remove(tempEvent);
                    db.SaveChanges();


                    ContactType ct = new ContactType();
                    ct.IsAuto = true;
                    ct.ContactTypeName = eventt.EventName + " Anonymous Registration";
                    db.contactType.Add(ct);
                    db.SaveChanges();


                    transaction.Commit();
                    return true;
                }


                catch (Exception ex)
                {
                    string m = ex.Message;
                    transaction.Rollback();
                    return false;
                }
            }

            

        }
    }
}
