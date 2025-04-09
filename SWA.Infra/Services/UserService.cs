using SWA.Core.DTO;
using SWA.Core.Repository;
using SWA.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Infra.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<UserInfoDto> GetUserInfo(string userId)
        {
            var user = await userRepository.GetUserInfo(userId);
            return user;
        }


        public async Task<List<UserInfoDto>> GetUsers(List<string> userIds)
        {
            var users = await userRepository.GetUsers(userIds);
            return users;
        }


        public async Task<List<UserInfoDto>> SearchUsers(string keyword)
        {
            var users = await userRepository.SearchUsers(keyword);
            return users;
        }
    }
}
