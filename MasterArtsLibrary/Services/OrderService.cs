
using MasterArtsLibrary.Models;
using MasterArtsLibrary.ViewModels;
using MasterArtsWeb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace MasterArtsLibrary.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IOrderEmailSender _order;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OrderService> _logger;
        private readonly MyDbContext _context;
        
        public Order Order { get; set; }


      


        public OrderService(MyDbContext context,IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<OrderService> logger, HttpClient httpClient)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _httpClient = httpClient;
            var authValue = Convert.ToBase64String(Encoding.ASCII.GetBytes("nils-emil1337@hotmail.se:27wH5AFp"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);
        }


        public async Task SendOrderConfirmationEmail(string recipientEmail, Order order)
        {
            // Konstruera e-postmeddelandet
            var subject = "Order Confirmation";
            var htmlMessage = $@"
        <html>
        <head>
           <style>
                .container {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    padding: 20px;
                    border-radius: 10px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                }}
                .header {{
                    background-color: #007bff;
                    color: white;
                    padding: 10px;
                    border-radius: 10px 10px 0 0;
                    text-align: center;
                }}
                .content p {{
                    color: #333;
                }}
            </style>
        </head>
        <body>
            <div class='email-container'>
                <div class='header'>
                    <h1>Orderbekräftelse</h1>
                </div>
                <div class='content'>
                    <p>Tack för din beställning!</p>
                    <p>Order ID: {order.Id}</p>
                    
                    <!-- Lägg till mer orderinformation här om det behövs -->
                </div>
                <div class='footer'>
                    <p>Om du har frågor, vänligen kontakta oss på ops@artslogistics.se eller ring +46 70-549 14 14</p>
                </div>
            </div>
        </body>
        </html>";

            // Skicka e-postmeddelandet
            await _order.SendEmailOrderAsync(recipientEmail, subject, htmlMessage);
        }


        

        public async Task<Order> GetOrderViewModelAsync(int orderId)
        {
            var apiUrl = _configuration.GetSection("ApiUrl").Value;
            var response = await _httpClient.GetAsync($"https://localhost:7009/api/OrderApi/{orderId}");


            if (response.IsSuccessStatusCode)
            {
                var order = await response.Content.ReadFromJsonAsync<Order>();

                if (order != null)
                {
                    return (order);
                }
            }

            return null;
        }

        public async Task<object> CalculateRatesAsync(ShippingRequest shippingRequest)
        {
            var authValue = Convert.ToBase64String(Encoding.ASCII.GetBytes("nils-emil1337@hotmail.se:27wH5AFp"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var jsonContent = JsonConvert.SerializeObject(shippingRequest, settings);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var endpoint = shippingRequest.UserSelection == "inlandtransport" ? "inlandtransport" : "calculaterateslcl";
            var url = $"https://ncse.nordic-on.com/api/v1/{endpoint}";

            try
            {
                var response = await _httpClient.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    // Använd olika deserialisering baserat på endpoint
                    if (endpoint == "inlandtransport")
                    {
                        return JsonConvert.DeserializeObject<ApiResponseInland>(responseString);
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<ApiResponse>(responseString);
                    }
                }
                else
                {
                    // Hantera icke-lyckade svar på lämpligt sätt
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Logga och hantera undantag
                return null;
            }
        }


        public async Task<ApiResponse> CalculateRatesFclAsync(FclRate shippingRequest, RateType rateType)
        {
            var authValue = Convert.ToBase64String(Encoding.ASCII.GetBytes("nils-emil1337@hotmail.se:27wH5AFp"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var jsonContent = JsonConvert.SerializeObject(shippingRequest, settings);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Bygg URL baserat på RateType
            var endpoint = rateType == RateType.LCL ? "calculaterateslcl" : "calculateratesfcl";
            var url = $"https://ncse.nordic-on.com/api/v1/{endpoint}";

            try
            {
                var response = await _httpClient.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResponse>(responseString);
                }
                else
                {
                    _logger.LogError("Servern returnerade ett fel: " + await response.Content.ReadAsStringAsync());
                    return null; 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Ett undantag inträffade: " + ex.Message);
                return null; 
            }
        }




        public async Task CreateOrderInApi(Order order)
        {
            var apiUrl = _configuration.GetSection("CISApi:ApiUrl").Value;
            var clientId = _configuration.GetSection("CISApi:ClientId").Value;
            var clientSecret = _configuration.GetSection("CISApi:ClientSecret").Value;
            var username = _configuration.GetSection("CISApi:Username").Value;
            var password = _configuration.GetSection("CISApi:Password").Value;

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes
                    ($"{clientId}:{clientSecret}"));
                httpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Basic", credentials);

                var tokenRequestData = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "username", username },
                { "password", password }
            };

                HttpResponseMessage tokenResponse;
                try
                {
                    tokenResponse = await httpClient.PostAsync
                        ($"{_configuration.GetSection("CISApi:TokenEndpoint").Value}",
                        new FormUrlEncodedContent(tokenRequestData));
                }
                catch (HttpRequestException e)
                {
                    _logger.LogError($"Fel vid anslutning till token endpoint: {e.Message}");
                    return;
                }

                if (!tokenResponse.IsSuccessStatusCode)
                {
                    var errorContent = await tokenResponse.Content.ReadAsStringAsync();
                    _logger.LogError($"Tokenförfrågan misslyckades med statuskod " +
                        $"{tokenResponse.StatusCode} och felmeddelande: {errorContent}");
                    return;
                }

                var tokenResponseBody = await tokenResponse.Content.ReadAsStringAsync();
                var tokenData = JsonSerializer.Deserialize<TokenResponse>
                    (tokenResponseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var accessToken = tokenData?.AccessToken;
                // This should work if tokenData is not null and the JSON was parsed correctly


                httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", accessToken);
                var orderResponse = await httpClient.PutAsJsonAsync(apiUrl + "order", order);
                // Se till att URL:en är korrekt

                if (orderResponse.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Order skapades lyckat med statuskod {orderResponse.StatusCode}.");
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                    // Här kan du hantera den skapade ordern vidare om så önskas
                }
                else
                {
                    var errorContent = await orderResponse.Content.ReadAsStringAsync();
                    _logger.LogError
                        ($"Skapande av order misslyckades med statuskod" +
                        $" {orderResponse.StatusCode} och felmeddelande: {errorContent}");
                }
            }
        }



        public async Task<TransportInformation> TrackAndTraceAsync(string trackingCode)
        {
            var url = $"https://ncse.nordic-on.com/api/v1/trackandtrace?input={trackingCode}";
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var transportInformation = JsonConvert.DeserializeObject<TransportInformation>(responseContent);
                    return transportInformation;
                }
                else
                {
                   
                    throw new Exception($"API call failed: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
               
                throw new Exception($"An error occurred while calling the API: {ex.Message}", ex);
            }
        }

        public async Task<string> GetAccessToken()
        {
            var clientId = _configuration["UPSApi:ClientId"];
            var clientSecret = _configuration["UPSApi:ClientSecret"];
            var tokenUrl = _configuration["UPSApi:TokenEndpoint"];

            var clientCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, tokenUrl))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", clientCredentials);
                requestMessage.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "client_credentials"}
            });

                var response = await _httpClient.SendAsync(requestMessage);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Token request failed with status code {response.StatusCode} and error message: {errorContent}");
                    return null;
                }

                var tokenResponse = await response.Content.ReadAsStringAsync();
                var tokenData = JsonSerializer.Deserialize<Dictionary<string, string>>(tokenResponse);
                return tokenData.ContainsKey("access_token") ? tokenData["access_token"] : null;
            }
        }


        public async Task<RateResponse> GetShippingRatesAsync(string jsonRequest)
        {
            // Retrieve the access token
            var accessToken = await GetAccessToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                _logger.LogError("Failed to obtain access token.");
                throw new InvalidOperationException("Failed to obtain access token.");
            }

            // Set up request headers
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var fullApiUrl = "https://wwwcie.ups.com/api/rating/v2403/rate?requestOption=Rate";

            try
            {
                // Create StringContent from the JSON request
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                _logger.LogInformation($"Sending JSON request: {jsonRequest}");

                // Send the request
                HttpResponseMessage response = await _httpClient.PostAsync(fullApiUrl, content);
                response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response status is an error code.

                // Read and deserialize the response
                var responseJson = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Received JSON response: {responseJson}");
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null // Ensures JSON properties are mapped exactly as defined in your model
                };
                var upsResponse = JsonSerializer.Deserialize<RateResponse>(responseJson, options);

                return upsResponse;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An HTTP error occurred: {ex.Message}");
                throw;
            }
            catch (System.Text.Json.JsonException ex)
            {
                _logger.LogError($"JSON parsing error: {ex.Message}");
                throw;
            }
        }


        private class TokenResponse
        {
            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; }
            [JsonPropertyName("token_type")]
            public string TokenType { get; set; }
            [JsonPropertyName("expires_in")]
            public int ExpiresIn { get; set; }
        }

    }

}


