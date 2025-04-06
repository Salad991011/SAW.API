using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.DTO
{
    public class ValidateCancelHajjVolunteersPermitRequest
    {
        public double UnifiedPermitNumber { get; set; }
        public string CancelReason { get; set; }
        public double OperatorID { get; set; }
        public string ClientIPAddress { get; set; }
        public string Lang { get; set; } = "Ar";
    }
}
