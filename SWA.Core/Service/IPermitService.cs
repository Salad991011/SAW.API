using System.Threading.Tasks;
using SWA.Core.DTO;

namespace SWA.Core.Services
{
    public interface IPermitService
    {
        Task<long> CreateWorkerPermitAsync(WorkerPermitCreateDto dto);
        Task<long> CreateVolunteerPermitAsync(VolunteerPermitCreateDto dto);
        Task<IEnumerable<PermitListItemDto>> GetAllCreatedPermitsAsync();
    }
}
