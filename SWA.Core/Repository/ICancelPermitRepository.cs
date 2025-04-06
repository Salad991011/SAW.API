using SWA.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.Repository
{
    public interface ICancelPermitRepository
    {
        Task<bool> CancelWorkerPermitAsync(CancelWorkerPermitDto dto);
        Task<bool> CancelVolunteerPermitAsync(CancelVolunteerPermitDto dto);
    }
}
