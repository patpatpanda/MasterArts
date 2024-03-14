
using MasterArtsLibrary.Models;
using MasterArtsLibrary.ViewModels;
using MasterArtsWeb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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


        private static readonly List<string> AllCountries = new List<string>
{
    "Afghanistan",
    "Albania",
    "Algeria",
    "Andorra",
    "Angola",
    "Antigua and Barbuda",
    "Argentina",
    "Armenia",
    "Australia",
    "Austria",
    "Azerbaijan",
    "Bahamas",
    "Bahrain",
    "Bangladesh",
    "Barbados",
    "Belarus",
    "Belgium",
    "Belize",
    "Benin",
    "Bhutan",
    "Bolivia",
    "Bosnia and Herzegovina",
    "Botswana",
    "Brazil",
    "Brunei",
    "Bulgaria",
    "Burkina Faso",
    "Burundi",
    "Cabo Verde",
    "Cambodia",
    "Cameroon",
    "Canada",
    "Central African Republic",
    "Chad",
    "Chile",
    "China",
    "Colombia",
    "Comoros",
    "Congo, Republic of the",
    "Congo, Democratic Republic of the",
    "Costa Rica",
    "Cote d'Ivoire",
    "Croatia",
    "Cuba",
    "Cyprus",
    "Czech Republic",
    "Denmark",
    "Djibouti",
    "Dominica",
    "Dominican Republic",
    "Ecuador",
    "Egypt",
    "El Salvador",
    "Equatorial Guinea",
    "Eritrea",
    "Estonia",
    "Eswatini",
    "Ethiopia",
    "Fiji",
    "Finland",
    "France",
    "Gabon",
    "Gambia",
    "Georgia",
    "Germany",
    "Ghana",
    "Greece",
    "Grenada",
    "Guatemala",
    "Guinea",
    "Guinea-Bissau",
    "Guyana",
    "Haiti",
    "Honduras",
    "Hungary",
    "Iceland",
    "India",
    "Indonesia",
    "Iran",
    "Iraq",
    "Ireland",
    "Israel",
    "Italy",
    "Jamaica",
    "Japan",
    "Jordan",
    "Kazakhstan",
    "Kenya",
    "Kiribati",
    "Kosovo",
    "Kuwait",
    "Kyrgyzstan",
    "Laos",
    "Latvia",
    "Lebanon",
    "Lesotho",
    "Liberia",
    "Libya",
    "Liechtenstein",
    "Lithuania",
    "Luxembourg",
    "Madagascar",
    "Malawi",
    "Malaysia",
    "Maldives",
    "Mali",
    "Malta",
    "Marshall Islands",
    "Mauritania",
    "Mauritius",
    "Mexico",
    "Micronesia",
    "Moldova",
    "Monaco",
    "Mongolia",
    "Montenegro",
    "Morocco",
    "Mozambique",
    "Myanmar",
    "Namibia",
    "Nauru",
    "Nepal",
    "Netherlands",
    "New Zealand",
    "Nicaragua",
    "Niger",
    "Nigeria",
    "North Korea",
    "North Macedonia",
    "Norway",
    "Oman",
    "Pakistan",
    "Palau",
    "Palestine",
    "Panama",
    "Papua New Guinea",
    "Paraguay",
    "Peru",
    "Philippines",
    "Poland",
    "Portugal",
    "Qatar",
    "Romania",
    "Russia",
    "Rwanda",
    "Saint Kitts and Nevis",
    "Saint Lucia",
    "Saint Vincent and the Grenadines",
    "Samoa",
    "San Marino",
    "Sao Tome and Principe",
    "Saudi Arabia",
    "Senegal",
    "Serbia",
    "Seychelles",
    "Sierra Leone",
    "Singapore",
    "Slovakia",
    "Slovenia",
    "Solomon Islands",
    "Somalia",
    "South Africa",
    "South Korea",
    "South Sudan",
    "Spain",
    "Sri Lanka",
    "Sudan",
    "Suriname",
    "Sweden",
    "Switzerland",
    "Syria",
    "Taiwan",
    "Tajikistan",
    "Tanzania",
    "Thailand",
    "Timor-Leste",
    "Togo",
    "Tonga",
    "Trinidad and Tobago",
    "Tunisia",
    "Turkey",
    "Turkmenistan",
    "Tuvalu",
    "Uganda",
    "Ukraine",
    "United Arab Emirates",
    "United Kingdom",
    "United States",
    "Uruguay",
    "Uzbekistan",
    "Vanuatu",
    "Vatican City",
    "Venezuela",
    "Vietnam",
    "Yemen",
    "Zambia",
    "Zimbabwe"
};


        public OrderService(MyDbContext context,IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<OrderService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
            _context = context;
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


        public Task<List<string>> GetAllCountries()
        {
            if (AllCountries != null)
            {
                return Task.FromResult(AllCountries);
            }
            else
            {
                // Om listan är null, returnera en tom lista
                return Task.FromResult(new List<string>());
            }
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

        ////public async Task<int> CreateOrderInApi(Order order)
        ////{


        //////    using (var httpClient = _httpClientFactory.CreateClient())
        //////    {
        //////        var response = await httpClient.PostAsJsonAsync($"https://localhost:7009/api/OrderApi", order);

        //////        if (response.IsSuccessStatusCode)
        //////        {
        //////            // Anta att API:et returnerar det genererade OrderId som en del av svaret
        //////            // Anpassa denna del baserat på hur ditt API faktiskt returnerar data
        //////            var orderIdString = await response.Content.ReadAsStringAsync();
        //////            // Om API:et returnerar ett enkelt ID som en sträng eller ett nummer
        //////            int orderId = int.Parse(orderIdString); // Anpassa parsingen baserat på svaret

        //////            return orderId;
        //////        }
        //////        else
        //////        {
        //////            // Hantera fel, kanske genom att kasta ett exception eller returnera ett speciellt värde
        //////            throw new Exception("Failed to create order in API");
        //////        }
        //////    }
        //////}

        public async Task CreateOrderInApi(Order order)
        {
            var apiUrl = _configuration.GetSection("CISApi:ApiUrl").Value;
            var clientId = _configuration.GetSection("CISApi:ClientId").Value;
            var clientSecret = _configuration.GetSection("CISApi:ClientSecret").Value;
            var username = _configuration.GetSection("CISApi:Username").Value;
            var password = _configuration.GetSection("CISApi:Password").Value;

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var tokenRequestData = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "username", username },
                { "password", password }
            };

                HttpResponseMessage tokenResponse;
                try
                {
                    tokenResponse = await httpClient.PostAsync($"{_configuration.GetSection("CISApi:TokenEndpoint").Value}", new FormUrlEncodedContent(tokenRequestData));
                }
                catch (HttpRequestException e)
                {
                    _logger.LogError($"Fel vid anslutning till token endpoint: {e.Message}");
                    return;
                }

                if (!tokenResponse.IsSuccessStatusCode)
                {
                    var errorContent = await tokenResponse.Content.ReadAsStringAsync();
                    _logger.LogError($"Tokenförfrågan misslyckades med statuskod {tokenResponse.StatusCode} och felmeddelande: {errorContent}");
                    return;
                }

                var tokenResponseBody = await tokenResponse.Content.ReadAsStringAsync();
                var tokenData = JsonSerializer.Deserialize<TokenResponse>(tokenResponseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var accessToken = tokenData?.Access_token;

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var orderResponse = await httpClient.PutAsJsonAsync(apiUrl + "order", order); // Se till att URL:en är korrekt

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
                    _logger.LogError($"Skapande av order misslyckades med statuskod {orderResponse.StatusCode} och felmeddelande: {errorContent}");
                }
            }
        }







    }
}

