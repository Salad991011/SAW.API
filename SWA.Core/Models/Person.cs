using System;
using System.Collections.Generic;

namespace SWA.Core.Models;

public partial class Person
{
    public long PersonId { get; set; }

    public string? PermitHolderFirstName { get; set; }

    public string? PermitHolderFatherName { get; set; }

    public string? PermitHolderGrandfatherName { get; set; }

    public string? PermitHolderFamilyName { get; set; }

    public string? FirstNameT { get; set; }

    public string? FatherNameT { get; set; }

    public string? GrandfatherNameT { get; set; }

    public string? FamilyNameT { get; set; }

    public string? PermitHolderNationality { get; set; }

    public int? NationalityCode { get; set; }

    public string? NationalityDescAr { get; set; }

    public string? NationalityDescEn { get; set; }

    public int? SexCode { get; set; }

    public string? SexDescAr { get; set; }

    public string? SexDescEn { get; set; }

    public string? DocumentType { get; set; }

    public string? DocumentNumber { get; set; }

    public string? Gender { get; set; }

    public string? DateOfBirthH { get; set; }

    public DateOnly? DateOfBirthG { get; set; }

    public bool? IsVisitor { get; set; }

    public string? PersonStatus { get; set; }

    public virtual ICollection<Permit> Permits { get; set; } = new List<Permit>();

    public virtual Volunteer? Volunteer { get; set; }

    public virtual Worker? Worker { get; set; }
}
