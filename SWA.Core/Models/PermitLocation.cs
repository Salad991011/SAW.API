using System;
using System.Collections.Generic;

namespace SWA.Core.Models;

public partial class PermitLocation
{
    public int Id { get; set; }

    public long? UnifiedPermitNumber { get; set; }

    public int? LocationCode { get; set; }

    public string? LocationDescAr { get; set; }

    public string? LocationDescEn { get; set; }

    public virtual Permit? UnifiedPermitNumberNavigation { get; set; }
}
