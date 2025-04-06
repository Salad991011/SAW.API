using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.Common
{
    public interface IDbContext
    {
        string ConnectionString { get; }
    }
}

