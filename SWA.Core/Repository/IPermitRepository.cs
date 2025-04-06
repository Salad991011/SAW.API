using SWA.Core.DTO;

public interface IPermitRepository
{
    Task<long> CreatePermitAsync(PermitCreateBaseDto dto);
    Task<long> CreateVolunteerPermitAsync(VolunteerPermitCreateDto dto);
    Task<IEnumerable<PermitListItemDto>> GetAllCreatedPermitsAsync();

}
