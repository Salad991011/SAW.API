using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.Core.DTO
{
    public class VolunteerPermitCreateDto : PermitCreateBaseDto
    {
        //public string VolunteerCategory { get; set; }
        //public string EducationLevel { get; set; }
        //public string Specialization { get; set; }
        //public string Region { get; set; }
        //public string District { get; set; }
        //public string PreferredLocation { get; set; }

        public string PersonStatus { get; set; } = "Volunteer";
    }
}
