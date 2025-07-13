using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.Data.DTO.User
{
    public class ApiUserDTO : LoginDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public char RoleLevel { get; set; }

    }
}
