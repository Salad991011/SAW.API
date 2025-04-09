using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.Models.SwccShared
{
    public interface ISWCCSharedDbContext
    {
        public DbSet<VEmployeeRecordAll> V_EmployeeRecord_All { get; set; }

    }

}
