using Invitify.Entities;
using Invitify.Models;

namespace Invitify.Repos
{
    public interface IPropertiesRep
    {
        List<Properties> GetAllProperties();

        bool EditProperty(EditPropertyModel obj);

        bool AddLogo(AddLogoModel obj);

        bool MigrationQrCodes();

        bool CheckLogo();
    }
}
