using Invitify.Context;
using Invitify.Models;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Routing.Template;
using Invitify.Entities;

namespace Invitify.Repos
{
    public class EmailRep:IEmailRep
    {
        private readonly DbContainer db;

        public EmailRep(DbContainer db)
        {
            this.db = db;
        }

        public bool SendInvitationMail(InvitationMailModel obj)
        {
            Eventt ev = db.eventt.Find(db.invitees.Find(obj.InviteesId[0]).eventtId);
            string FirstDate = db.eventDates.Where(a => a.EventtId == ev.Id).OrderBy(a => a.Date).FirstOrDefault().Date.ToString("MMMM dd, yyyy");
            State st = db.state.Find(ev.StateId);
            Country co = db.country.Find(st.CountryId);


            var abouthtml = WebUtility.HtmlDecode(ev.About);


            foreach (var item in obj.InviteesId)
            {
                Invitees inv = db.invitees.Find(item);
                Contact c = db.contact.Find(inv.ContactId);
                if (c.Email == "" || c.Email == null)
                {
                
                }
                else
                {

                    try
                    {
                        string body = File.ReadAllText(Directory.GetCurrentDirectory() + "/Templates/index.html");
                        body = body.Replace("[[EventName]]", ev.EventName).Replace("[[FirstDate]]", FirstDate).Replace("[[EventDomain1]]", ev.Domain + "?" + ev.Guidd + "&" + c.Guidd).Replace("[[EventDomain2]]", ev.Domain + "?" + ev.Guidd + "&" + c.Guidd).Replace("[[RegisterButton1]]", obj.ButtonText).Replace("[[RegisterButton2]]", obj.ButtonText).Replace("[[ContactName]]", c.ContactName).Replace("[[EmailBody]]", obj.EmailBody).Replace("[[EventAbout]]", abouthtml).Replace("[[EventAddress]]", ev.Address).Replace("[[StateName]]", st.StateName).Replace("[[CountryName]]", co.CountryName);

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
                        m.Subject = obj.EmailSubject;
                        m.From = new MailAddress(PropertiesModel.EmailAddress);
                        m.Sender = new MailAddress(PropertiesModel.EmailAddress);
                        m.IsBodyHtml = true;
                        m.Body = body;
                        m.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient(PropertiesModel.SmtpServer, PropertiesModel.EmailPort);
                        smtp.EnableSsl = false;
                        smtp.Credentials = new NetworkCredential(PropertiesModel.EmailAddress, PropertiesModel.EmailPassword);
                        smtp.Send(m);

                        inv.IsEmail = true;
                    }
                    catch (Exception ex)
                    {

                        continue;
                    }

                    

                }
               

            }
            db.SaveChanges();
            return true;


        }

        public bool SendTestMail(TestMailModel obj)
        {
            Invitees inv = db.invitees.Find(obj.InviteesId);
            Contact c = db.contact.Find(inv.ContactId);
            Eventt ev = db.eventt.Find(inv.eventtId);
            string FirstDate = db.eventDates.Where(a => a.EventtId == ev.Id).OrderBy(a => a.Date).FirstOrDefault().Date.ToString("MMMM dd, yyyy");
            State st = db.state.Find(ev.StateId);
            Country co = db.country.Find(st.CountryId);

            var abouthtml = WebUtility.HtmlDecode(ev.About);


            string body = File.ReadAllText(Directory.GetCurrentDirectory() + "/Templates/index.html");
            string bodyy = body.Replace("[[EventName]]", ev.EventName).Replace("[[FirstDate]]", FirstDate).Replace("[[EventDomain1]]", ev.Domain).Replace("[[EventDomain2]]",ev.Domain+"?"+ev.Guidd+"&"+c.Guidd).Replace("[[RegisterButton1]]",obj.ButtonText).Replace("[[RegisterButton2]]",obj.ButtonText).Replace("[[ContactName]]", c.ContactName).Replace("[[EmailBody]]",obj.EmailBody).Replace("[[EventAbout]]",abouthtml).Replace("[[SpeakersCount]]",ev.Speakers.ToString()).Replace("[[ParticipantsCount]]",ev.Participants.ToString()).Replace("[[EventAddress]]",ev.Address).Replace("[[StateName]]",st.StateName).Replace("[[CountryName]]", co.CountryName);
            MailMessage m = new MailMessage();
            m.To.Add(obj.TestMail);
            m.Subject = obj.EmailSubject;
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
