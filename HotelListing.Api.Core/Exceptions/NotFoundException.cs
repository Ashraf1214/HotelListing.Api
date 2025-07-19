namespace HotelListing.Api.Exceptions
{
    public class NotFoundException : Exception
    {
        // Entity name would be the resourece, e.g., "Hotel", "Country", etc.
        // EntityId would be the unique identifier of the entity, e.g., hotelId, countryId, etc.
        // Parent constructor ,i.e., Exception, is called with a formatted message by using 'base' keyword.
        // Exception constructor accepts a string message that describes the error.
        public NotFoundException(string entityName, object entityId) : base($"{entityName} ({entityId}) was not found.")
        {
                
        }
    }
}
