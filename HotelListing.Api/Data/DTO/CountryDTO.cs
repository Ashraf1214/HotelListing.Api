namespace HotelListing.Api.Data.DTO
{
    public class CountryDTO
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public List<HotelDTO> Hotels { get; set; }
    }
}
