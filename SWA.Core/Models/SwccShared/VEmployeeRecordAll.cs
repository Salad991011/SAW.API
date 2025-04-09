using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.Models.SwccShared
{

    public class VEmployeeRecordAll
    {
        public string? UID { get; set; }
        public string? ClearUID { get; set; }
        public string? FirstNameAr { get; set; }
        public required string SecondNameAr { get; set; }
        public string? LastNameAr { get; set; }
        public string? FirstNameEn { get; set; }
        public required string SecondNameEn { get; set; }
        public required string LastNameEn { get; set; }
        public string? LocationCode { get; set; }
        public string? LocationNameAr { get; set; }
        public string? LocationNameEn { get; set; }
        public string? JobTitle { get; set; }
        public string? JobCode { get; set; }
        public string? JobTitleEn { get; set; }
        public string? JobNameAr { get; set; }
        public string? JobNameEn { get; set; }
        public string? JobRank { get; set; }
        public string? JobDegree { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public string? SectorNameAr { get; set; }
        public string? SectorNameEn { get; set; }
        public string? SectionCode { get; set; }
        public string? SectionNameAr { get; set; }
        public string? SectionNameEn { get; set; }
        public string? DivisionCode { get; set; }
        public string? DivisionNameAr { get; set; }
        public string? DivisionNameEn { get; set; }
        public string? DepartmentCode { get; set; }
        public string? DepartmentNameAr { get; set; }
        public string? DepartmentNameEn { get; set; }
        public Nullable<int> DegreeCode { get; set; }
        public string? DegreeNameAr { get; set; }
        public string? DegreeNameEn { get; set; }
        public Nullable<int> MajorCode { get; set; }
        public string? MajorNameAr { get; set; }
        public string? MajorNameEn { get; set; }
        public string? MaritalStatusCode { get; set; }
        public string? MaritalStatusNameAr { get; set; }
        public string? MaritalStatusNameEn { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? NationalId { get; set; }
        public string? ArabicNationality { get; set; }
        public string? NationalityCode { get; set; }
        public string? Mobile { get; set; }
        public string? Extention { get; set; }
        public string? ManagerUid { get; set; }
        public Nullable<int> ResignationReason { get; set; }
        public Nullable<System.DateTime> ResignationDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int IsOnTheJob { get; set; }
        public int IsSapUser { get; set; }
        public string? Email { get; set; }
    }
}
