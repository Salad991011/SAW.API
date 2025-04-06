using System;
using System.Collections.Generic;

namespace SWA.Core.Models;

public partial class Log
{
    public int LogId { get; set; }

    public long UserId { get; set; }

    public string Action { get; set; } = null!;

    public string? EntityName { get; set; }

    public long? EntityId { get; set; }

    public string? Description { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<ApiErrorLog> ApiErrorLogs { get; set; } = new List<ApiErrorLog>();

    public virtual User User { get; set; } = null!;
}
