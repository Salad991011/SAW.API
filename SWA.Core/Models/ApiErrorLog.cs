using System;
using System.Collections.Generic;

namespace SWA.Core.Models;

public partial class ApiErrorLog
{
    public int ApiLogId { get; set; }

    public int? LogId { get; set; }

    public string? ApiName { get; set; }

    public int? HttpStatusCode { get; set; }

    public int? NicStatusCode { get; set; }

    public string? ErrorMessage { get; set; }

    public string? ErrorMessageAr { get; set; }

    public string? RequestPayload { get; set; }

    public string? ResponsePayload { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Log? Log { get; set; }
}
