using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWA.Core.DTO;
namespace SWA.Core.Service
{


    public interface ICancelPermitService
    {
        Task<bool> CancelWorkerPermitAsync(CancelWorkerPermitDto dto);
        Task<bool> CancelVolunteerPermitAsync(CancelVolunteerPermitDto dto);
    }

}
