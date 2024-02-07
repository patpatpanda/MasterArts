
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
            

            //public async Task CreateOrderInApi(Order order)
            //{
            //    var apiUrl = _configuration.GetSection("ApiUrl").Value;
            //    var clientId = _configuration.GetSection("ClientId").Value;
            //    var clientSecret = _configuration.GetSection("ClientSecret").Value;
            //    var username = _configuration.GetSection("Username").Value;
            //    var password = _configuration.GetSection("Password").Value;

            //    // Skapa en HttpClient
            //    using (var httpClient = _httpClientFactory.CreateClient())
            //    {
            //        // Skapa en Base64-kodad sträng av klientidentifiering och klienthemlighet
            //        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

            //        // Autentisera din applikation med OAuth2
            //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            //        // Skapa en dictionary för att innehålla uppgifter för token-förfrågan
            //        var tokenRequestData = new Dictionary<string, string>
            //{
            //    { "grant_type", "password" },
            //    { "username", username },
            //    { "password", password }
            //};

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
}
