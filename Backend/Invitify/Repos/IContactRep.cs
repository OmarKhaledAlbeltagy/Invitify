using Invitify.Entities;
using Invitify.Models;

namespace Invitify.Repos
{
    public interface IContactRep
    {
        List<ContactModel> GetAllContact();

        bool AddContact(AddContactModel obj);

        bool EditContact(EditContactModel obj);

        bool DeleteContact(int id);
    }
}
