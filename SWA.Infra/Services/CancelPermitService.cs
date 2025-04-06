using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SWA.Core.DTO;
using SWA.Core.Repository;
using SWA.Core.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Infra.Services
{
    public class CancelPermitService : ICancelPermitService
    {
        private readonly ICancelPermitRepository _repository;

        public CancelPermitService(ICancelPermitRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CancelWorkerPermitAsync(CancelWorkerPermitDto dto)
            => await _repository.CancelWorkerPermitAsync(dto);

        public async Task<bool> CancelVolunteerPermitAsync(CancelVolunteerPermitDto dto)
            => await _repository.CancelVolunteerPermitAsync(dto);
    }

}
