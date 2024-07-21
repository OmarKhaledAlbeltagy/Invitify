namespace Invitify.Entities
{
    public class State
    {
        public int Id { get; set; }

        public string StateName { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

        public int CountryId { get; set; }

        public Country country { get; set; }

        public IEnumerable<Contact> contact { get; set; }
    }
}
