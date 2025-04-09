using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.Models.SwccShared
{

    public class SWCCSharedDbContext : DbContext, ISWCCSharedDbContext
    {
        public SWCCSharedDbContext(DbContextOptions<SWCCSharedDbContext> options)
              : base(options)
        {
        }

        public virtual DbSet<VEmployeeRecordAll> V_EmployeeRecord_All { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SWCCSharedDbContext).Assembly);
        }
    }

}