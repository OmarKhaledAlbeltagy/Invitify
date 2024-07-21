namespace Invitify.Entities
{
    public class Country
    {
        public int Id { get; set; }

        public string CountryName { get; set; }

        public string Iso { get; set; }

        public string PhoneCode { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

        public string Emoji { get; set; }

        public IEnumerable<State> states { get; set; }
    }
}
