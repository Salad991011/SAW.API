using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.DTO
{
    public class CancelPermitResponseDto
    {
        public long UnifiedPermitNumber { get; set; }
        public string Status { get; set; } // e.g., "Success" or "Failed"
        public string Message { get; set; } // Optional message (localized)
        public DateTime? CanceledAt { get; set; } // Optional if available
    }
}
