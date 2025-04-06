using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.DTO
{
    public class ValidateCreateWorkerPermitRequest
    {
        public double PermitHolderID { get; set; }
        public double BusinessID { get; set; }
        public int PermitIssueDateH { get; set; }
        public int PermitExpiryDateH { get; set; }
        public List<int> PermitLocationList { get; set; }
        public double HolderPhone { get; set; }
        public double OperatorID { get; set; }
        public string ClientIPAddress { get; set; }
        public string Lang { get; set; } = "En";
    }
}
