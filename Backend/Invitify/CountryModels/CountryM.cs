namespace Invitify.CountryModels
{
    public class CountryM
    {
        public string name { get; set; }

        public string iso3 { get; set; }

        public string iso2 { get; set; }

        public string numeric_code { get; set; }

        public string phone_code { get; set; }

        public string capital { get; set; }

        public string currency { get; set; }

        public string currency_name { get; set; }

        public string currency_symbol { get; set; }

        public string tld { get; set; }

        public string? native { get; set; }

        public string region { get; set; }

        public string subregion { get; set; }

        public List<TimeZonesM> timezones { get; set; }

        public TranslationsM translations { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }

        public string emoji { get; set; }

        public string emojiU { get; set; }

        public List<statem> states { get; set; }
    }
}
