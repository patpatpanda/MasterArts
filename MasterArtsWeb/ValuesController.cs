using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace MasterArtsWeb
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public ShippingController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("rate")]
        public async Task<IActionResult> GetShippingRate([FromBody] object shippingRequest)
        {
            string token = await GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Failed to obtain access token.");
            }

            var client = _httpClientFactory.CreateClient();
            var apiBaseUrl = _configuration["UPSApi:ApiBaseUrl"];
            var requestUrl = $"{apiBaseUrl}/v2403/rate?requestOption=Rate";

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.PostAsync(requestUrl, new StringContent(shippingRequest.ToString(), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        private async Task<string> GetToken()
        {
            var client = _httpClientFactory.CreateClient();
            var tokenUrl = _configuration["UPSApi:TokenEndpoint"];
            var clientId = _configuration["UPSApi:ClientId"];
            var clientSecret = _configuration["UPSApi:ClientSecret"];

            var request = new HttpRequestMessage(HttpMethod.Post, tokenUrl)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", clientId },
                { "client_secret", clientSecret }
            })
            };

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenData = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent);
                return tokenData["access_token"];
            }

            return null;
        }
    }

}
