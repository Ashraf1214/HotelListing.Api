namespace HotelListing.Api.Data.DTO.Hotel
{
    public class UpdateHotelDTO : BaseHotelDTO
    {
        // No additional properties needed for update, as it uses the same properties as BaseHotelDTO
        // Id is also not required since the id will be provided in the URL when making the PUT request
    }
}
