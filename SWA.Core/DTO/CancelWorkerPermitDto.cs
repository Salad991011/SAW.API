﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.DTO
{
    public class CancelWorkerPermitDto
    {
        public long UnifiedPermitNumber { get; set; }
        public long PermitHolderID { get; set; }
        public long OperatorID { get; set; }
        public string ClientIPAddress { get; set; }
        public string Lang { get; set; } = "AR";
    }
}
