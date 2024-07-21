using Invitify.Context;
using Invitify.Entities;
using Invitify.Models;
using Microsoft.EntityFrameworkCore.Internal;
using MoreLinq.Extensions;

namespace Invitify.Repos
{
    public class ContactRep : IContactRep
    {
        private readonly DbContainer db;
        private readonly ITimeRep ti;

        public ContactRep(DbContainer db,ITimeRep ti)
        {
            this.db = db;
            this.ti = ti;
        }

        public bool AddContact(AddContactModel obj)
        {
            int checkcontact = db.contact.Count();
            if (checkcontact >= PropertiesModel.ContactsLimit)
            {
                return false;
            }

            DateTime now = ti.GetCurrentTime();
            Contact c = new Contact();
            c.ContactName = obj.ContactName;
            c.Address = obj.Address;
            c.Gender = obj.Gender;
            c.ContactTypeId = obj.ContactTypeId;
            c.CreationDateTime = now;
            c.LastModifiedDateTime = now;
            c.Email = obj.Email;
            c.Notes = obj.Notes;
            c.MobileNumber = obj.MobileNumber;
            c.StateId = obj.StateId;
            c.PhoneCodeId = obj.PhoneCodeId;
            db.contact.Add(c);
            db.SaveChanges();

            return true;
        }

        public bool DeleteContact(int id)
        {
            Contact c =  db.contact.Find(id);
            db.contact.Remove(c);
            db.SaveChanges();
            return true;
        }

        public bool EditContact(EditContactModel obj)
        {

            Invitees check = db.invitees.Where(a => a.ContactId == obj.Id).FirstOrDefault();
            DateTime now = ti.GetCurrentTime();
            Contact c = db.contact.Find(obj.Id);

            if (check == null)
            {
                c.ContactName = obj.ContactName;
                c.Address = obj.Address;
                c.Gender = obj.Gender;
                c.ContactTypeId = obj.ContactTypeId;
                c.LastModifiedDateTime = now;
                c.Email = obj.Email;
                c.Notes = obj.Notes;
                c.MobileNumber = obj.MobileNumber;
                c.StateId = obj.StateId;
                c.PhoneCodeId = obj.PhoneCodeId;
                db.SaveChanges();
              
            }
            else
            {
                c.Address = obj.Address;
                c.Gender = obj.Gender;
                c.ContactTypeId = obj.ContactTypeId;
                c.LastModifiedDateTime = now;
                c.Email = obj.Email;
                c.Notes = obj.Notes;
                c.MobileNumber = obj.MobileNumber;
                c.StateId = obj.StateId;
                c.PhoneCodeId = obj.PhoneCodeId;
                db.SaveChanges();
            }

            return true;
        }

        public List<ContactModel> GetAllContact()
        {

            List<ContactModel> res = new List<ContactModel>();

            List<ContactModel> res1 = db.contact.Join(db.contactType, a => a.ContactTypeId, b => b.Id, (a, b) => new
            {
                Id = a.Id,
                ContactName = a.ContactName,
                ContactTypeId = b.Id,
                ContactTypeName = b.ContactTypeName,
                Gender = a.Gender,
                StateId = a.StateId,
                Address = a.Address,
                MobileNumber = a.MobileNumber,
                Email = a.Email,
                Notes = a.Notes,
                CreationDateTime = a.CreationDateTime,
                LastModifiedDateTime = a.LastModifiedDateTime,
                PhoneCodeId = a.PhoneCodeId
            }).Join(db.state, a => a.StateId, b => b.Id, (a, b) => new
            {
                Id = a.Id,
                ContactName = a.ContactName,
                ContactTypeId = a.ContactTypeId,
                ContactTypeName = a.ContactTypeName,
                Gender = a.Gender,
                StateId = a.StateId,
                StateName = b.StateName,
                CountryId = b.CountryId,
                Address = a.Address,
                MobileNumber = a.MobileNumber,
                Email = a.Email,
                Notes = a.Notes,
                CreationDateTime = a.CreationDateTime,
                LastModifiedDateTime = a.LastModifiedDateTime,
                PhoneCodeId = a.PhoneCodeId
            }).Join(db.country, a => a.CountryId, b => b.Id, (a, b) => new 
            {
                Id = a.Id,
                ContactName = a.ContactName,
                ContactTypeId = a.ContactTypeId,
                ContactTypeName = a.ContactTypeName,
                Gender = a.Gender,
                StateName = a.StateName,
                StateId= a.StateId,
                CountryId = a.CountryId,
                CountryName = b.CountryName,
                Address = a.Address,
                MobileNumber = a.MobileNumber,
                Email = a.Email,
                Notes = a.Notes,
                CreationDateTime = a.CreationDateTime,
                LastModifiedDateTime = a.LastModifiedDateTime,
                PhoneCodeId = a.PhoneCodeId
            }).Join(db.country, a => a.PhoneCodeId, b => b.Id, (a, b) => new ContactModel
            {
                Id = a.Id,
                ContactName = a.ContactName,
                ContactTypeId = a.ContactTypeId,
                ContactTypeName = a.ContactTypeName,
                Gender = a.Gender,
                StateName = a.StateName,
                StateId = a.StateId,
                CountryId = a.CountryId,
                CountryName = b.CountryName,
                Address = a.Address,
                MobileNumber = a.MobileNumber,
                Email = a.Email,
                Notes = a.Notes,
                CreationDateTime = a.CreationDateTime.ToString("dd MMMM yyyy - hh:mm tt"),
                LastModifiedDateTime = a.LastModifiedDateTime.ToString("dd MMMM yyyy - hh:mm tt"),
                PhoneCodeId = a.PhoneCodeId,
                PhoneCode = b.PhoneCode
            }).OrderBy(a => a.ContactName).ToList();

            List<ContactModel> res2 = db.contact.Join(db.contactType, a => a.ContactTypeId, b => b.Id, (a, b) => new
            {
                Id = a.Id,
                ContactName = a.ContactName,
                ContactTypeId = b.Id,
                ContactTypeName = b.ContactTypeName,
                Gender = a.Gender,
                StateId = a.StateId,
                Address = a.Address,
                MobileNumber = a.MobileNumber,
                Email = a.Email,
                Notes = a.Notes,
                CreationDateTime = a.CreationDateTime,
                LastModifiedDateTime = a.LastModifiedDateTime,
                PhoneCodeId = a.PhoneCodeId
            }).Join(db.state, a => a.StateId, b => b.Id, (a, b) => new
            {
                Id = a.Id,
                ContactName = a.ContactName,
                ContactTypeId = a.ContactTypeId,
                ContactTypeName = a.ContactTypeName,
                Gender = a.Gender,
                StateId = a.StateId,
                StateName = b.StateName,
                CountryId = b.CountryId,
                Address = a.Address,
                MobileNumber = a.MobileNumber,
                Email = a.Email,
                Notes = a.Notes,
                CreationDateTime = a.CreationDateTime,
                LastModifiedDateTime = a.LastModifiedDateTime,
                PhoneCodeId = a.PhoneCodeId
            }).Join(db.country, a => a.CountryId, b => b.Id, (a, b) => new ContactModel
            {
                Id = a.Id,
                ContactName = a.ContactName,
                ContactTypeId = a.ContactTypeId,
                ContactTypeName = a.ContactTypeName,
                Gender = a.Gender,
                StateName = a.StateName,
                StateId = a.StateId,
                CountryId = a.CountryId,
                CountryName = b.CountryName,
                Address = a.Address,
                MobileNumber = a.MobileNumber,
                Email = a.Email,
                Notes = a.Notes,
                CreationDateTime = a.CreationDateTime.ToString("dd MMMM yyyy - hh:mm tt"),
                LastModifiedDateTime = a.LastModifiedDateTime.ToString("dd MMMM yyyy - hh:mm tt"),
                PhoneCodeId = a.PhoneCodeId
            }).OrderBy(a => a.ContactName).ToList();

            foreach (var item in res1)
            {
                Invitees Check = db.invitees.Where(a => a.ContactId == item.Id).FirstOrDefault();
                if (Check == null)
                {
                    item.CanDeleted = true;
                }
                else
                {
                    item.CanDeleted = false;
                }
                res.Add(item);
            }

            foreach (var item in res2)
            {

                Invitees Check = db.invitees.Where(a => a.ContactId == item.Id).FirstOrDefault();
                if (Check == null)
                {
                    item.CanDeleted = true;
                }
                else
                {
                    item.CanDeleted = false;
                }
                res.Add(item);
            }
            
            return DistinctByExtension.DistinctBy(res, a => a.Id).ToList();

        }
    }
}
