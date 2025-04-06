using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.DTO
{
    public class ListHajjWorkersPermitsRequest
    {
        public int HajjYear { get; set; }
        public double? UnifiedPermitNumber { get; set; }
        public double? HolderID { get; set; }
        public double? BusinessID { get; set; }
        public double OperatorID { get; set; }
        public string ClientIPAddress { get; set; }
        public string Lang { get; set; } = "Ar";
    }
}
