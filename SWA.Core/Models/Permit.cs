using System;
using System.Collections.Generic;

namespace SWA.Core.Models;

public partial class Permit
{
    public long UnifiedPermitNumber { get; set; }

    public long? PermitHolderId { get; set; }

    public long? BusinessId { get; set; }

    public int? PermitIssueDateH { get; set; }

    public int? PermitExpiryDateH { get; set; }

    public DateTime? PermitIssueDateG { get; set; }

    public DateTime? PermitExpiryDateG { get; set; }

    public string? RequestCreationDateH { get; set; }

    public string? RequestReceivingDateH { get; set; }

    public DateTime? RequestTimestamp { get; set; }

    public DateTime? ResponseTimestamp { get; set; }

    public long? OperatorId { get; set; }

    public long? HolderPhone { get; set; }

    public string? ClientIpaddress { get; set; }

    public string? CancelReason { get; set; }

    public string? Lang { get; set; }

    public string? Status { get; set; }

    public int? HajjYear { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Business? Business { get; set; }

    public virtual User? Operator { get; set; }

    public virtual Person? PermitHolder { get; set; }

    public virtual ICollection<PermitLocation> PermitLocations { get; set; } = new List<PermitLocation>();
}
