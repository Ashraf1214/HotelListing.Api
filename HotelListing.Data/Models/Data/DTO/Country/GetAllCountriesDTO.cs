using HotelListing.Api.Data.DTO.Hotel;

namespace HotelListing.Api.Data.DTO.Country
{
    public class GetAllCountriesDTO : BaseCountryDTO
    {
        public List<GetHotelDTO> Hotels { get; set; }
    }
}
