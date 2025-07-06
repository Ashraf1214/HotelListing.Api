using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.Data.DTO.Hotel
{
    public abstract class BaseHotelDTO
    {
        //public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public double? Rating { get; set; }
        //[Range(1,int.MaxValue)]
        //public int CountryId { get; set; }
    }
}
