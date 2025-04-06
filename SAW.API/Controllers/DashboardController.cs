
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SWA.Core.Repository;
using SWA.Core.Common.Helpers;

namespace SAW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IPermitRepository _permitRepository;

        public DashboardController(IPermitRepository permitRepository)
        {
            _permitRepository = permitRepository;
        }

        [HttpGet("data")]
        public async Task<IActionResult> GetDashboardData()
        {
            var permits = await _permitRepository.GetAllCreatedPermitsAsync();
            var approvedPermits = permits
                .Where(p => p.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase))
                .ToList();

            var barData = approvedPermits.GroupBy(p =>
            {
                int hijriInt = p.PermitIssueDateH;
                if (hijriInt == 0)
                    return DateTime.MinValue; // fallback if invalid

                var hijriStr = hijriInt.ToString("00000000");
                string formattedHijri = $"{hijriStr.Substring(0, 4)}/{hijriStr.Substring(4, 2)}/{hijriStr.Substring(6, 2)}";

                return HijriDateConverter.ToGregorian(formattedHijri).Date;
            })
            .Where(g => g.Key != DateTime.MinValue)
            .Select(g => new BarDataItemDto
            {
                Date = g.Key.ToString("d/M/yyyy"),
                Worker = g.Count(p => p.PermitType == "Worker"),
                Volunteer = g.Count(p => p.PermitType == "Volunteer"),
            })
            .ToList();


            var workerCount = approvedPermits.Count(p => p.PermitType == "Worker");
            var volunteerCount = approvedPermits.Count(p => p.PermitType == "Volunteer");

            var pieData = new List<PieDataItemDto>
    {
        new PieDataItemDto { Name = "تصريح عامل", Value = workerCount },
        new PieDataItemDto { Name = "تصريح متطوع", Value = volunteerCount },
    };

            var sortedBarData = barData.OrderBy(x =>
                DateTime.ParseExact(x.Date, "d/M/yyyy", CultureInfo.InvariantCulture)
            ).ToList();

            var lineData = sortedBarData.Select((x, index) => new LineDataItemDto
            {
                Day = index + 1,
                Permits = x.Worker + x.Volunteer
            }).ToList();

            var unifiedPermitCount = approvedPermits.Count;

            var dashboardData = new DashboardDataDto
            {
                BarData = barData,
                PieData = pieData,
                LineData = lineData,
                WorkerPermitCount = workerCount,
                VolunteerPermitCount = volunteerCount,
                UnifiedPermitCount = unifiedPermitCount
            };

            return Ok(dashboardData);
        }


        // Helper method: Converts Arabic numeral characters to Latin digits.
        private string ConvertArabicDigitsToLatin(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var arabicToLatin = new Dictionary<char, char>
            {
                { '٠', '0' },
                { '١', '1' },
                { '٢', '2' },
                { '٣', '3' },
                { '٤', '4' },
                { '٥', '5' },
                { '٦', '6' },
                { '٧', '7' },
                { '٨', '8' },
                { '٩', '9' }
            };

            return new string(input.Select(ch => arabicToLatin.ContainsKey(ch) ? arabicToLatin[ch] : ch).ToArray());
        }
    }

    // DTO definitions – adjust these as needed for your domain.
    public class DashboardDataDto
    {
        public List<BarDataItemDto> BarData { get; set; }
        public List<PieDataItemDto> PieData { get; set; }
        public List<LineDataItemDto> LineData { get; set; }
        public int WorkerPermitCount { get; set; }
        public int VolunteerPermitCount { get; set; }
        public int UnifiedPermitCount { get; set; }
    }

    public class BarDataItemDto
    {
        public string Date { get; set; }
        public int Worker { get; set; }
        public int Volunteer { get; set; }
    }

    public class PieDataItemDto
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class LineDataItemDto
    {
        public int Day { get; set; }
        public int Permits { get; set; }
    }
}
