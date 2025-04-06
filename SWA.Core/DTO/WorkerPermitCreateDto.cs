using System;
using System.Collections.Generic;

namespace SWA.Core.DTO
{
  
        public class WorkerPermitCreateDto : PermitCreateBaseDto
        {
        //    public string SAPEmployeeID { get; set; }
        //    public string JobTitle { get; set; }
        //    public string Department { get; set; }
        //    public string Sector { get; set; }
        //    public string SubSector { get; set; }
        //    public string EmployeeType { get; set; }
        //    public string Region { get; set; }
        //    public string Branch { get; set; }

            public string PersonStatus { get; set; } = "Worker";
        
    }
}