﻿namespace HotelListing.Api.Data.DTO
{
    public class HotelDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public double Rating { get; set; }

        public int CountryId { get; set; }
    }
}
