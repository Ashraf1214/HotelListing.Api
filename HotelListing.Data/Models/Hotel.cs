﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Api.Data.Models;

public partial class Hotel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public double? Rating { get; set; }

    [ForeignKey(nameof(CountryId))]
    public int CountryId { get; set; }

    public virtual Country Country { get; set; }
}
