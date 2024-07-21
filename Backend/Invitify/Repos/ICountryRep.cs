using Invitify.Entities;
using Invitify.Models;

namespace Invitify.Repos
{
    public interface ICountryRep
    {
        List<Country> GetAllCountries();

        List<State> GetAllStates();

        List<PhoneCodeModel> GetAllPhoneCode();
    }
}
