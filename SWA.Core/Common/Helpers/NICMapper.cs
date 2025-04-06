using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWA.Core.DTO;
namespace SWA.Core.Common.Helpers
{
    public static class NICMapper
    {
        public static NICWorkerPermitRequestDto ToNICDto(this WorkerPermitCreateDto dto)
        {
            return new NICWorkerPermitRequestDto
            {
                PermitHolderID = dto.PermitHolderID,
                BusinessID = dto.BusinessID,
                PermitIssueDateH = dto.PermitIssueDateH,
                PermitExpiryDateH = dto.PermitExpiryDateH,
                PermitLocationList = dto.PermitLocationList,
                HolderPhone = dto.HolderPhone,
                OperatorID = dto.OperatorID,
                ClientIPAddress = dto.ClientIPAddress,
                Lang = "AR"
            };
        }
    }
}
