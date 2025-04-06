using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SWA.Core.DTO;
using System.Data;

public class PermitRepository : IPermitRepository
{
    private readonly IConfiguration _config;

    public PermitRepository(IConfiguration config)
    {
        _config = config;
    }

    public async Task<long> CreatePermitAsync(PermitCreateBaseDto dto)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("HajjDbConnection"));

        var parameters = new DynamicParameters();
        parameters.Add("@PermitHolderID", dto.PermitHolderID);
        parameters.Add("@BusinessID", dto.BusinessID);
        parameters.Add("@PermitIssueDateH", dto.PermitIssueDateH);
        parameters.Add("@PermitExpiryDateH", dto.PermitExpiryDateH);
        parameters.Add("@PermitIssueDateG", DateTime.Now);
        parameters.Add("@PermitExpiryDateG", DateTime.Now);
        parameters.Add("@RequestCreationDateH", dto.PermitIssueDateH);
        parameters.Add("@RequestReceivingDateH", dto.PermitIssueDateH);
        parameters.Add("@OperatorID", dto.OperatorID);
        parameters.Add("@HolderPhone", dto.HolderPhone);
        parameters.Add("@ClientIPAddress", dto.ClientIPAddress);
        parameters.Add("@Lang", dto.Lang);
        parameters.Add("@Status", "Approved");
        parameters.Add("@HajjYear", dto.HajjYear);
        parameters.Add("@LocationCodes", string.Join(",", dto.PermitLocationList));

        return await connection.ExecuteScalarAsync<long>(
            "CreatePermit", parameters, commandType: CommandType.StoredProcedure);
    }
    public async Task<long> CreateVolunteerPermitAsync(VolunteerPermitCreateDto dto)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("HajjDbConnection"));
        var parameters = new DynamicParameters();

        // Shared fields
        parameters.Add("@PermitHolderID", dto.PermitHolderID);
        parameters.Add("@BusinessID", dto.BusinessID);
        parameters.Add("@PermitIssueDateH", dto.PermitIssueDateH);
        parameters.Add("@PermitExpiryDateH", dto.PermitExpiryDateH);
        parameters.Add("@PermitIssueDateG", DateTime.Now);
        parameters.Add("@PermitExpiryDateG", DateTime.Now);
        parameters.Add("@RequestCreationDateH", dto.PermitIssueDateH);
        parameters.Add("@RequestReceivingDateH", dto.PermitIssueDateH);
        parameters.Add("@OperatorID", dto.OperatorID);
        parameters.Add("@HolderPhone", dto.HolderPhone);
        parameters.Add("@ClientIPAddress", dto.ClientIPAddress);
        parameters.Add("@Lang", dto.Lang);
        parameters.Add("@Status", "Approved");
        parameters.Add("@HajjYear", dto.HajjYear);
        parameters.Add("@LocationCodes", string.Join(",", dto.PermitLocationList));

        // Volunteer-specific fields
        //parameters.Add("@VolunteerCategory", dto.VolunteerCategory);
        //parameters.Add("@EducationLevel", dto.EducationLevel);
        //parameters.Add("@Specialization", dto.Specialization);
        //parameters.Add("@Region", dto.Region);
        //parameters.Add("@District", dto.District);
        //parameters.Add("@PreferredLocation", dto.PreferredLocation);
        //parameters.Add("@PersonStatus", dto.PersonStatus);

        return await connection.ExecuteScalarAsync<long>("CreateVolunteerPermit", parameters, commandType: CommandType.StoredProcedure);
    }
    public async Task<IEnumerable<PermitListItemDto>> GetAllCreatedPermitsAsync()
    {
        using var connection = new SqlConnection(_config.GetConnectionString("HajjDbConnection"));

        var permits = await connection.QueryAsync<PermitListItemDto>(
            "GetAllCreatedPermits",
            commandType: CommandType.StoredProcedure
        );

        return permits;
    }
}
