using Microsoft.AspNetCore.Mvc;
using SWA.Core.DTO;
using SWA.Core.Service;
using SWA.Core.Services;
using System.Text.Json;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class PermitController : ControllerBase
{
    private readonly IPermitService _permitService;
    private readonly ICancelPermitService _cancelPermitService;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly INICPermitService _nicPermitService;
    public PermitController(IPermitService permitService, ICancelPermitService cancelPermitService, IConfiguration configuration, IHttpClientFactory httpClientFactory, INICPermitService nICPermitService)
    {
        _permitService = permitService;
        _cancelPermitService = cancelPermitService;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _nicPermitService = nICPermitService;
    }
    [HttpPost("get-nic-token")]
    public async Task<IActionResult> GetNicToken()
    {
        var payload = new
        {
            username = _configuration["NIC:Username"],
            password = _configuration["NIC:Password"],
            grant_type = "password"
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var client = new HttpClient();
        var authUrl = _configuration["NIC:AuthUrl"];

        try
        {
            var response = await client.PostAsync(authUrl, content);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, new
                {
                    Status = "Failed",
                    Message = "NIC Authentication failed",
                    Details = result
                });
            }

            using var doc = JsonDocument.Parse(result);
            return Ok(new
            {
                access_token = doc.RootElement.GetProperty("access_token").GetString(),
                token_type = doc.RootElement.GetProperty("token_type").GetString(),
                expires_in = doc.RootElement.GetProperty("expires_in").GetInt32(),
                issued_at = doc.RootElement.GetProperty("issued_at").GetString(),
                expires_on_date = doc.RootElement.GetProperty("expires_on_date").GetString()
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Status = "Error",
                Message = "Failed to connect to NIC Auth server",
                Exception = ex.Message
            });
        }
    }

    [HttpPost("create-worker-permit")]
    public async Task<IActionResult> CreateWorkerPermit([FromBody] NICWorkerPermitRequestDto dto)
    {
        var (result, statusCode, raw) = await _nicPermitService.CreateWorkerPermitAsync(dto);

        // TODO: Log the statusCode and raw response to DB

        if (result != null)
        {
            return Ok(new
            {
                message = "Permit created successfully",
                show_message = false,
                data = result
            });
        }

        return StatusCode(statusCode, new
        {
            message = "Failed to create worker permit",
            show_message = true,
            raw
        });
    }

    [HttpPost("create-volunteer-permit")]
    public async Task<IActionResult> CreateVolunteerPermit([FromBody] VolunteerPermitCreateDto dto)
    {
        try
        {
            long permitNumber = await _permitService.CreateVolunteerPermitAsync(dto);

            return Ok(new CreatePermitResponseDto
            {
                UnifiedPermitNumber = permitNumber,
                PermitHolderID = dto.PermitHolderID,
                Status = "Success",
                Message = "Volunteer permit created successfully"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new CreatePermitResponseDto
            {
                UnifiedPermitNumber = 0,
                PermitHolderID = dto.PermitHolderID,
                Status = "Failed",
                Message = ex.Message
            });
        }
    }

    [HttpGet("all-created-permits")]
    public async Task<IActionResult> GetAllCreatedPermits()
    {
        try
        {
            var permits = await _permitService.GetAllCreatedPermitsAsync();
            return Ok(permits);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Status = "Failed", Message = ex.Message });
        }
    }
    [HttpPost("cancel-worker-permit")]
    public async Task<IActionResult> CancelWorkerPermit([FromBody] CancelWorkerPermitDto dto)
    {
        try
        {
            bool success = await _cancelPermitService.CancelWorkerPermitAsync(dto);
            if (success)
            {
                return Ok(new { Status = "Success", Message = "Worker permit cancelled successfully" });
            }
            return BadRequest(new { Status = "Failed", Message = "Cancellation failed" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Status = "Error", Message = ex.Message });
        }
    }



    [HttpGet("client-ip")]
    public IActionResult GetClientIp()
    {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        return Ok(new { ip });
    }

    [HttpPost("cancel-volunteer-permit")]
    public async Task<IActionResult> CancelVolunteerPermit([FromBody] CancelVolunteerPermitDto dto)
    {
        try
        {
            bool success = await _cancelPermitService.CancelVolunteerPermitAsync(dto);
            if (success)
            {
                return Ok(new
                {
                    Status = "Success",
                    Message = "Volunteer permit cancelled successfully"
                });
            }

            // In case update did not affect any rows (wrong ID or already cancelled)
            return BadRequest(new
            {
                Status = "Failed",
                Message = "Cancellation failed"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Status = "Error",
                Message = ex.Message
            });
        }


      
    }
}
