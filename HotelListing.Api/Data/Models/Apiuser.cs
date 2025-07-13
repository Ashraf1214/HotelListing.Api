using Microsoft.AspNetCore.Identity;

namespace HotelListing.Api.Data.Models
{
    //Extending and adding two more properties to the IdentityUser class
    public class Apiuser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
