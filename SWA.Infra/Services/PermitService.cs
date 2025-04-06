using SWA.Core.Common.Helpers;
using SWA.Core.DTO;
using SWA.Core.Services;
using SWA.Core.Common.Helpers;
using SWA.Core.Service;
public class PermitService : IPermitService
{
    private readonly IPermitRepository _repository;
    private readonly INICPermitService _nicPermitService;

    public PermitService(IPermitRepository repository, INICPermitService nicPermitService)
    {
        _repository = repository;
        _nicPermitService = nicPermitService;
    }

    public async Task<long> CreateWorkerPermitAsync(WorkerPermitCreateDto dto)
    {
        // ? Patch defaults
       

        // ? Map to NIC DTO
        var nicDto = dto.ToNICDto(); // Make sure `using SWA.Core.Helpers;` is present

        // ? Send to NIC API
        var (response, statusCode, rawJson) = await _nicPermitService.CreateWorkerPermitAsync(nicDto);

        // ? Handle failure
        if (response == null)
            throw new Exception($"NIC API Error (Code {statusCode}): {rawJson}");

        // ? Return UnifiedPermitNumber as long
        return (long)response.UnifiedPermitNumber;
    }
    public Task<long> CreateVolunteerPermitAsync(VolunteerPermitCreateDto dto)
    {
        // Ensure default values for missing fields specific to Volunteer
       
        return _repository.CreatePermitAsync(dto);
    }
    public Task<IEnumerable<PermitListItemDto>> GetAllCreatedPermitsAsync()
    {
        return _repository.GetAllCreatedPermitsAsync();
    }
};
