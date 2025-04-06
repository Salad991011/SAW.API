using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.DTO
{
    public class PermitCreateBaseDto
    {
        public long PermitHolderID { get; set; }
        public long BusinessID { get; set; }
        public int PermitIssueDateH { get; set; }
        public int PermitExpiryDateH { get; set; }
        public List<int> PermitLocationList { get; set; }
        public long HolderPhone { get; set; }
        public long OperatorID { get; set; }
        public string ClientIPAddress { get; set; }
        public string Lang { get; set; } = "Ar";
        public int HajjYear { get; set; } = 1445;

        // Optional shared person fields for internal testing
     
    }
}
