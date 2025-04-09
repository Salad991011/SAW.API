using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using SWA.Core.Common.Enum;
using SWA.Core.Models;
using SWA.Core.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;


namespace SAW.API.Controllers;


[Route("api/[controller]")]
[ApiController]

public class AccountController : ControllerBase
{
    private readonly AppSettings _settings;
    private readonly IUserService _userService;
    private readonly JWTModel _options;
    private readonly string site = "https://api.swcc.gov.sa";




    public AccountController(IOptions<AppSettings> settings, IOptions<JWTModel> options, IUserService userService)
    {
        _settings = settings.Value;
        _options = options.Value;
        _userService = userService;
    }

    [NonAction]
    public TokenValidationParameters TokenValidationParameters()
    {
        var res = new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = site, //some string, normally web url,
            ValidAudience = site,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.MasterKey))
        };
        return res;
    }

    [HttpGet("GetToken")]
    [AllowAnonymous]
    public async Task<IActionResult> GetToken()
    {
        string uid, fullName;
        uid = fullName = null;
        string EncKey = _options.EncKey;
        var tokenValidationParameters = TokenValidationParameters();
        string jwtKey = _settings.JwtKey;
        var issuer = _settings.FrontUrl;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        string authString = Request.HttpContext.Request.Headers["Authorization"];

        if (string.IsNullOrWhiteSpace(authString))
            return new UnauthorizedObjectResult("Unauthorized Access");

        authString = authString.Replace("Bearer ", "");

        var claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(authString, tokenValidationParameters, out var token);
        string uidEnc = "";
        if (token is JwtSecurityToken jwtSecurityToken)
        {
            fullName = jwtSecurityToken.Claims.First(claim => claim.Type == "fullName").Value;
            uidEnc = jwtSecurityToken.Claims.First(claim => claim.Type == "uid").Value;

            string U_name = fullName;
            uid = fullName;

        }


        string jsonData = string.Empty;
        var user = Regex.Replace(uid, @"swcc\\u", "");
        List<RoleEnum> rolesList = new List<RoleEnum>();


        using (HttpClient httpClient = new HttpClient())
        {
            string uri = _settings.UserMgtUrl + $"/api/user/{user}";
            string permissionToken = "Basic " + System.Convert.ToBase64String(Encoding.UTF8.GetBytes(_settings.UserMgtTenantId + ":" + _settings.UserMgtSecret));
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", permissionToken);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            HttpResponseMessage Res = httpClient.GetAsync(uri).Result;
            if (Res.IsSuccessStatusCode)
            {
                string result = Res.Content.ReadAsStringAsync().Result;
                if (result != "")
                {
                    JObject obj = JObject.Parse(result);
                    if (obj["UserRoles"].HasValues)
                    {
                        var usersRoles = JArray.Parse(obj["UserRoles"].ToString());
                        foreach (var userRole in usersRoles)
                        {
                            var role = JObject.Parse(userRole["role"].ToString());
                            RoleEnum roleEnum = (RoleEnum)((int)role["Id"]);
                            string roleName = role["EnglishName"]?.ToString() ?? "";
                            rolesList.Add(roleEnum);

                        }
                    }
                }
            }

            var userData = await _userService.GetUserInfo(user);
            //Create a List of Claims, Keep claims name short
            var permClaims = new List<Claim>();
            if (userData != null)
            {

                permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                permClaims.Add(new Claim(ClaimTypes.NameIdentifier, user));
                permClaims.Add(new Claim("EmployeeNameAr", !string.IsNullOrWhiteSpace(userData.EmployeeNameAr) ? userData.EmployeeNameAr : ""));
                permClaims.Add(new Claim("EmployeeNameEn", !string.IsNullOrWhiteSpace(userData.EmployeeNameEn) ? userData.EmployeeNameEn : ""));
                permClaims.Add(new Claim("IsSapUser", userData.IsSapUser.ToString()));
                permClaims.Add(new Claim("IsMan", userData.IsMan.ToString()));
                permClaims.Add(new Claim("Mobile", !string.IsNullOrWhiteSpace(userData.Mobile) ? userData.Mobile : ""));
                permClaims.Add(new Claim("UID", userData.UID.ToString()));
                permClaims.Add(new Claim("Extention", !string.IsNullOrWhiteSpace(userData.Extention) ? userData.Extention : ""));
                permClaims.Add(new Claim("Email", !string.IsNullOrWhiteSpace(userData.Email) ? userData.Email : ""));
                permClaims.Add(new Claim("LocationName", !string.IsNullOrWhiteSpace(userData.LocationName) ? userData.LocationName : ""));
                permClaims.Add(new Claim("LocationCode", !string.IsNullOrWhiteSpace(userData.LocationCode) ? userData.LocationCode : ""));
                permClaims.Add(new Claim("Division", !string.IsNullOrWhiteSpace(userData.Division) ? userData.Division : ""));
                permClaims.Add(new Claim("Department", !string.IsNullOrWhiteSpace(userData.Department) ? userData.Department : ""));
                permClaims.Add(new Claim("Gender", userData.IsMan ? "M" : "F"));

                if (rolesList.Count > 0)
                    permClaims.Add(new Claim("Roles", string.Join(',', rolesList)));

            }
            else
            {
                permClaims.Add(new Claim("IsSapUser", false.ToString()));
            }

            //Create Security Token object by giving required parameters
            var authToken = new JwtSecurityToken(issuer, //Issure
                            issuer,  //Audience
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(authToken);
            return Ok(jwt_token);
        }
    }




}
