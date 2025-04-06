using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.DTO
{
    public class ValidateCreateVolunteerPermitResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string ValidationCode { get; set; }
    }
}
