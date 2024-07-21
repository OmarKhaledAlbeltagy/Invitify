using Invitify.Context;
using Invitify.Entities;
using Invitify.Models;

namespace Invitify.Repos
{
    public class CountryRep : ICountryRep
    {
        private readonly DbContainer db;

        public CountryRep(DbContainer db)
        {
            this.db = db;
        }


        public List<Country> GetAllCountries()
        {
            List<Country> res = db.country.ToList();
            return res;
        }

        public List<PhoneCodeModel> GetAllPhoneCode()
        {
            List<Country> c = db.country.ToList();
            List<PhoneCodeModel> res = new List<PhoneCodeModel>();
            foreach (var item in c)
            {
                PhoneCodeModel obj = new PhoneCodeModel();
                obj.Id = item.Id;
                obj.PhoneCode = item.PhoneCode;
                obj.Iso = item.Iso;
                obj.Emoji= item.Emoji;
                res.Add(obj);
            }
            return res;
        }

        public List<State> GetAllStates()
        {
            List<State> res = db.state.ToList();
            return res;
        }
    }
}
