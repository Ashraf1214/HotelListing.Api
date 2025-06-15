using System;
using System.Collections.Generic;

namespace HotelListing.Api.Data.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public DateOnly? HireDate { get; set; }
}
