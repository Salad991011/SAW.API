using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.DTO
{
    public class ListHajjWorkersPermitsResponse
    {
        public string UnifiedPermitNumber { get; set; }
        public string PermitStatus { get; set; }
        public string PermitHolderName { get; set; }
        public int PermitIssueDateH { get; set; }
        public int PermitExpiryDateH { get; set; }
        // Add more fields as needed from NIC spec
    }

}
