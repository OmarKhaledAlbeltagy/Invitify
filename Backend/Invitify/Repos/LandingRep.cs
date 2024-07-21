using Invitify.Context;
using Invitify.Entities;
using Invitify.Models;
using Microsoft.AspNetCore.Authorization;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Invitify.Repos
{
    public class LandingRep:ILandingRep
    {
        private readonly DbContainer db;
        private readonly ITimeRep ti;

        const int codelength = 6;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public LandingRep(DbContainer db, ITimeRep ti)
        {
            this.db = db;
            this.ti = ti;
        }

        public dynamic AnonymousRegister(AnonymousRegistrationModel obj)
        {
            DateTime now = ti.GetCurrentTime();

            Eventt ev = db.eventt.Find(obj.EventId);


            Registration check = db.registration.Where(a=>a.Email== obj.Email || a.PhoneNumber == obj.MobileNumber).FirstOrDefault();

            if (check == null)
            {

                using (IDbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Contact c = new Contact();
                        c.ContactName = obj.FullName;
                        c.CreationDateTime = now;
                        c.LastModifiedDateTime = now;
                        c.ContactTypeId = db.contactType.Where(a => a.ContactTypeName == ev.EventName + " Anonymous Registration").FirstOrDefault().Id;
                        c.StateId = obj.StateId;
                        c.PhoneCodeId = obj.PhoneCodeId;
                        c.MobileNumber = obj.MobileNumber;
                        c.Email = obj.Email;
                        c.Gender = obj.Gender;
                        db.contact.Add(c);
                        db.SaveChanges();


                        var qrGenerator = new QRCodeGenerator();
                        var qrCodeData = qrGenerator.CreateQrCode(ev.Domain + "?" + ev.Guidd + "&" + c.Guidd, QRCodeGenerator.ECCLevel.H);

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
                            Invitees inv = new Invitees();
                            inv.eventtId = obj.EventId;
                            inv.ContactId = c.Id;
                            inv.CreationDateTime = now;
                            inv.Accepted = true;
                            inv.Data = bytes;
                            db.invitees.Add(inv);
                            db.SaveChanges();
                        }

                     

                        var random = new Random();
                        var randomCode = new string(Enumerable.Repeat(chars, codelength)
                                                                .Select(s => s[random.Next(s.Length)]).ToArray());


                        string g = Guid.NewGuid().ToString().Replace("-", string.Empty);


                        var codeGenerator = new QRCodeGenerator();
                        var codeCodeData = codeGenerator.CreateQrCode(PropertiesModel.DashboardDomain + "/EntryOrganizer/AttendByToken.html" + "?" + g, QRCodeGenerator.ECCLevel.H);
                        var codeCodeBitmap = new QRCode(codeCodeData).GetGraphic(60);
                        if (l != null)
                        {
                            var logostream = new MemoryStream(l.Data);
                            var logoImage = Image.FromStream(logostream);
                            var logoWidth = codeCodeBitmap.Width / 4;
                            var logoHeight = codeCodeBitmap.Height / 4;
                            var logoResized = new Bitmap(logoImage, logoWidth, logoHeight);

                            var logoX = (codeCodeBitmap.Width - logoWidth) / 2;
                            var logoY = (codeCodeBitmap.Height - logoHeight) / 2;

                            using var graphics = Graphics.FromImage(codeCodeBitmap);
                            graphics.DrawImage(logoResized, logoX, logoY, logoWidth, logoHeight);
                        }

                        Registration r = new Registration();
                        using (var stream = new MemoryStream())
                        {

                            codeCodeBitmap.Save(stream, ImageFormat.Png);
                            var bytes = stream.ToArray();
                            r.ContactId = c.Id;
                            r.EventtId = obj.EventId;
                            r.PhoneCodeId = obj.PhoneCodeId;
                            r.PhoneNumber = obj.MobileNumber;
                            r.Email = obj.Email;
                            r.RegistrationCode = randomCode;
                            r.Guidd = g;
                            r.Data = bytes;
                            r.CreationDateTime = now;
                            db.registration.Add(r);
                            db.SaveChanges();
                        }


               

                       

                        string body = File.ReadAllText(Directory.GetCurrentDirectory() + "/Templates/RegistrationEmail.html");
                        string bodyy = body.Replace("[[ContactName]]", c.ContactName).Replace("[[EventName]]", ev.EventName).Replace("[[QRCodeDomain1]]", PropertiesModel.BackEndDomain + "/Images/GetRegistrationQRCode/" + g).Replace("[[QRCodeDomain2]]", PropertiesModel.BackEndDomain + "/Images/GetRegistrationQRCode/" + g).Replace("[[RegistrationCode]]", randomCode);
                        MailMessage m = new MailMessage();
                        m.To.Add(obj.Email);
                        m.Subject = ev.EventName + " Registration Code";
                        m.From = new MailAddress(PropertiesModel.EmailAddress);
                        m.Sender = new MailAddress(PropertiesModel.EmailAddress);
                        m.IsBodyHtml = true;
                        m.Body = bodyy;
                        m.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient(PropertiesModel.SmtpServer, PropertiesModel.EmailPort);
                        smtp.EnableSsl = false;
                        smtp.Credentials = new NetworkCredential(PropertiesModel.EmailAddress, PropertiesModel.EmailPassword);
                        smtp.Send(m);


                        AnonymousRegistrationReturnModel res = new AnonymousRegistrationReturnModel();
                        res.ContactId = c.Id;
                        res.RegistrationId = r.Id;
                        res.ContactToken = c.Guidd;

                        transaction.Commit();
                        return res;
                    }
          
                catch (Exception ex)
                {
                    string m = ex.Message;
                    transaction.Rollback();
                    return ex.Message + "....." + ex.InnerException;
                }
                    }

                }

            else
            {
                return 0;
            }
            
        }

        public bool CheckRegisteration(string EventToken, string ContactToken)
        {
            Contact c = db.contact.Where(a=>a.Guidd == ContactToken).FirstOrDefault();
            Eventt ev = db.eventt.Where(a=>a.Guidd== EventToken).FirstOrDefault();

            Registration check = db.registration.Where(a=>a.ContactId == c.Id && a.EventtId == ev.Id).FirstOrDefault();

            if (check == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool EditRegistration(EditEventRegisterationModel obj)
        {
            Registration r = db.registration.Find(obj.RegistrationId);

            r.Email = obj.Email;
            r.PhoneNumber = obj.PhoneNumber;
            r.PhoneCodeId = obj.PhoneCodeId;
            db.SaveChanges();

            Contact c = db.contact.Find(r.ContactId);
            Eventt ev = db.eventt.Find(r.EventtId);


            string body = File.ReadAllText(Directory.GetCurrentDirectory() + "/Templates/RegistrationEmail.html");
            string bodyy = body.Replace("[[ContactName]]", c.ContactName).Replace("[[EventName]]", ev.EventName).Replace("[[QRCodeDomain1]]", PropertiesModel.BackEndDomain + "/Images/GetRegistrationQRCode/" + r.Guidd).Replace("[[QRCodeDomain2]]", PropertiesModel.BackEndDomain + "/Images/GetRegistrationQRCode/" + r.Guidd).Replace("[[RegistrationCode]]", r.RegistrationCode.ToString());
            MailMessage m = new MailMessage();
            m.To.Add(obj.Email);
            m.Subject = ev.EventName + " Registration Code";
            m.From = new MailAddress(PropertiesModel.EmailAddress);
            m.Sender = new MailAddress(PropertiesModel.EmailAddress);
            m.IsBodyHtml = true;
            m.Body = bodyy;
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(PropertiesModel.SmtpServer, PropertiesModel.EmailPort);
            smtp.EnableSsl = false;
            smtp.Credentials = new NetworkCredential(PropertiesModel.EmailAddress, PropertiesModel.EmailPassword);
            smtp.Send(m);
            return true;


        }

        public GetEventModel GetAllEventData(string token)
        {
            Eventt e = db.eventt.Where(a=>a.Guidd == token).FirstOrDefault();

            GetEventModel res = db.eventt.Where(a => a.Guidd == token).
                Join(db.state, a => a.StateId, b => b.Id, (a, b) => new
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
                    AllowAnonymous = a.AllowAnonymous
                })
                .Join(db.country, a => a.CountryId, b => b.Id, (a, b) => new GetEventModel
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
                    AllowAnonymous = a.AllowAnonymous
                }).FirstOrDefault();

            List<EventSchedule> EventSchedule = new List<EventSchedule>();
            List<DateTime> Dates = new List<DateTime>();
            //Dates
            List<EventDates> EventDates = db.eventDates.Where(a => a.EventtId == res.Id).ToList();
            List<GetEventDatesModel> EventDatesModel = new List<GetEventDatesModel>();
            foreach (var item in EventDates)
            {
                GetEventDatesModel EventDatesObj = new GetEventDatesModel();
                EventDatesObj.Id = item.Id;
                EventDatesObj.dateTime = item.Date.ToString("MMMM dd, yyyy");
                EventDatesObj.dateTimeOrder = item.Date;
                EventDatesObj.EventId = item.EventtId;
                EventDatesModel.Add(EventDatesObj);

                List<EventSchedule> es = db.eventSchedule.Where(a=>a.eventDatesId == item.Id).ToList();
                EventSchedule.AddRange(es);
                Dates.Add(item.Date);
            }
            res.dates = EventDatesModel;
            //Schedule
            List<EventScheduleLandingDatesModel> EventScheduleModel = new List<EventScheduleLandingDatesModel>();
            
            foreach (var item in Dates)
            {
                EventScheduleLandingDatesModel EventScheduleObj = new EventScheduleLandingDatesModel();
                EventScheduleObj.dateTime = item.Date;
                EventScheduleObj.dateTimeString = item.ToString("dd MMMM");
                List<EventScheduleLandingTopicsModel> esltm = new List<EventScheduleLandingTopicsModel>();
                foreach (var d in EventSchedule.Where(a=>a.From.Date == item.Date))
                {
                    EventScheduleLandingTopicsModel esltmobj = new EventScheduleLandingTopicsModel();
                    esltmobj.Title = d.Title;
                    esltmobj.Description = d.Description;
                    esltmobj.From = d.From;
                    esltmobj.To = d.To;
                    esltmobj.FromString = d.From.ToString("hh:mm tt");
                    esltmobj.ToString = d.To.ToString("hh:mm tt");
                    esltm.Add(esltmobj);
                }
                EventScheduleObj.Topics = esltm.OrderBy(a=>a.From).ToList();
                EventScheduleModel.Add(EventScheduleObj);
            }
            res.landingschedule = EventScheduleModel.OrderBy(a => a.dateTime).ToList();

            //Speakers
            List<GetEventSpeakersModel> SpeakersModel = new List<GetEventSpeakersModel>();
            List<EventSpeakers> eventSpeakers = db.eventSpeakers.Where(a => a.EventtId == res.Id).ToList();
            foreach (var item in eventSpeakers)
            {
                GetEventSpeakersModel SpeakersObj = new GetEventSpeakersModel();
                SpeakersObj.Id = item.Id;
                SpeakersObj.Title = item.Title;
                SpeakersObj.Description = item.Description;
                SpeakersObj.FullName = item.FullName;
                SpeakersObj.EventtId = res.Id;
                SpeakersModel.Add(SpeakersObj);
            }
            res.speakerss = SpeakersModel;

            //Sponsors
            List<GetEventSponsorsModel> SponsorsModel = new List<GetEventSponsorsModel>();
            List<EventSponsors> sponsors = db.eventSponsors.Where(a=>a.EventtId== res.Id).ToList();
            foreach (var item in sponsors)
            {
                GetEventSponsorsModel SponsorsObj = new GetEventSponsorsModel();
                SponsorsObj.Id = item.Id;
                SponsorsObj.SponsorName = item.SponsorName;
                SponsorsObj.EventtId = res.Id;
                SponsorsModel.Add(SponsorsObj);
            }
            res.sponsors = SponsorsModel;

            //Gallery
            List<GetEventGalleryModel> GalleryModel = new List<GetEventGalleryModel>();
            List<EventGallery> eventgallery = db.eventGallery.Where(a => a.EventtId == res.Id).ToList();

            foreach (var item in eventgallery)
            {
                GetEventGalleryModel GalleryObj = new GetEventGalleryModel();
                GalleryObj.Id = item.Id;
                GalleryObj.EventtId = res.Id;
                GalleryModel.Add(GalleryObj);
            }
            res.gallery = GalleryModel;

            return res;
        }

        public Contact GetContactByToken(string token)
        {
            return db.contact.Where(a => a.Guidd == token).FirstOrDefault();
        }

        public ContactInfoModel GetContactInfo()
        {
            ContactInfoModel res = new ContactInfoModel();
            res.Email = db.properties.Where(a => a.Property == "Email").FirstOrDefault().Value;
            res.PhoneNumber = db.properties.Where(a => a.Property == "Phone Number").FirstOrDefault().Value;
            return res;
        }

        public GetEventModel GetEventByToken(string token)
        {

            GetEventModel res = db.eventt.Where(a => a.Guidd == token).Join(db.state, a => a.StateId, b => b.Id, (a, b) => new
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

        public List<string> GetEventDates(int id)
        {
            List<string> res = new List<string>();

            List<EventDates> edates = db.eventDates.Where(a => a.EventtId == id).OrderBy(a => a.Date).ToList();
            foreach (var item in edates)
            {
                res.Add(item.Date.ToString("ddd dd MMMM, yyyy"));
            }
           

            return res;
        }

        public List<EventScheduleLandingDatesModel> GetEventSchedule(int id)
        {
            Eventt ev = db.eventt.Find(id);
            List<EventSchedule> EventSch = new List<EventSchedule>();
            List<EventDates> Dates = db.eventDates.Where(a => a.EventtId == id).ToList();

            foreach (var item in Dates)
            {
                List<EventSchedule> ss = db.eventSchedule.Where(a => a.eventDatesId == item.Id).ToList();
                EventSch.AddRange(ss);
            }



            List<EventScheduleLandingDatesModel> res = new List<EventScheduleLandingDatesModel>();

            foreach (var item in Dates)
            {
                EventScheduleLandingDatesModel EventScheduleObj = new EventScheduleLandingDatesModel();
                EventScheduleObj.dateTime = item.Date.Date;
                EventScheduleObj.dateTimeString = item.Date.ToString("dd MMMM");
                List<EventScheduleLandingTopicsModel> esltm = new List<EventScheduleLandingTopicsModel>();
                foreach (var d in EventSch.Where(a => a.eventDatesId == item.Id))
                {
                    EventScheduleLandingTopicsModel esltmobj = new EventScheduleLandingTopicsModel();
                    esltmobj.Title = d.Title;
                    esltmobj.Description = d.Description;
                    esltmobj.From = d.From;
                    esltmobj.To = d.To;
                    esltmobj.FromString = d.From.ToString("hh:mm tt");
                    esltmobj.ToString = d.To.ToString("hh:mm tt");
                    esltm.Add(esltmobj);
                }
                EventScheduleObj.Topics = esltm.OrderBy(a => a.From).ToList();
                res.Add(EventScheduleObj);
            }

            return res.OrderBy(a=>a.dateTime).ToList();
        }

        public List<int> GetGalleryImages(int id)
        {
        
            return db.eventGallery.Where(a=>a.EventtId == id).Select(a=>a.Id).ToList();
            
        }

        public RegisterationDataModel GetRegistrationData(string EventToken, string ContactToken)
        {
            Eventt ev = db.eventt.Where(a => a.Guidd == EventToken).FirstOrDefault();
            Contact c = db.contact.Where(a=>a.Guidd == ContactToken).FirstOrDefault();

            RegisterationDataModel res = db.registration.Where(a => a.EventtId == ev.Id && a.ContactId == c.Id)
                .Join(db.contact, a => a.ContactId, b => b.Id, (a, b) => new
                {
                    Id = a.Id,
                    ContactId = b.Id,
                    ContactName = b.ContactName,
                    Email = a.Email,
                    PhoneCodeId = a.PhoneCodeId,
                    MobileNumber = a.PhoneNumber
                }).Join(db.country, a => a.PhoneCodeId, b => b.Id, (a, b) => new RegisterationDataModel
                {
                    Id = a.Id,
                    ContactId = a.ContactId,
                    ContactName = a.ContactName,
                    Email = a.Email,
                    PhoneCodeId = a.PhoneCodeId,
                    MobileNumber = a.MobileNumber,
                    PhoneCode = b.PhoneCode
                }).FirstOrDefault();

            return res;
        }

        public List<Properties> GetSocialMedia()
        {
            return db.properties.Where(a => a.IsSocialMedia == true && a.Value != null && a.Value != "").ToList();
        }

        public bool NotInterested(NotInterestedModel obj)
        {
            Invitees inv = db.invitees.Where(a => a.ContactId == obj.ContactId && a.eventtId == obj.EventId).FirstOrDefault();
            inv.Accepted = false;
            db.SaveChanges();
            return true;
        }

        public bool NotInterested(string EventToken, string ContactToken)
        {
            Eventt ev = db.eventt.Where(a=>a.Guidd == EventToken).FirstOrDefault();
            Contact c = db.contact.Where(a => a.Guidd == ContactToken).FirstOrDefault();
            Invitees inv = db.invitees.Where(a => a.eventtId == ev.Id && a.ContactId == c.Id).FirstOrDefault();
            inv.Accepted = false;
            db.SaveChanges();
            return true;
        }

        public int Register(EventRegisterationModel obj)
        {

            var random = new Random();
            var randomCode = new string(Enumerable.Repeat(chars, codelength).Select(s => s[random.Next(s.Length)]).ToArray());



            Registration check = db.registration.Where(a => a.EventtId == obj.EventId && a.ContactId == obj.ContactId).FirstOrDefault();


            if (check == null)
            {

                DateTime now = ti.GetCurrentTime();

                string g = Guid.NewGuid().ToString().Replace("-", string.Empty);

                var qrGenerator = new QRCodeGenerator();

                var qrCodeData = qrGenerator.CreateQrCode(PropertiesModel.DashboardDomain + "/EntryOrganizer/AttendByToken.html" + "?" + g, QRCodeGenerator.ECCLevel.H);

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

                Registration r = new Registration();
                Contact c = db.contact.Find(obj.ContactId);
                Eventt ev = db.eventt.Find(obj.EventId);

                using (var stream = new MemoryStream())
                {

                    qrCodeBitmap.Save(stream, ImageFormat.Png);
                    var bytes = stream.ToArray();

                    r.ContactId = obj.ContactId;
                    r.EventtId = obj.EventId;
                    r.PhoneCodeId = obj.PhoneCodeId;
                    r.PhoneNumber = obj.PhoneNumber;
                    r.Email = obj.Email;
                    r.RegistrationCode = randomCode;
                    r.Guidd = g;
                    r.Data = bytes;
                    r.CreationDateTime = now;
                    db.registration.Add(r);
                    Invitees inv = db.invitees.Where(a => a.eventtId == obj.EventId && a.ContactId == obj.ContactId).FirstOrDefault();
                    inv.Accepted = true;
                    db.SaveChanges();


                }

                string body = File.ReadAllText(Directory.GetCurrentDirectory()+"/Templates/RegistrationEmail.html");
                string bodyy = body.Replace("[[ContactName]]", c.ContactName).Replace("[[EventName]]", ev.EventName).Replace("[[QRCodeDomain1]]", PropertiesModel.BackEndDomain + "/Images/GetRegistrationQRCode/" + g).Replace("[[QRCodeDomain2]]", PropertiesModel.BackEndDomain + "/Images/GetRegistrationQRCode/" + g).Replace("[[RegistrationCode]]", randomCode);
                MailMessage m = new MailMessage();
                m.To.Add(obj.Email);
                m.Subject = ev.EventName + " Registration Code";
                m.From = new MailAddress(PropertiesModel.EmailAddress);
                m.Sender = new MailAddress(PropertiesModel.EmailAddress);
                m.IsBodyHtml = true;
                m.Body = bodyy;
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(PropertiesModel.SmtpServer, PropertiesModel.EmailPort);
                smtp.EnableSsl = false;
                smtp.Credentials = new NetworkCredential(PropertiesModel.EmailAddress, PropertiesModel.EmailPassword);
                smtp.Send(m);
                return r.Id;
            }

            else
            {
                return 0;
            }
        }
    }
}
 