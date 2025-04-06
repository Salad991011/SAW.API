using SWA.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.Service
{
    public interface INICPermitService
    {
        Task<(NICWorkerPermitResponseDto? result, int statusCode, string rawResponse)> CreateWorkerPermitAsync(NICWorkerPermitRequestDto dto);
    }

}
