using SWA.Core.Models.SwccShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.DTO
{

    public class UserInfoDto
    {
        public string? EmployeeNameAr { get; set; }
        public string? EmployeeNameEn { get; set; }
        public string? UID { get; set; }
        public string? Extention { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? LocationCode { get; set; }
        public string? LocationName { get; set; }
        public string? Division { get; set; }
        public string? Department { get; set; }
        public string? Gender { get; set; }
        public bool IsSapUser { get; set; }
        public bool IsMan { get; set; } = true;
        public string Photo { get; set; }



        public static implicit operator UserInfoDto(VEmployeeRecordAll request)
        {
            if (request != null)
            {

                string employeeNameAr = string.Empty;
                if (!string.IsNullOrWhiteSpace(request.FirstNameAr))
                    employeeNameAr = request.FirstNameAr;
                else if (!string.IsNullOrWhiteSpace(request.FirstNameEn))
                    employeeNameAr = request.FirstNameEn;

                if (!string.IsNullOrWhiteSpace(request.LastNameAr))
                    employeeNameAr += " " + request.LastNameAr;
                else if (!string.IsNullOrWhiteSpace(request.LastNameEn))
                    employeeNameAr += " " + request.LastNameEn;

                if (string.IsNullOrEmpty(employeeNameAr))
                    employeeNameAr = request.UID;


                string employeeNameEn = string.Empty;
                if (!string.IsNullOrWhiteSpace(request.FirstNameEn))
                    employeeNameEn = request.FirstNameEn;

                if (!string.IsNullOrWhiteSpace(request.LastNameEn))
                    employeeNameEn += " " + request.LastNameEn;

                if (string.IsNullOrEmpty(employeeNameEn))
                    employeeNameEn = request.UID;


                string? jobRank = new string(request?.JobRank?.Where(c => char.IsDigit(c)).ToArray());
                int rank = 0;
                if (request?.JobRank != null && request.JobRank.ToLower().StartsWith('g'))
                    int.TryParse(jobRank, out rank);

                return new()
                {
                    EmployeeNameEn = employeeNameEn,
                    EmployeeNameAr = employeeNameAr,
                    UID = request.UID,
                    Extention = request.Extention,
                    Mobile = request.Mobile,
                    LocationCode = request.LocationCode,
                    Email = request.Email,
                    IsSapUser = request.IsSapUser == 1,
                    IsMan = request.Gender != "F",
                    Department = request.DepartmentNameAr,
                    Division = request.DivisionNameAr,
                    LocationName = request.LocationNameAr,
                    Gender = request.Gender,
                    Photo = $"https://apiext.swcc.gov.sa/MobileSharedApis/Photo/getimage?uid={request.UID}"
                };
            }
            return null;
        }

    }
}
