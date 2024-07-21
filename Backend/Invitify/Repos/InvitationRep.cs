using Invitify.Context;
using Invitify.Entities;
using Invitify.Models;
using Microsoft.EntityFrameworkCore.Internal;
using MoreLinq;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using MoreLinq;
using MoreLinq.Extensions;

namespace Invitify.Repos
{
    public class InvitationRep : IInvitationRep
    {
        private readonly DbContainer db;
        private readonly ITimeRep ti;

        public InvitationRep(DbContainer db, ITimeRep ti)
        {
            this.db = db;
            this.ti = ti;
        }


        public bool AddToInvitees(AddToInviteesModel obj)
        {
            DateTime now = ti.GetCurrentTime();

            Eventt ev = db.eventt.Find(obj.EventId);

            foreach (var item in obj.ContactsId)
            {

                Contact c = db.contact.Find(item);

                Invitees check = db.invitees.Where(a => a.eventtId == obj.EventId && a.ContactId == item).FirstOrDefault();

                if (check == null)
                {

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
                        inv.Data = bytes;
                        inv.CreationDateTime = now;
                        db.invitees.Add(inv);
                    }

                }




            }
            db.SaveChanges();
            return true;
        }

        public List<SimpleContactModel> GetEventInvitees(int id)
        {
            List<SimpleContactModel> res1 = db.invitees.Where(a => a.eventtId == id).Join(db.contact, a => a.ContactId, b => b.Id, (a, b) => new
            {
                Id = b.Id,
                InviteesId = a.Id,
                ContactName = b.ContactName,
                MobileNumber = b.MobileNumber,
                Email = b.Email,
                PhoneCodeId = b.PhoneCodeId,
                Address = b.Address,
                StateId = b.StateId,
                ContactTypeId = b.ContactTypeId,
                IsEmail = a.IsEmail
            }).Join(db.state, a => a.StateId, b => b.Id, (a, b) => new
            {
                Id = a.Id,
                InviteesId = a.InviteesId,
                ContactName = a.ContactName,
                MobileNumber = a.MobileNumber,
                Email = a.Email,
                PhoneCodeId = a.PhoneCodeId,
                Address = a.Address,
                StateName = b.StateName,
                ContactTypeId = a.ContactTypeId,
                IsEmail = a.IsEmail,
                CountryId = b.CountryId
            }).Join(db.country, a => a.CountryId, b => b.Id, (a, b) => new
            {
                Id = a.Id,
                InviteesId = a.InviteesId,
                ContactName = a.ContactName,
                MobileNumber = a.MobileNumber,
                Email = a.Email,
                PhoneCodeId = a.PhoneCodeId,
                Address = a.Address,
                StateName = a.StateName,
                ContactTypeId = a.ContactTypeId,
                IsEmail = a.IsEmail,
                CountryName = b.CountryName
            }).Join(db.contactType, a => a.ContactTypeId, b => b.Id, (a, b) => new
            {
                Id = a.Id,
                InviteesId = a.InviteesId,
                ContactName = a.ContactName,
                MobileNumber = a.MobileNumber,
                Email = a.Email,
                PhoneCodeId = a.PhoneCodeId,
                Address = a.Address,
                StateName = a.StateName,
                ContactTypeName = b.ContactTypeName,
                IsEmail = a.IsEmail,
                CountryName = a.CountryName
            }).Join(db.country, a => a.PhoneCodeId, b => b.Id, (a, b) => new SimpleContactModel
            {
                Id = a.Id,
                InviteesId = a.InviteesId,
                ContactName = a.ContactName,
                MobileNumber = a.MobileNumber,
                Email = a.Email,
                PhoneCode = b.PhoneCode,
                Address = a.Address,
                StateName = a.StateName,
                ContactTypeName = a.ContactTypeName,
                IsEmail = a.IsEmail,
                CountryName = a.CountryName
            }).OrderBy(a => a.ContactName).ToList();

            List<SimpleContactModel> res2 = db.invitees.Where(a => a.eventtId == id).Join(db.contact, a => a.ContactId, b => b.Id, (a, b) => new
            {
                Id = b.Id,
                InviteesId = a.Id,
                ContactName = b.ContactName,
                MobileNumber = b.MobileNumber,
                Email = b.Email,
                Address = b.Address,
                StateId = b.StateId,
                ContactTypeId = b.ContactTypeId,
                IsEmail = a.IsEmail
            }).Join(db.state, a => a.StateId, b => b.Id, (a, b) => new
            {
                Id = a.Id,
                InviteesId = a.InviteesId,
                ContactName = a.ContactName,
                MobileNumber = a.MobileNumber,
                Email = a.Email,
                Address = a.Address,
                StateName = b.StateName,
                ContactTypeId = a.ContactTypeId,
                IsEmail = a.IsEmail,
                CountryId = b.CountryId
            }).Join(db.country, a => a.CountryId, b => b.Id, (a, b) => new
            {
                Id = a.Id,
                InviteesId = a.InviteesId,
                ContactName = a.ContactName,
                MobileNumber = a.MobileNumber,
                Email = a.Email,
                Address = a.Address,
                StateName = a.StateName,
                ContactTypeId = a.ContactTypeId,
                IsEmail = a.IsEmail,
                CountryName = b.CountryName
            }).Join(db.contactType, a => a.ContactTypeId, b => b.Id, (a, b) => new SimpleContactModel
            {
                Id = a.Id,
                InviteesId = a.InviteesId,
                ContactName = a.ContactName,
                MobileNumber = a.MobileNumber,
                Email = a.Email,
                Address = a.Address,
                StateName = a.StateName,
                ContactTypeName = b.ContactTypeName,
                IsEmail = a.IsEmail,
                CountryName = a.CountryName
            }).OrderBy(x => x.ContactName).ToList();


            List<SimpleContactModel> result = new List<SimpleContactModel>();
            result.AddRange(res1);
            result.AddRange(res2);

            return DistinctByExtension.DistinctBy(result, a => a.Id).OrderBy(a => a.ContactName).ToList();
        }

        public List<InviteesContactModel> GetInviteesData()
        {
            List<Eventt> events = db.eventt.ToList();
            List<Contact> c = db.contact.ToList();
            List<InviteesContactModel> res = new List<InviteesContactModel>();
            foreach (var item in events)
            {

                List<SimpleContactModel> NotInvited = new List<SimpleContactModel>();

                List<SimpleContactModel> Invited = db.invitees.Where(a => a.eventtId == item.Id).Join(db.contact, a => a.ContactId, b => b.Id, (a, b) => new SimpleContactModel
                {
                    Id = b.Id,
                    ContactId = b.Id,
                    ContactName = b.ContactName
                }).ToList();

                foreach (var con in c)
                {

                    if (Invited.Any(a => a.Id == con.Id) == false)
                    {
                      
                        SimpleContactModel not = new SimpleContactModel();
                        not.Id = con.Id;
                        not.ContactName = con.ContactName;
                        NotInvited.Add(not);

                    }

                }


                List<SimpleContactModel> Invited2 = new List<SimpleContactModel>();
                foreach (var inv in Invited)
                {
                    Registration check = db.registration.Where(a => a.ContactId == inv.ContactId && a.EventtId == item.Id).FirstOrDefault();

                    if (check == null)
                    {
                        Invited2.Add(inv);
                    }
                }

                InviteesContactModel obj = new InviteesContactModel();
                obj.EventId = item.Id;
                obj.Invited = Invited2.OrderBy(a => a.ContactName).ToList();
                obj.NotInvited = NotInvited.OrderBy(a => a.ContactName).ToList();
                obj.NumberOfInvitees = Invited.Count;
                obj.NumberOfRegistrations = db.registration.Where(a=>a.EventtId == item.Id).Count();
                res.Add(obj);

            }

            return res;


        }

        public bool RemoveFromInvitees(AddToInviteesModel obj)
        {
            foreach (var item in obj.ContactsId)
            {
                Invitees inv = db.invitees.Where(a=>a.eventtId == obj.EventId && a.ContactId == item).FirstOrDefault();
                db.invitees.Remove(inv);
            }
            db.SaveChanges();
            return true;
        }
    }
}
