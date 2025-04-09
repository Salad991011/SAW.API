











using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.Models.SwccShared
{
    public class VEmployeeRecordAllConfiguration : IEntityTypeConfiguration<VEmployeeRecordAll>
    {
        public void Configure(EntityTypeBuilder<VEmployeeRecordAll> builder)
        {
            builder.HasNoKey();
            builder.ToView("VEmployeeRecordAll", "dbo");
        }
    }

}
