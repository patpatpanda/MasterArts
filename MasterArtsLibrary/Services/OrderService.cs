
using MasterArtsLibrary.Models;
using MasterArtsLibrary.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public OrderService(HttpClient httpClient, IHttpClientFactory httpClientFactory, IConfiguration configuration, IOrderEmailSender orderEmailSender)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _order = orderEmailSender;
            _httpClientFactory = httpClientFactory;
        }


        public async Task SendOrderConfirmationEmail(string recipientEmail, Order order)
        {
            // Konstruera e-postmeddelandet
            var subject = "Order Confirmation";
            var htmlMessage = $"Thank you for your order! Order ID";

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

        public async Task CreateOrderInApi(Order order)
        {
            var apiUrl = _configuration.GetSection("ApiUrl").Value;

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.PostAsJsonAsync($"https://localhost:7009/api/OrderApi", order);

                if (!response.IsSuccessStatusCode)
                {
                    order = Order;
                }
            }

        }
        //public async Task CreateOrderInApi(Order order)
        //{
        //    var apiUrl = _configuration.GetSection("ApiUrl").Value;
        //    var clientId = _configuration.GetSection("ClientId").Value;
        //    var clientSecret = _configuration.GetSection("ClientSecret").Value;
        //    var username = clientId;
        //    var password = clientSecret;

        //    // Skapa en HttpClient
        //    using (var httpClient = _httpClientFactory.CreateClient())
        //    {
        //        // Skapa en Base64-kodad sträng av klientidentifiering och klienthemlighet
        //        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

        //        // Autentisera din applikation med OAuth2
        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

        //        // Skapa en dictionary för att innehålla uppgifter för token-förfrågan
        //        var tokenRequestData = new Dictionary<string, string>
        //    {
        //        { "grant_type", "password" },
        //        { "username", username },
        //        { "password", password }
        //    };

        //        // Skicka POST-förfrågan till Token Endpoint för att få en åtkomsttoken
        //        var tokenResponse = await httpClient.PostAsync("https://cis.cargoit.se/auth/oauth/token", new FormUrlEncodedContent(tokenRequestData));
        //        tokenResponse.EnsureSuccessStatusCode(); // Kasta ett undantag om förfrågan inte lyckas

        //        // Läs och tolka svaret från servern
        //        var tokenResponseBody = await tokenResponse.Content.ReadAsStringAsync();
        //        var tokenData = JsonSerializer.Deserialize<Dictionary<string, string>>(tokenResponseBody);
        //        var accessToken = tokenData["access_token"];

        //        // Använd åtkomsttokenet för att göra en begäran till API-et
        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        //        var orderResponse = await httpClient.PostAsJsonAsync(apiUrl, order);

        //        // Hantera svar från API-et enligt behov
        //        if (orderResponse.IsSuccessStatusCode)
        //        {
        //            // Order skapades framgångsrikt
        //            var createdOrder = await orderResponse.Content.ReadFromJsonAsync<Order>();
        //        }
        //        else
        //        {
        //            // Det uppstod ett fel när order skapades
        //            // Hantera felet här
        //        }
        //    }
        //}






    }
    }

