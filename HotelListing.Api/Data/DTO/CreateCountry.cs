using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.Data.DTO
{
    public class CreateCountry
    {
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
