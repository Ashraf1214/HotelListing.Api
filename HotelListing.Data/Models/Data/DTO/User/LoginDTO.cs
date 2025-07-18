﻿using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.Data.DTO.User
{
    public class LoginDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
