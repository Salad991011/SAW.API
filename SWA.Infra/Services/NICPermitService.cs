using Microsoft.Extensions.Configuration;
using SWA.Core.DTO;
using SWA.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
namespace SWA.Infra.Services
{
    public class NICPermitService : INICPermitService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public NICPermitService(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        public async Task<(NICWorkerPermitResponseDto?, int, string)> CreateWorkerPermitAsync(NICWorkerPermitRequestDto dto)
        {
            var tokenPayload = new
            {
                username = _config["NIC:Username"],
                password = _config["NIC:Password"],
                grant_type = "password"
            };

            var tokenClient = _clientFactory.CreateClient();
            var tokenRequest = new StringContent(JsonSerializer.Serialize(tokenPayload), Encoding.UTF8, "application/json");
            var tokenResp = await tokenClient.PostAsync(_config["NIC:AuthUrl"], tokenRequest);

            if (!tokenResp.IsSuccessStatusCode)
                return (null, (int)tokenResp.StatusCode, await tokenResp.Content.ReadAsStringAsync());

            var accessToken = JsonDocument.Parse(await tokenResp.Content.ReadAsStringAsync())
                                          .RootElement.GetProperty("access_token").GetString();

            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            var apiClient = _clientFactory.CreateClient();
            apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var nicResp = await apiClient.PostAsync(_config["NIC:WorkerCreateUrl"], content);
            var raw = await nicResp.Content.ReadAsStringAsync();

            if (!nicResp.IsSuccessStatusCode) return (null, (int)nicResp.StatusCode, raw);

            var json = JsonDocument.Parse(raw);
            var res = new NICWorkerPermitResponseDto
            {
                UnifiedPermitNumber = json.RootElement.GetProperty("UnifiedPermitNumber").GetDouble(),
                RequestTimestamp = json.RootElement.GetProperty("RequestTimestamp").GetString(),
                ResponseTimestamp = json.RootElement.GetProperty("ResponseTimestamp").GetString()
            };

            return (res, (int)nicResp.StatusCode, raw);
        }
    }

}
