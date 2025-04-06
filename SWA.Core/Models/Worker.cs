using System;
using System.Collections.Generic;

namespace SWA.Core.Models;

public partial class Worker
{
    public int WorkerId { get; set; }

    public long? PersonId { get; set; }

    public string? SapemployeeId { get; set; }

    public string? JobTitle { get; set; }

    public string? Department { get; set; }

    public string? Sector { get; set; }

    public string? SubSector { get; set; }

    public string? EmployeeType { get; set; }

    public string? Region { get; set; }

    public string? Branch { get; set; }

    public virtual Person? Person { get; set; }
}
