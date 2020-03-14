using System;

namespace WikiScraper.Models
{
    public class Event
    {
        public Event() => Id = Guid.NewGuid();
        public Guid Id { get; set; }
        public string AstroId { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public string Date { get; set; }
        public string FriendlyDate { get; set; }
        public string Time { get; set; }
        public DateTime DateTime { get; set; }
        public string TimeAMPM { get; set; }
        public string Place { get; set; }
        public string BirthCountry { get; set; }
        public string PlaceAndCountryCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeZoneAbbreviation { get; set; }
        public string TimeType { get; set; }
        public string FriendlyTimeType { get; set; }
        public string Calendar { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string DataSourceCode { get; set; }
        public string FriendlyDataSource { get; set; }
        public string RoddenRatingCode { get; set; }
        public string FriendlyRoddenRating { get; set; }
        public string TimeAccuracyCode { get; set; }
        public string FriendlyTimeAccuracy { get; set; }
        public string Gender { get; set; }
        public string FriendlyGender { get; set; }
        public string Notes { get; set; }
        public string BirthName { get; set; }
        public string SunSign { get; set; }
        public string SunDegMin { get; set; }
        public string MoonSign { get; set; }
        public string MoonDegMin { get; set; }
        public string AscSign { get; set; }
        public string AscDegMin { get; set; }
    }
}