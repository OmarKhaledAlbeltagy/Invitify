using Invitify.Context;
using Invitify.Entities;
using Invitify.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using static System.Text.StringBuilder;
using MoreLinq;
using Microsoft.AspNetCore.SignalR;

namespace Invitify.Repos
{
    public class EntryOrganizerRep:IEntryOrganizerRep
    {
        private readonly DbContainer db;
        private readonly ITimeRep ti;

        public EntryOrganizerRep(DbContainer db, ITimeRep ti)
        {
            this.db = db;
            this.ti = ti;
        }

        public dynamic CheckCode(string code)
        {
            Registration r = db.registration.Where(a => a.RegistrationCode == code).FirstOrDefault();

            if (r == null)
            {
                return false;
            }

            else
            {
                DateTime now = ti.GetCurrentTime();
                Eventt ev = db.eventt.Find(r.EventtId);
                Contact c = db.contact.Find(r.ContactId);

                
                List<DateTime> CheckAttend = db.attendance.Where(a => a.ContactId == r.ContactId && a.EventtId == r.EventtId && a.AttendanceDateTime.Date == now.Date).OrderByDescending(a=>a.AttendanceDateTime).Select(a=>a.AttendanceDateTime).ToList();

                if (CheckAttend.Count == 0)
                {
                    return true;
                }
                else
                {
                    List<string> dates = new List<string>();

                    foreach (var item in CheckAttend)
                    {
                        dates.Add(item.ToString("dddd dd MMMM yyyy - hh:mm tt"));
                    }
                    return dates;
                }
            }
        }

        public EventRegistrationListModel GetEventRegsitrations(int EventId)
        {
            EventRegistrationListModel res = new EventRegistrationListModel();
            res.EventName = db.eventt.Find(EventId).EventName;
            List<Registration> reg = db.registration.Where(a => a.EventtId == EventId).ToList();

            List<Invitees> inv = db.invitees.Where(a => a.eventtId == EventId).ToList();

            List<RegistrationListModel> reglist = new List<RegistrationListModel>();

            foreach (var item in reg)
            {
                RegistrationListModel obj = new RegistrationListModel();
                Contact c = db.contact.Find(item.ContactId);
                obj.Id = item.Id;
                obj.ContactId = item.ContactId;
                obj.ContactName = c.ContactName;
                obj.Email = c.Email;
                obj.RegisteredEmail = item.Email;

                if (c.PhoneCodeId == null || c.MobileNumber == "" || c.MobileNumber == null)
                {
                    obj.MobileNumber = "";
                }
                else
                {
                    Country phonecode = db.country.Find(c.PhoneCodeId);
                    obj.MobileNumber = phonecode.PhoneCode + c.MobileNumber;
                }


                Country regphonecode = db.country.Find(item.PhoneCodeId);
                obj.RegisteredMobileNumber = regphonecode.PhoneCode + item.PhoneNumber;



                reglist.Add(obj);
            }


            List<RegistrationListModel> invlist = new List<RegistrationListModel>();

            foreach (var item in inv)
            {
                RegistrationListModel check = reglist.Where(a => a.ContactId == item.ContactId).FirstOrDefault();

                if (check == null)
                {
                    RegistrationListModel obj = new RegistrationListModel();
                    Contact c = db.contact.Find(item.ContactId);

                    obj.Id = item.Id;
                    obj.ContactId = item.ContactId;
                    obj.ContactName = c.ContactName;
                    obj.Email = c.Email;
                    obj.RegisteredEmail = "";
                    obj.RegisteredMobileNumber = "";


                    if (c.PhoneCodeId == null || c.MobileNumber == "" || c.MobileNumber == null)
                    {
                        obj.MobileNumber = "";
                    }
                    else
                    {
                        Country phonecode = db.country.Find(c.PhoneCodeId);
                        obj.MobileNumber = phonecode.PhoneCode + c.MobileNumber;
                    }


                    invlist.Add(obj);

                }

            }



            res.registrations = reglist.OrderBy(a => a.ContactName).ToList();
            res.invitees = invlist.OrderBy(a => a.ContactName).ToList();


            return res;

        }

        public int GetTodayEvent(string UserId)
        {
            DateTime now = ti.GetCurrentTime();
            List<int> todayevents = db.eventDates.Where(a => a.Date.Date == now.Date).Select(a=>a.EventtId).ToList();
            List<int> userevents = db.eventEntryOrganizer.Where(a => a.ExtendIdentityUserId == UserId).Select(a => a.EventtId).ToList();

            foreach (var t in todayevents)
            {
                foreach (var u in userevents)
                {
                    if (t == u)
                    {
                        return t;
                    }
                }
            }

            List<int> yesterdayevents = db.eventDates.Where(a => a.Date.Date == now.AddDays(-1).Date).Select(a => a.EventtId).ToList();

            foreach (var t in yesterdayevents)
            {
                foreach (var u in userevents)
                {
                    if (t == u)
                    {
                        return t;
                    }
                }
            }

            return 0;

        }

        public bool SaveAttendance(string code, string UserId)
        {
            DateTime now = ti.GetCurrentTime();
            Registration r = db.registration.Where(a => a.RegistrationCode == code).FirstOrDefault();

            if (r == null)
            {
                return false;
            }
            else
            {
                Attendance obj = new Attendance();
                obj.ExtendIdentityUserId = UserId;
                obj.EventtId = r.EventtId;
                obj.ContactId = r.ContactId;
                obj.AttendanceDateTime = now;
                db.attendance.Add(obj);
                db.SaveChanges();
                return true;
            }
         

        }

        public bool SendInvitationEmail(int InvId)
        {
            Invitees inv = db.invitees.Find(InvId);
            Contact c = db.contact.Find(inv.ContactId);
            Eventt ev = db.eventt.Find(inv.eventtId);
            State st = db.state.Find(ev.StateId);
            Country co = db.country.Find(st.CountryId);
            string FirstDate = db.eventDates.Where(a => a.EventtId == ev.Id).OrderBy(a => a.Date).Select(a => a.Date).ToList().FirstOrDefault().Date.ToString("MMMM dd, yyyy");

           var abouthtml = WebUtility.HtmlDecode(ev.About);


            string body = File.ReadAllText(Directory.GetCurrentDirectory() + "/Templates/index.html");
            body = body.Replace("[[EventName]]", ev.EventName).Replace("[[FirstDate]]", FirstDate).Replace("[[EventDomain1]]", ev.Domain + "?" + ev.Guidd + "&" + c.Guidd).Replace("[[EventDomain2]]", ev.Domain + "?" + ev.Guidd + "&" + c.Guidd).Replace("[[RegisterButton1]]", "Register Now !").Replace("[[RegisterButton2]]", "Register Now !").Replace("[[ContactName]]", c.ContactName).Replace("[[EmailBody]]", "").Replace("[[EventAbout]]", abouthtml).Replace("[[EventAddress]]", ev.Address).Replace("[[StateName]]", st.StateName).Replace("[[CountryName]]", co.CountryName);

            if (ev.Speakers == null || ev.Speakers == 0)
            {
                body = body.Replace("<td valign=\"middle\" width=\"50%\" style=\"-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;mso-table-lspace: 0pt !important;mso-table-rspace: 0pt !important;\">\r\n\t\t\t\t\t\t\t\t\t<table role=\"presentation\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\" style=\"-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;mso-table-lspace: 0pt !important;mso-table-rspace: 0pt !important;border-spacing: 0 !important;border-collapse: collapse !important;table-layout: fixed !important;margin: 0 auto !important;\">\r\n\t\t\t\t\t\t\t\t\t\t<tr style=\"-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;\">\r\n\t\t\t\t\t\t\t\t\t\t\t<td class=\"counter-text\" style=\"-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;text-align: center;mso-table-lspace: 0pt !important;mso-table-rspace: 0pt !important;\">\r\n\t\t\t\t\t\t\t\t\t\t\t\t<span class=\"num\" style=\"-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;display: block;color: #ffffff;font-size: 34px;font-weight: 700;\">[[SpeakersCount]]+</span>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<span class=\"name\" style=\"-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;display: block;color: rgba(255,255,255,.9);font-size: 13px;\">Speakers</span>\r\n\t\t\t\t\t\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t\t\t\t\t</tr>\r\n\t\t\t\t\t\t\t\t\t</table>\r\n\t\t\t\t\t\t\t\t</td>", "");
            }
            else
            {
                body = body.Replace("[[SpeakersCount]]", ev.Speakers.ToString());
            }

            if (ev.Participants == null || ev.Participants == 0)
            {
                body = body.Replace("<td valign=\"middle\" width=\"50%\" style=\"-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;mso-table-lspace: 0pt !important;mso-table-rspace: 0pt !important;\">\r\n\t\t\t\t\t\t\t\t\t<table role=\"presentation\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\" style=\"-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;mso-table-lspace: 0pt !important;mso-table-rspace: 0pt !important;border-spacing: 0 !important;border-collapse: collapse !important;table-layout: fixed !important;margin: 0 auto !important;\">\r\n\t\t\t\t\t\t\t\t\t\t<tr style=\"-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;\">\r\n\t\t\t\t\t\t\t\t\t\t\t<td class=\"counter-text\" style=\"-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;text-align: center;mso-table-lspace: 0pt !important;mso-table-rspace: 0pt !important;\">\r\n\t\t\t\t\t\t\t\t\t\t\t\t<span class=\"num\" style=\"-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;display: block;color: #ffffff;font-size: 34px;font-weight: 700;\">[[ParticipantsCount]]+</span>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<span class=\"name\" style=\"-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;display: block;color: rgba(255,255,255,.9);font-size: 13px;\">Participants</span>\r\n\t\t\t\t\t\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t\t\t\t\t</tr>\r\n\t\t\t\t\t\t\t\t\t</table>\r\n\t\t\t\t\t\t\t\t</td>", "");
            }
            else
            {
                body = body.Replace("[[ParticipantsCount]]", ev.Participants.ToString());
            }

            MailMessage m = new MailMessage();
            m.To.Add(c.Email);
            m.Subject = ev.EventName+" Invitation";
            m.From = new MailAddress(PropertiesModel.EmailAddress);
            m.Sender = new MailAddress(PropertiesModel.EmailAddress);
            m.IsBodyHtml = true;
            m.Body = body;
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(PropertiesModel.SmtpServer, PropertiesModel.EmailPort);
            smtp.EnableSsl = false;
            smtp.Credentials = new NetworkCredential(PropertiesModel.EmailAddress, PropertiesModel.EmailPassword);
            smtp.Send(m);

            return true;
        }

        public bool SendRegistrationEmailToContactEmail(int RegId)
        {
            Registration r = db.registration.Find(RegId);
            Contact c = db.contact.Find(r.ContactId);
            Eventt ev = db.eventt.Find(r.EventtId);

            string body = File.ReadAllText(Directory.GetCurrentDirectory() + "/Templates/RegistrationEmail.html");
            string bodyy = body.Replace("[[ContactName]]", c.ContactName).Replace("[[EventName]]", ev.EventName).Replace("[[QRCodeDomain1]]", PropertiesModel.BackEndDomain + "/Images/GetRegistrationQRCode/" + r.Guidd).Replace("[[QRCodeDomain2]]", PropertiesModel.BackEndDomain + "/Images/GetRegistrationQRCode/" + r.Guidd).Replace("[[RegistrationCode]]", r.RegistrationCode);
            MailMessage m = new MailMessage();
            m.To.Add(c.Email);
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

        public bool SendRegistrationEmailToRegisteredEmail(int RegId)
        {
            Registration r = db.registration.Find(RegId);
            Contact c = db.contact.Find(r.ContactId);
            Eventt ev = db.eventt.Find(r.EventtId);

            string body = File.ReadAllText(Directory.GetCurrentDirectory() + "/Templates/RegistrationEmail.html");
            string bodyy = body.Replace("[[ContactName]]", c.ContactName).Replace("[[EventName]]", ev.EventName).Replace("[[QRCodeDomain1]]", PropertiesModel.BackEndDomain + "/Images/GetRegistrationQRCode/" + r.Guidd).Replace("[[QRCodeDomain2]]", PropertiesModel.BackEndDomain + "/Images/GetRegistrationQRCode/" + r.Guidd).Replace("[[RegistrationCode]]", r.RegistrationCode);
            MailMessage m = new MailMessage();
            m.To.Add(r.Email);
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
    }
}
