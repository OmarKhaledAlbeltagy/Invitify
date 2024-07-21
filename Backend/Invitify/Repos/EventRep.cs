using Invitify.Context;
using Invitify.Entities;
using Invitify.Models;
using Invitify.Privilage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop.Implementation;
using Microsoft.VisualBasic.FileIO;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;

namespace Invitify.Repos
{
    public class EventRep:IEventRep
    {
        private readonly DbContainer db;
        private readonly ITimeRep ti;
        private readonly UserManager<ExtendIdentityUser> userManager;

        public EventRep(DbContainer db, ITimeRep ti, UserManager<ExtendIdentityUser> userManager)
        {
            this.db = db;
            this.ti = ti;
            this.userManager = userManager;
        }

        public dynamic AddBulkGalleryImages(AddBulkGalleryImagesModel list)
        {
            List<EventGallery> galleryList = db.eventGallery.Where(a=>a.EventtId == list.EventId).ToList();
   
            long uploadedsize = 0;
            long oldsize = 0;
            foreach (var item in list.files)
            {
                uploadedsize = uploadedsize + item.Length;
            }
            foreach (var item in galleryList)
            {
                oldsize = oldsize + item.Data.LongLength;
            }

            if (uploadedsize + oldsize > 55000000)
            {
                return ("Error: Total size of gallery images uploaded is larger than 50 MB");
            }
            var length = list.files.Count();
            if (length + galleryList.Count() >= 10)
            {
                return ("Error: Total number of gallery images uploaded is more than 10 images");
            }

            DateTime now = ti.GetCurrentTime();

            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in list.files)
                    {

                        int indx = item.FileName.Split('.').Length - 1;
                        string extension = item.FileName.Split('.')[indx];
                        string fileType = item.ContentType;
                        using (var stream = new MemoryStream())
                        {
                            item.CopyTo(stream);
                            var bytes = stream.ToArray();
                            EventGallery eg = new EventGallery();
                            eg.Extension = extension;
                            eg.ContentType = fileType;
                            eg.Data = bytes;
                            eg.EventtId = list.EventId;
                            db.eventGallery.Add(eg);
                        }

                    }
      
                    Eventt e = db.eventt.Find(list.EventId);
                    e.LastModifiedDateTime = now;
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

        public bool AddDatesToEvent(AddEventDatesModel obj)
        {
            DateTime now = ti.GetCurrentTime();
            Eventt ev = db.eventt.Find(obj.EventId);

            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {

                try
                {
                    
                    foreach (var item in obj.dateTime)
                    {
                        EventDates ed = new EventDates();
                        ed.EventtId = obj.EventId;
                        ed.Date = item;
                        db.eventDates.Add(ed);
                    }
                    ev.LastModifiedDateTime = now;
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                   var xx = ex.Message;
                    return false;
                }
            }
        }

        public bool AddEvent(AddEventModel obj)
        {
            //int eventscount = db.eventt.Count();
            //int limit = PropertiesModel.EventsLimit;

            //if (eventscount >= limit)
            //{
            //    return false;
            //}
            //DateTime now = ti.GetCurrentTime();

            //Eventt e = new Eventt();
            //e.EventName = obj.EventName;
            //e.StateId = obj.StateId;
            //e.Address = obj.Address;
            //e.Participants = obj.Participants;
            //e.Speakers = obj.Participants;
            //e.Latitude = obj.Latitude;
            //e.Longitude = obj.Longitude;
            //e.About = obj.About;
            //e.Domain = obj.Domain;
            //e.CreationDateTime = now;
            //e.LastModifiedDateTime = now;
            //db.eventt.Add(e);
            //db.SaveChanges();

            //foreach (var item in obj.Date)
            //{
            //    EventDates ed = new EventDates();
            //    ed.EventtId = e.Id;
            //    ed.Date = item;
            //    db.eventDates.Add(ed);
            //}
            //db.SaveChanges();

            //foreach (var item in obj.eventSponsors)
            //{
            //    int indx = item.file.FileName.Split('.').Length - 1;
            //    string extension = item.file.FileName.Split('.')[indx];

            //    long fileSize = item.file.Length;
            //    string fileType = item.file.ContentType;
            //    using (var stream = new MemoryStream())
            //    {
            //        item.file.CopyTo(stream);
            //        var bytes = stream.ToArray();
            //        EventSponsors es = new EventSponsors();
            //        es.EventtId = e.Id;
            //        es.Extension = extension;
            //        es.ContentType = fileType;
            //        es.Data = bytes;
            //        es.SponsorName = item.SponsorName;
            //        db.eventSponsors.Add(es);
            //    }
            //}

            //foreach (var item in obj.eventGallery)
            //{

            //    int indx = item.FileName.Split('.').Length - 1;
            //    string extension = item.FileName.Split('.')[indx];

            //    long fileSize = item.Length;
            //    string fileType = item.ContentType;
            //    using (var stream = new MemoryStream())
            //    {
            //        item.CopyTo(stream);
            //        var bytes = stream.ToArray();
            //        EventGallery eg = new EventGallery();
            //        eg.EventtId = e.Id;
            //        eg.Extension = extension;
            //        eg.ContentType = fileType;
            //        eg.Data = bytes;

            //        db.eventGallery.Add(eg);
            //    }

            //}

            //foreach (var item in obj.eventSpeakers)
            //{
            //    int indx = item.file.FileName.Split('.').Length - 1;
            //    string extension = item.file.FileName.Split('.')[indx];

            //    long fileSize = item.file.Length;
            //    string fileType = item.file.ContentType;
            //    using (var stream = new MemoryStream())
            //    {
            //        item.file.CopyTo(stream);
            //        var bytes = stream.ToArray();
            //        EventSpeakers es = new EventSpeakers();
            //        es.EventtId = e.Id;
            //        es.Extension = extension;
            //        es.ContentType = fileType;
            //        es.Data = bytes;
            //        es.Title = item.Title;
            //        es.FullName = item.FullName;
            //        es.Description = item.Description;
            //        db.eventSpeakers.Add(es);
            //    }
            //}

            //foreach (var item in obj.eventSchedule)
            //{
            //    int ThisDateId = db.eventDates.Where(a=>a.Date.Date == item.From.Date && a.EventtId == e.Id).FirstOrDefault().Id;
            //    EventSchedule es = new EventSchedule();
            //    es.Title = item.Title;
            //    es.Description = item.Description;
            //    es.From = item.From;
            //    es.To = item.To;
            //    es.EventDatestId = ThisDateId;
            //    db.eventSchedule.Add(es);
            //}
            //db.SaveChanges();
            //return true;

            throw new NotImplementedException();
        }

        public bool AddEventt(AddEventModel obj)
        {

            //using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        DateTime now = ti.GetCurrentTime();

            //        Eventt e = new Eventt();
            //        e.EventName = obj.EventName;
            //        e.StateId = obj.StateId;
            //        e.Address = obj.Address;
            //        e.Participants = obj.Participants;
            //        e.Speakers = obj.Participants;
            //        e.Latitude = obj.Latitude;
            //        e.Longitude = obj.Longitude;
            //        e.About = obj.About;
            //        e.Domain = obj.Domain;
            //        e.CreationDateTime = now;
            //        e.LastModifiedDateTime = now;
            //        db.eventt.Add(e);
            //        db.SaveChanges();

            //        foreach (var item in obj.Date)
            //        {
            //            EventDates ed = new EventDates();
            //            ed.EventtId = e.Id;
            //            ed.Date = item;
            //            db.eventDates.Add(ed);
            //        }
            //        db.SaveChanges();

            //        foreach (var item in obj.eventSponsors)
            //        {
            //            int indx = item.file.FileName.Split('.').Length - 1;
            //            string extension = item.file.FileName.Split('.')[indx];

            //            long fileSize = item.file.Length;
            //            string fileType = item.file.ContentType;
            //            using (var stream = new MemoryStream())
            //            {
            //                item.file.CopyTo(stream);
            //                var bytes = stream.ToArray();
            //                EventSponsors es = new EventSponsors();
            //                es.EventtId = e.Id;
            //                es.Extension = extension;
            //                es.ContentType = fileType;
            //                es.Data = bytes;
            //                es.SponsorName = item.SponsorName;
            //                db.eventSponsors.Add(es);
            //            }
            //        }

            //        foreach (var item in obj.eventGallery)
            //        {

            //            int indx = item.FileName.Split('.').Length - 1;
            //            string extension = item.FileName.Split('.')[indx];

            //            long fileSize = item.Length;
            //            string fileType = item.ContentType;
            //            using (var stream = new MemoryStream())
            //            {
            //                item.CopyTo(stream);
            //                var bytes = stream.ToArray();
            //                EventGallery eg = new EventGallery();
            //                eg.EventtId = e.Id;
            //                eg.Extension = extension;
            //                eg.ContentType = fileType;
            //                eg.Data = bytes;

            //                db.eventGallery.Add(eg);
            //            }

            //        }

            //        foreach (var item in obj.eventSpeakers)
            //        {
            //            int indx = item.file.FileName.Split('.').Length - 1;
            //            string extension = item.file.FileName.Split('.')[indx];

            //            long fileSize = item.file.Length;
            //            string fileType = item.file.ContentType;
            //            using (var stream = new MemoryStream())
            //            {
            //                item.file.CopyTo(stream);
            //                var bytes = stream.ToArray();
            //                EventSpeakers es = new EventSpeakers();
            //                es.EventtId = e.Id;
            //                es.Extension = extension;
            //                es.ContentType = fileType;
            //                es.Data = bytes;
            //                es.Title = item.Title;
            //                es.FullName = item.FullName;
            //                es.Description = item.Description;
            //                db.eventSpeakers.Add(es);
            //            }
            //        }

            //        foreach (var item in obj.eventSchedule)
            //        {
            //            int ThisDateId = db.eventDates.Where(a => a.Date.Date == item.From.Date && a.EventtId == e.Id).FirstOrDefault().Id;
            //            EventSchedule es = new EventSchedule();
            //            es.Title = item.Title;
            //            es.Description = item.Description;
            //            es.From = item.From;
            //            es.To = item.To;
            //            es.EventDatestId = ThisDateId;
            //            db.eventSchedule.Add(es);
            //        }
            //        db.SaveChanges();

            //        transaction.Commit();
            //        return true;
            //    }
            //    catch (Exception)
            //    {

            //        transaction.Rollback();
            //        return false;
            //    }
            //}
            throw new NotImplementedException();
        }

        public bool AddSpeakertoEvent(EditEventSpeakerModel obj)
        {
            DateTime now = ti.GetCurrentTime();
       
            if (obj.file.Length > 5500000)
            {
                return (false);
            }
            if (obj.file != null)
            {
                int indx = obj.file.FileName.Split('.').Length - 1;
                string extension = obj.file.FileName.Split('.')[indx];
                string fileType = obj.file.ContentType;
                using (var stream = new MemoryStream())
                {
                    obj.file.CopyTo(stream);
                    var bytes = stream.ToArray();
                    EventSpeakers sp = new EventSpeakers();
                    sp.EventtId = obj.Id;
                    sp.Extension = extension;
                    sp.ContentType = fileType;
                    sp.Data = bytes;
                    sp.Title = obj.Title;
                    sp.FullName = obj.FullName;
                    if (obj.Description == "EMPTYYY")
                    {
                        sp.Description = "";
                    }
                    else
                    {
                        sp.Description = obj.Description;
                    }
                  
                    db.eventSpeakers.Add(sp);

                    Eventt ev = db.eventt.Find(obj.Id);
                    ev.LastModifiedDateTime = now;
                    db.SaveChanges();
                }
            }

            return true;
            
        }

        public bool AddSponsortoEvent(EditEventSponsorModel obj)
        {
            DateTime now = ti.GetCurrentTime();

            if (obj.file.Length > 5500000)
            {
                return (false);
            }
            if (obj.file != null)
            {
                int indx = obj.file.FileName.Split('.').Length - 1;
                string extension = obj.file.FileName.Split('.')[indx];
                string fileType = obj.file.ContentType;
                using (var stream = new MemoryStream())
                {
                    obj.file.CopyTo(stream);
                    var bytes = stream.ToArray();
                    EventSponsors sp = new EventSponsors();
                    sp.EventtId = obj.Id;
                    sp.Extension = extension;
                    sp.ContentType = fileType;
                    sp.Data = bytes;
                    sp.SponsorName = obj.SponsorName;
                    db.eventSponsors.Add(sp);

                    Eventt ev = db.eventt.Find(obj.Id);
                    ev.LastModifiedDateTime = now;
                    db.SaveChanges();
                }
            }

            return true;
        }

        public bool AssignOrganizers(AddEventOrganizersModel obj)
        {
            foreach (var item in obj.UserIds)
            {
                EventEntryOrganizer eeo = new EventEntryOrganizer();
                eeo.ExtendIdentityUserId = item;
                eeo.EventtId = obj.EventId;
                db.eventEntryOrganizer.Add(eeo);
               
            }
            db.SaveChanges();
            return true;
        }

        public dynamic CheckEventLimit()
        {
            int EventCount = db.eventt.Count();
            int limit = PropertiesModel.EventsLimit;

            if (EventCount >= limit)
            {
                return "Error: you have reached the limit of events you can create, the limit is " + limit + " events maximum";
            }
            else
            {
                return true;
            }
        }

        public bool DeleteEvent(int id)
        {
            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Eventt ev = db.eventt.Find(id);
                    ContactType ct = db.contactType.Where(a => a.ContactTypeName == ev.EventName + " Anonymous Registration" && a.IsAuto == true).FirstOrDefault();
                    db.contactType.Remove(ct);
                    db.eventt.Remove(ev);
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool DeleteEventDate(int id)
        {
            DateTime now = ti.GetCurrentTime();

            EventDates ed = db.eventDates.Find(id);
            Eventt e = db.eventt.Find(ed.EventtId);
            List<EventSchedule> es = db.eventSchedule.Where(a=>a.eventDatesId == id).ToList();

            if (ed.Date.Date <= now.Date)
            {
                return false;
            }

            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    e.LastModifiedDateTime = now;
                    db.eventSchedule.RemoveRange(es);
                    db.SaveChanges();

                    db.eventDates.Remove(ed);
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

        public bool DeleteGalleryImage(int id)
        {
            DateTime now = ti.GetCurrentTime();
            EventGallery g = db.eventGallery.Find(id);
            Eventt e = db.eventt.Find(g.EventtId);
            db.eventGallery.Remove(g);
            e.LastModifiedDateTime = now;
            db.SaveChanges();
            return true;
        }

        public bool DeleteSpeaker(int id)
        {
            DateTime now = ti.GetCurrentTime();
       

            EventSpeakers es = db.eventSpeakers.Find(id);
            int EventId = es.EventtId;
            db.eventSpeakers.Remove(es);
            Eventt ev = db.eventt.Find(EventId);
            ev.LastModifiedDateTime = now;
            db.SaveChanges();
            return true;
        }

        public bool DeleteSponsor(int id)
        {
            DateTime now = ti.GetCurrentTime();


            EventSponsors es = db.eventSponsors.Find(id);
            int EventId = es.EventtId;
            db.eventSponsors.Remove(es);
            Eventt ev = db.eventt.Find(EventId);
            ev.LastModifiedDateTime = now;
            db.SaveChanges();
            return true;
        }

        public bool EditEventDates(List<EditDatesModel> list)
        {
            DateTime now = ti.GetCurrentTime();
            Eventt e = db.eventt.Find(db.eventDates.Find(list[0].Id).EventtId);
            foreach (var item in list)
            {
                EventDates ed = db.eventDates.Find(item.Id);
                List<EventSchedule> es = db.eventSchedule.Where(a => a.eventDatesId == item.Id).ToList();

                if (item.date.Date < now.Date)
                {

                }

                else
                {
                    using (IDbContextTransaction transaction = db.Database.BeginTransaction()) 
                    {
                        try
                        {
                            e.LastModifiedDateTime = now;
                            foreach (var sch in es)
                            {
                                sch.From = new DateTime(item.date.Year, item.date.Month, item.date.Day, sch.From.Hour, sch.From.Minute, 0);
                                sch.To = new DateTime(item.date.Year, item.date.Month, item.date.Day, sch.To.Hour, sch.To.Minute, 0);
                            }
                            db.SaveChanges();

                            ed.Date = item.date;
                            db.SaveChanges();
                            transaction.Commit();
                   
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
            return true;
        }

        public bool EditEventGeneralInfo(AddEventModel obj)
        {
            DateTime now = ti.GetCurrentTime();
            Eventt e = db.eventt.Find(obj.Id);
            string CurrentDomain = e.Domain;

            if (CurrentDomain == obj.Domain)
            {
                e.EventName = obj.EventName;
                e.StateId = obj.StateId;
                e.Address = obj.Address;
                e.Participants = obj.Participants;
                e.Speakers = obj.Speakers;
                e.About = obj.About;
                e.AllowAnonymous = obj.AllowAnonymous;
                e.LastModifiedDateTime = now;
                db.SaveChanges();
                return true;
            }
            else
            {
                using (IDbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        e.EventName = obj.EventName;
                        e.StateId = obj.StateId;
                        e.Address = obj.Address;
                        e.Participants = obj.Participants;
                        e.Speakers = obj.Speakers;
                        e.About = obj.About;
                        e.Domain = obj.Domain;
                        e.AllowAnonymous = obj.AllowAnonymous;
                        e.LastModifiedDateTime = now;
                        db.SaveChanges();
                        List<Invitees> invitees = db.invitees.Where(a => a.eventtId == obj.Id).ToList();

                        foreach (var item in invitees)
                        {
                            Contact c = db.contact.Find(item.ContactId);
                            var qrGenerator = new QRCodeGenerator();
                            var qrCodeData = qrGenerator.CreateQrCode(e.Domain + "?" + e.Guidd + "&" + c.Guidd, QRCodeGenerator.ECCLevel.H);
                            var qrCodeBitmap = new QRCode(qrCodeData).GetGraphic(60);
                            Logo l = db.logo.Where(a => a.Description == "White").FirstOrDefault();
                            if (l != null)
                            {
                                var logostream = new MemoryStream(l.Data);
                                var logoImage = Image.FromStream(logostream);
                                var logoWidth = qrCodeBitmap.Width / 4;
                                var logoHeight = qrCodeBitmap.Height / 4;
                                var logoResized = new Bitmap(logoImage, logoWidth, logoHeight);

                                var logoX = (qrCodeBitmap.Width - logoWidth) / 2;
                                var logoY = (qrCodeBitmap.Height - logoHeight) / 2;

                                using var graphics = Graphics.FromImage(qrCodeBitmap);
                                graphics.DrawImage(logoResized, logoX, logoY, logoWidth, logoHeight);
                            }

                            using (var stream = new MemoryStream())
                            {

                                qrCodeBitmap.Save(stream, ImageFormat.Png);
                                var bytes = stream.ToArray();
                                item.Data = bytes;
                            }
                        }


                        db.SaveChanges();
                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                        transaction.Rollback();
                        return false;
                    }

                   
                }
                return true;
            }

           
        }

        public bool EditEventIframeInfo(GetEventiframeModel obj)
        {
            DateTime now = ti.GetCurrentTime();
            Eventt e = db.eventt.Find(obj.Id);
            e.IframeLocation = obj.IframeLocation;
            e.LastModifiedDateTime = now;
            db.SaveChanges();
            return true;
        }

        public bool EditEventScheduleInfo(List<AddEventScheduleModel> list)
        {
            DateTime now = ti.GetCurrentTime();
            Eventt ev = db.eventt.Find(list[0].EventId);
            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {

                    List<EventDates> ed = db.eventDates.Where(a=>a.EventtId == ev.Id).ToList();
                    foreach (var item in ed)
                    {
                        List<EventSchedule> esch = db.eventSchedule.Where(a => a.eventDatesId == item.Id).ToList();
                        db.eventSchedule.RemoveRange(esch);
                    }
                    db.SaveChanges();

                    foreach (var item in list)
                    {
                        EventDates date = db.eventDates.Where(a => a.EventtId == item.EventId && a.Date.Date == item.From.Date).FirstOrDefault();
                        EventSchedule tes = new EventSchedule();
                        tes.Title = item.Title;
                        tes.eventDatesId = date.Id;
                        tes.Description = item.Description;
                        tes.From = item.From;
                        tes.To = item.To;
                        db.eventSchedule.Add(tes);
                    }

                    ev.LastModifiedDateTime = now;
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    var xx = ex.Message;
                    transaction.Rollback();
                    return false;
                    
                }
            }

               
        }

        public bool EditGalleryImage(EdiGalleryImageModel obj)
        {
            DateTime now = ti.GetCurrentTime();

            long totalsize = 0;
            List<EventGallery> eg = db.eventGallery.Where(a => a.EventtId == (db.eventGallery.Find(obj.Id).EventtId)).ToList();

            foreach (var item in eg)
            {
                totalsize = totalsize + item.Data.LongLength;
            }

            if (obj.file.Length + totalsize - db.eventGallery.Find(obj.Id).Data.LongLength >= 55000000)
            {
                return (false);
            }
            if (obj.file != null)
            {
                int indx = obj.file.FileName.Split('.').Length - 1;
                string extension = obj.file.FileName.Split('.')[indx];
                string fileType = obj.file.ContentType;
                using (var stream = new MemoryStream())
                {
                    obj.file.CopyTo(stream);
                    var bytes = stream.ToArray();
                    EventGallery g = db.eventGallery.Find(obj.Id);
                    g.Extension = extension;
                    g.ContentType = fileType;
                    g.Data = bytes;

                    Eventt ev = db.eventt.Find(g.EventtId);
                    ev.LastModifiedDateTime = now;

                    db.SaveChanges();
                }
                 
            }
            return true;
        }

        public bool EditSpeakerWithImage(EditEventSpeakerModel obj)
        {
            DateTime now = ti.GetCurrentTime();

            if (obj.file.Length > 5500000)
            {
                return (false);
            }

            if (obj.file != null)
            {
                int indx = obj.file.FileName.Split('.').Length - 1;
                string extension = obj.file.FileName.Split('.')[indx];
                string fileType = obj.file.ContentType;
                using (var stream = new MemoryStream())
                {
                    obj.file.CopyTo(stream);
                    var bytes = stream.ToArray();
                    EventSpeakers sp = db.eventSpeakers.Find(obj.Id);
                    sp.Extension = extension;
                    sp.ContentType = fileType;
                    sp.Data = bytes;
                    sp.Title = obj.Title;
                    sp.FullName = obj.FullName;
                    sp.Description = obj.Description;

                    Eventt ev = db.eventt.Find(sp.EventtId);
                    ev.LastModifiedDateTime = now;

                    db.SaveChanges();
                }
            }

            return true;
        }

        public bool EditSpeakerWithoutImage(EditEventSpeakerModel obj)
        {
            DateTime now = ti.GetCurrentTime();
          

            EventSpeakers sp = db.eventSpeakers.Find(obj.Id);
            sp.Title = obj.Title;
            sp.FullName = obj.FullName;
            sp.Description = obj.Description;

            Eventt ev = db.eventt.Find(sp.EventtId);
            ev.LastModifiedDateTime = now;

            db.SaveChanges();
            return true;
        }

        public bool EditSponsorWithImage(EditEventSponsorModel obj)
        {
            DateTime now = ti.GetCurrentTime();

            if (obj.file.Length > 5500000)
            {
                return (false);
            }

            if (obj.file != null)
            {
                int indx = obj.file.FileName.Split('.').Length - 1;
                string extension = obj.file.FileName.Split('.')[indx];
                string fileType = obj.file.ContentType;
                using (var stream = new MemoryStream())
                {
                    obj.file.CopyTo(stream);
                    var bytes = stream.ToArray();
                    EventSponsors sp = db.eventSponsors.Find(obj.Id);
                    sp.Extension = extension;
                    sp.ContentType = fileType;
                    sp.Data = bytes;
                    sp.SponsorName = obj.SponsorName;

                    Eventt ev = db.eventt.Find(sp.EventtId);
                    ev.LastModifiedDateTime = now;

                    db.SaveChanges();
                }
            }

            return true;
        }

        public bool EditSponsorWithoutImage(EditEventSponsorModel obj)
        {
            DateTime now = ti.GetCurrentTime();


            EventSponsors sp = db.eventSponsors.Find(obj.Id);
            sp.SponsorName = obj.SponsorName;

            Eventt ev = db.eventt.Find(sp.EventtId);
            ev.LastModifiedDateTime = now;

            db.SaveChanges();
            return true;
        }

        public List<EventEntryOrganizerListModel> GetAllEventEntryOrganizers()
        {
            
            List<Eventt> events = db.eventt.ToList();
            List<ExtendIdentityUser> EntryOrganizers = userManager.GetUsersInRoleAsync("Entry Organizer").Result.ToList();
            List<EventEntryOrganizerListModel> res = new List<EventEntryOrganizerListModel>();


            foreach (var item in events)
            {
                List<SimpleUserModel> NotAssigned = new List<SimpleUserModel>();

                List<SimpleUserModel> Assigned = db.eventEntryOrganizer.Where(a => a.EventtId == item.Id).Join(db.Users, a => a.ExtendIdentityUserId, b => b.Id, (a, b) => new SimpleUserModel
                {
                    UserId = b.Id,
                    FullName = b.FullName
                }).ToList();

                foreach (var user in EntryOrganizers)
                {
                    if (Assigned.Any(a => a.UserId == user.Id) == false)
                    {
                        SimpleUserModel not = new SimpleUserModel();
                        not.UserId = user.Id;
                        not.FullName = user.FullName;
                        NotAssigned.Add(not);
                    }
                }
                EventEntryOrganizerListModel obj = new EventEntryOrganizerListModel();
                obj.Assigned = Assigned;
                obj.NotAssigned = NotAssigned;
                obj.EventId = item.Id;
                res.Add(obj);
            }

            return res;
        }

        public List<GetEventModel> GetAllEvents()
        {
           DateTime now = ti.GetCurrentTime();

            List<GetEventModel> res = db.eventt.Join(db.state, a => a.StateId, b => b.Id, (a, b) => new
            {
                Id = a.Id,
                EventName = a.EventName,
                StateName = b.StateName,
                CountryId = b.CountryId,
                Address = a.Address,
                Participants = a.Participants,
                Speakers = a.Speakers,
                IframeLocation = a.IframeLocation,
                About = a.About,
                Domain = a.Domain,
                AllowAnonymous = a.AllowAnonymous,
                Guidd = a.Guidd,
                CreationDateTime = a.CreationDateTime,
                LastModifiedDateTime = a.LastModifiedDateTime,
                CreationDate = a.CreationDateTime
            }).Join(db.country, a => a.CountryId, b => b.Id, (a, b) => new GetEventModel
            {
                Id = a.Id,
                EventName = a.EventName,
                StateName = a.StateName,
                CountryName = b.CountryName,
                Address = a.Address,
                Participants = a.Participants,
                Speakers = a.Speakers,
                IframeLocation = a.IframeLocation,
                About = a.About,
                Domain = a.Domain,
                AllowAnonymous = a.AllowAnonymous,
                Guidd = a.Guidd,
                CreationDateTime = a.CreationDateTime.ToString("dd MMMM yyyy - hh:mm tt"),
                LastModifiedDateTime = a.LastModifiedDateTime.ToString("dd MMMM yyyy - hh:mm tt"),
                CreationDate = a.CreationDateTime,
                AllowDelete = true
            }).ToList();

            foreach (var item in res)
            {

                List<GetEventScheduleModel> scheduleres = new List<GetEventScheduleModel>();
                List<EventSchedule> schedule = new List<EventSchedule>();
        

                List<GetEventDatesModel> resdates = new List<GetEventDatesModel>();
                List<EventDates> dates = db.eventDates.Where(a => a.EventtId == item.Id).ToList();
                List<DateTime> datess = new List<DateTime>();
                foreach (var date in dates)
                {
                    datess.Add(date.Date);
                    GetEventDatesModel d = new GetEventDatesModel();
                    d.dateTime = date.Date.ToString("dddd dd MMMM yyyy");
                    d.dateTimeOrder = date.Date;
                    d.Id = date.Id;
                    d.EventId = item.Id;
                    resdates.Add(d);

                    List<EventSchedule> eventschedule = db.eventSchedule.Where(a => a.eventDatesId == date.Id).ToList();
                    schedule.AddRange(eventschedule);
                }


                item.dates = resdates.OrderBy(a => a.dateTimeOrder).ToList();
             

                foreach (var sch in schedule)
                {
                    GetEventScheduleModel evsch = new GetEventScheduleModel();
                    evsch.Date = sch.From.ToString("dd MMMM yyyy");
                    evsch.From = sch.From.ToString("hh:mm tt");
                    evsch.To = sch.To.ToString("hh:mm tt");
                    evsch.Id = sch.Id;
                    evsch.EventDatesId = sch.eventDatesId;
                    evsch.Title = sch.Title;
                    evsch.Description = sch.Description;
                    scheduleres.Add(evsch);
                }

                item.schedule = scheduleres;
              

                List<GetEventSpeakersModel> speakerres = new List<GetEventSpeakersModel>();
                List<EventSpeakers> sp = db.eventSpeakers.Where(a => a.EventtId == item.Id).ToList();

                foreach (var speaker in sp)
                {
                    GetEventSpeakersModel evsp = new GetEventSpeakersModel();
                    evsp.Id = speaker.Id;
                    evsp.Title = speaker.Title;
                    evsp.Description = speaker.Description;
                    evsp.FullName = speaker.FullName;
                    evsp.EventtId = speaker.EventtId;
                    speakerres.Add(evsp);
                }

                item.speakerss = speakerres;
             

                List<GetEventSponsorsModel> sponsorres = new List<GetEventSponsorsModel>();
                List<EventSponsors> spon = db.eventSponsors.Where(a => a.EventtId == item.Id).ToList();

                foreach (var sponsor in spon)
                {
                    GetEventSponsorsModel evspon = new GetEventSponsorsModel();
                    evspon.Id = sponsor.Id;
                    evspon.EventtId = sponsor.EventtId;
                    evspon.SponsorName = sponsor.SponsorName;
                    sponsorres.Add(evspon);
                }

                item.sponsors = sponsorres;
            

                List<GetEventGalleryModel> galleryres = new List<GetEventGalleryModel>();
                List<EventGallery> gal = db.eventGallery.Where(a => a.EventtId == item.Id).ToList();

                foreach (var g in gal)
                {
                    GetEventGalleryModel evgal = new GetEventGalleryModel();
                    evgal.Id = g.Id;
                    evgal.EventtId = g.EventtId;
                    galleryres.Add(evgal);
                }

               item.gallery = galleryres;



              datess = datess.OrderBy(a => a.Date).ToList();

                if (datess.FirstOrDefault().Date <= now.Date)
                {
                    item.AllowDelete = false;
                }

                else
                {
                    if (db.registration.Where(a => a.EventtId == item.Id).Count() > 0)
                    {
                        item.AllowDelete = false;
                    }
                }
                


                item.StartDate = datess.FirstOrDefault().ToString("dddd dd MMMM yyyy");


    

            }

            return res.ToList();
        }

        public GetEventModel GetEventById(int id)
        {
            GetEventModel res = db.eventt.Where(a=>a.Id == id).Join(db.state, a => a.StateId, b => b.Id, (a, b) => new
            {
                Id = a.Id,
                EventName = a.EventName,
                StateName = b.StateName,
                CountryId = b.CountryId,
                Address = a.Address,
                Participants = a.Participants,
                Speakers = a.Speakers,
                IframeLocation = a.IframeLocation,
                About = a.About,
                Domain = a.Domain,
                AllowAnonymous = a.AllowAnonymous,
                Guidd = a.Guidd,
                CreationDateTime = a.CreationDateTime,
                LastModifiedDateTime = a.LastModifiedDateTime,
                CreationDate = a.CreationDateTime,
                StateId = a.StateId
            }).Join(db.country, a => a.CountryId, b => b.Id, (a, b) => new GetEventModel
            {
                Id = a.Id,
                EventName = a.EventName,
                StateName = a.StateName,
                CountryName = b.CountryName,
                Address = a.Address,
                Participants = a.Participants,
                Speakers = a.Speakers,
                IframeLocation = a.IframeLocation,
                About = a.About,
                Domain = a.Domain,
                AllowAnonymous = a.AllowAnonymous,
                Guidd = a.Guidd,
                CreationDateTime = a.CreationDateTime.ToString("dd MMMM yyyy - hh:mm tt"),
                LastModifiedDateTime = a.LastModifiedDateTime.ToString("dd MMMM yyyy - hh:mm tt"),
                CreationDate = a.CreationDateTime,
                AllowDelete = true,
                StateId = a.StateId,
                CountryId = a.CountryId
            }).FirstOrDefault();

            return res;
        }

        public List<GetEventDatesModel> GetEventDatesforEdit(int id)
        {

            DateTime now = ti.GetCurrentTime();

            List<EventDates> edates = db.eventDates.Where(a=>a.EventtId == id).OrderBy(a=>a.Date).ToList();
            List<GetEventDatesModel> res = new List<GetEventDatesModel>();
            foreach (var item in edates)
            {
                GetEventDatesModel obj = new GetEventDatesModel();
                obj.Id = item.Id;
                obj.dateTime = item.Date.ToString("yyyy-MM-dd");
                obj.dateTimeOrder = item.Date;
                obj.EventId = id;

                if (item.Date.Date <= now.Date)
                {
                    obj.AllowEdit = false;
                }
                else
                {
                    obj.AllowEdit = true;
                }

                res.Add(obj);
            }

            return res.OrderBy(a=>a.dateTimeOrder).ToList();

        }

        public GetGalleryForEditModel GetEventGallery(int id)
        {
            List<EventGallery> g = db.eventGallery.Where(a => a.EventtId == id).ToList();

            GetGalleryForEditModel res = new GetGalleryForEditModel();
            long s = 0;
            foreach (var item in g)
            {
                s = s + item.Data.LongLength;
            }
            res.GalleryCount = g.Count;
            res.galleryList = g;
            res.TotalSize = s;
            return res;
        }

        public GetEventiframeModel GetEventIFrame(int id)
        {
            Eventt e = db.eventt.Find(id);
            GetEventiframeModel res = new GetEventiframeModel();
            res.Id = e.Id;
            res.EventName = e.EventName;
            res.IframeLocation = e.IframeLocation;
            return res;
        }

        public List<DateAndScheduleListModel> GetEventScheduleForEdit(int id)
        {
            List<EventDates> ed = db.eventDates.Where(a => a.EventtId == id).ToList();
            List<DateAndScheduleListModel> res = new List<DateAndScheduleListModel>();

            foreach (var item in ed)
            {
                DateAndScheduleListModel obj = new DateAndScheduleListModel();
                obj.date = item.Date;

                List<EventSchedule> sch = db.eventSchedule.Where(a => a.eventDatesId == item.Id).ToList();
                List<GetEventScheduleModel> schlist = new List<GetEventScheduleModel>();
                foreach (var s in sch)
                {
                    GetEventScheduleModel getsch = new GetEventScheduleModel();
                    getsch.From = s.From.ToString("HH:mm:ss");
                    getsch.To = s.To.ToString("HH:mm:ss");
                    getsch.Title = s.Title;
                    getsch.Description = s.Description;
                    getsch.Date = s.From.ToString("yyyy-MM-dd");
                    getsch.EventDatesId = s.eventDatesId;
                    schlist.Add(getsch);
                }

                obj.schedule = schlist;
                res.Add(obj);
            }

            return res;

        }

        public List<EventSpeakers> GetEventSpeakers(int id)
        {
            List<EventSpeakers> sp = db.eventSpeakers.Where(a=>a.EventtId == id) .ToList();
            return sp;
        }

        public List<EventSponsors> GetEventSponsors(int id)
        {
            List<EventSponsors> sp = db.eventSponsors.Where(a => a.EventtId == id).ToList();
            return sp;
        }

        public bool UnAssignOrganizers(AddEventOrganizersModel obj)
        {
            foreach (var item in obj.UserIds)
            {
                EventEntryOrganizer eeo = db.eventEntryOrganizer.Where(a => a.EventtId == obj.EventId && a.ExtendIdentityUserId == item).FirstOrDefault();
                db.eventEntryOrganizer.Remove(eeo);
            }
            db.SaveChanges();
            return true;
        }
    }
}
