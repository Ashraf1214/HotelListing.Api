using System;
using System.Collections.Generic;

namespace HotelListing.Api.Data.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string? Name { get; set; }

    public string? ShortName { get; set; }

    public virtual ICollection<Hotel> Hotels { get; set; }
}
