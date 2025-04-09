using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.Models
{
    public class AppSettings
    {
        public string ApiKey { get; set; }
        public string JwtKey { get; set; }
        public string Issuer { get; set; }
        public string UserMgtUrl { get; set; }
        public string UserMgtTenantId { get; set; }
        public string UserMgtSecret { get; set; }
        public string FrontUrl { get; set; }
    }
}
