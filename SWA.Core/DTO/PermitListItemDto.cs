
using System;
// getAllUsers Permit
namespace SWA.Core.DTO
{
    public class PermitListItemDto
    {
        // Permit Info
        public long UnifiedPermitNumber { get; set; }
        public string PermitHolderID { get; set; }           // Person.DocumentNumber
        public int PermitIssueDateH { get; set; }
        public int PermitExpiryDateH { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? RespondedAt { get; set; }
        public string Status { get; set; }                   // Permit.Status
        public string PermitType { get; set; }               // Person.PersonStatus ("Worker" / "Volunteer")
        public long OperatorID { get; set; }
        // Person Info
        public long PersonID { get; set; }
        public string PermitHolderFirstName { get; set; }
        public string PermitHolderFatherName { get; set; }
        public string PermitHolderGrandfatherName { get; set; }
        public string PermitHolderFamilyName { get; set; }
        public string FirstNameT { get; set; }
        public string FatherNameT { get; set; }
        public string GrandfatherNameT { get; set; }
        public string FamilyNameT { get; set; }
        public string PermitHolderNationality { get; set; }
        public int? NationalityCode { get; set; }
        public string NationalityDescAr { get; set; }
        public string NationalityDescEn { get; set; }
        public int? SexCode { get; set; }
        public string SexDescAr { get; set; }
        public string SexDescEn { get; set; }
        public string DocumentType { get; set; }
        public string Gender { get; set; }
        public string DateOfBirthH { get; set; }
        public DateTime? DateOfBirthG { get; set; }
        public bool? IsVisitor { get; set; }
    }
}

