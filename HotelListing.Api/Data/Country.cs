namespace HotelListing.Api.Data
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public virtual IList<Hotel> Hotels { get; set; } = new List<Hotel>();
        //Can also be written as
        //public virtual IList<Hotel> Hotels { get; set; } = [];
    }
}
