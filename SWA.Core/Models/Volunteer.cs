using System;
using System.Collections.Generic;

namespace SWA.Core.Models;

public partial class Volunteer
{
    public int VolunteerId { get; set; }

    public long? PersonId { get; set; }

    public string? VolunteerCategory { get; set; }

    public string? EducationLevel { get; set; }

    public string? Specialization { get; set; }

    public string? Region { get; set; }

    public string? District { get; set; }

    public string? PreferredLocation { get; set; }

    public virtual Person? Person { get; set; }
}
