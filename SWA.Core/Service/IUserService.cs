using SWA.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.Service
{
    public interface IUserService
    {
        public Task<UserInfoDto> GetUserInfo(string userId);
        public Task<List<UserInfoDto>> GetUsers(List<string> userIds);
        public Task<List<UserInfoDto>> SearchUsers(string keyword);
    }

}
