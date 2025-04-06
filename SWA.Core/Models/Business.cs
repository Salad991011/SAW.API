using System;
using System.Collections.Generic;

namespace SWA.Core.Models;

public partial class Business
{
    public long BusinessId { get; set; }

    public string? BusinessName { get; set; }

    public string? BusinessType { get; set; }

    public string? ContactInfo { get; set; }

    public string? Address { get; set; }

    public long? OwnerId { get; set; }

    public virtual ICollection<Permit> Permits { get; set; } = new List<Permit>();
}
