using Microsoft.EntityFrameworkCore;
using SWA.Core.DTO;
using SWA.Core.Models.SwccShared;
using SWA.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Infra.Repository
{

    public class UserRepository(ISWCCSharedDbContext dbContext) : IUserRepository
    {
        public async Task<UserInfoDto> GetUserInfo(string userId)
        {
            var records = await dbContext.V_EmployeeRecord_All.Where(s => s.UID == userId && s.LocationCode != null).ToListAsync();
            if (records != null && records.Count > 0)
            {
                UserInfoDto record = records.First();
                return record;
            }
            return null;
        }

        public async Task<List<UserInfoDto>> GetUsers(List<string> userIds)
        {
            var records = await dbContext.V_EmployeeRecord_All.ToListAsync();
            if (records != null && records.Count > 0)
            {
                records = records.Where(s => userIds.Contains(s.UID) && s.LocationCode != null).ToList();
                List<UserInfoDto> result = new List<UserInfoDto>();
                foreach (var record in records)
                {
                    UserInfoDto sapUser = record;
                    result.Add(sapUser);
                }
                return result;
            }
            return null;
        }

        public async Task<List<UserInfoDto>> SearchUsers(string keyword)
        {
            var records = await dbContext.V_EmployeeRecord_All.ToListAsync();
            if (records != null && records.Count > 0)
            {
                records = records.Where(s => s.UID.Contains(keyword) && s.LocationCode != null).Take(20).ToList();
                var usersIds = records.Select(s => s.UID).ToList();
                List<UserInfoDto> result = new List<UserInfoDto>();
                foreach (var record in records)
                {
                    UserInfoDto sapUser = record;
                    result.Add(sapUser);
                }
                return result;
            }
            return null;
        }
    }
}
