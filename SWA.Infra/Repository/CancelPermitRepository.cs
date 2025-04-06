using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SWA.Core.DTO;
using SWA.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Infra.Repository
{
    public class CancelPermitRepository : ICancelPermitRepository
    {
        private readonly IConfiguration _config;

        public CancelPermitRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> CancelWorkerPermitAsync(CancelWorkerPermitDto dto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("HajjDbConnection"));

            var parameters = new DynamicParameters();
            parameters.Add("@UnifiedPermitNumber", dto.UnifiedPermitNumber);
            parameters.Add("@PermitHolderID", dto.PermitHolderID);
            parameters.Add("@OperatorID", dto.OperatorID);
            parameters.Add("@ClientIPAddress", dto.ClientIPAddress);
            parameters.Add("@Lang", dto.Lang);

            Console.WriteLine($"[DEBUG] Cancelling Worker Permit - UnifiedPermitNumber: {dto.UnifiedPermitNumber}, PermitHolderID: {dto.PermitHolderID}");

            try
            {
                int result = await connection.QuerySingleAsync<int>(
                    "DoCancelHajjWorkersPermits",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                Console.WriteLine($"[DEBUG] Stored procedure returned: {result}");
                return result == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] CancelWorkerPermitAsync Exception: {ex.Message}");
                throw;
            }
        }



        public async Task<bool> CancelVolunteerPermitAsync(CancelVolunteerPermitDto dto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("HajjDbConnection"));

            var parameters = new DynamicParameters();
            parameters.Add("@UnifiedPermitNumber", dto.UnifiedPermitNumber);
            parameters.Add("@PermitHolderID", dto.PermitHolderID);
            parameters.Add("@CancelReason", dto.CancelReason);
            parameters.Add("@OperatorID", dto.OperatorID);
            parameters.Add("@ClientIPAddress", dto.ClientIPAddress);
            parameters.Add("@Lang", dto.Lang);

            var rows = await connection.ExecuteAsync("DoCancelHajjVolunteersPermits", parameters, commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

    }

}
