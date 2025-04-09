using SWA.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.Repository
{
    public interface IUserRepository
    {
        Task<UserInfoDto> GetUserInfo(string userId);
        Task<List<UserInfoDto>> GetUsers(List<string> userIds);
        Task<List<UserInfoDto>> SearchUsers(string keyword);
    }

}
