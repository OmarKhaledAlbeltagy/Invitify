using Invitify.Context;
using Invitify.Entities;
using Invitify.Models;
using System.Collections.Generic;

namespace Invitify.Repos
{
    public class ContactTypeRep:IContactTypeRep
    {
        private readonly DbContainer db;

        public ContactTypeRep(DbContainer db)
        {
            this.db = db;
        }

        public ContactType AddContactType(ContactType obj)
        {
            db.contactType.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public bool DeleteContactType(int id)
        {
            ContactType obj = db.contactType.Find(id);
            db.contactType.Remove(obj);
            db.SaveChanges();
            return true;
        }

        public bool EditContactType(ContactType obj)
        {
            ContactType contacttype = db.contactType.Find(obj.Id);
            contacttype.ContactTypeName = obj.ContactTypeName;
            db.SaveChanges();
            return true;
        }

        public List<ContactTypeModel> GetAllContactType()
        {
            List<ContactTypeModel> res = new List<ContactTypeModel>();
            List<ContactType> list = db.contactType.Where(a=>a.IsAuto == false).OrderBy(a => a.ContactTypeName).ToList();

            foreach (ContactType item in list)
            {
                ContactTypeModel obj = new ContactTypeModel();
                obj.ContactTypeName = item.ContactTypeName;
                obj.Id = item.Id;
                Contact c = db.contact.Where(a => a.ContactTypeId == item.Id).FirstOrDefault();
                if (c == null)
                {
                    obj.CanDeleted = true;
                }
                else
                {
                    obj.CanDeleted = false;
                }
                res.Add(obj);
            }

            return res; 
        }
    }
}
