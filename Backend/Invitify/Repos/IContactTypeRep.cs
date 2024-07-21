using Invitify.Entities;
using Invitify.Models;

namespace Invitify.Repos
{
    public interface IContactTypeRep
    {
        ContactType AddContactType(ContactType obj);

        bool EditContactType(ContactType obj);

        List<ContactTypeModel> GetAllContactType();

        bool DeleteContactType(int id);
    }
}
