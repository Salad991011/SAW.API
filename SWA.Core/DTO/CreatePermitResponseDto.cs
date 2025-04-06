using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.DTO
{
    public class CreatePermitResponseDto
    {
        public long UnifiedPermitNumber { get; set; }
        public long PermitHolderID { get; set; }
        public string Status { get; set; } // Could be "Success", "Failed", etc.
        public string? Message { get; set; } // Optional for error/success description
    }

}
