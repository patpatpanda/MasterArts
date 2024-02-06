
using MasterArtsLibrary.Models;
using MasterArtsLibrary.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IOrderEmailSender _order;
        private readonly IHttpClientFactory _httpClientFactory;
        public OrderService(HttpClient httpClient, IHttpClientFactory httpClientFactory, IConfiguration configuration, IOrderEmailSender orderEmailSender)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _order = orderEmailSender;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Order> GetOrderViewModelAsync(int orderId)
        {
            var apiUrl = _configuration.GetSection("ApiUrl").Value;
            var response = await _httpClient.GetAsync($"https://localhost:7223/api/OrderApi/{orderId}");


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
                var response = await httpClient.PostAsJsonAsync($"https://localhost:7223/api/OrderApi", order);

                if (!response.IsSuccessStatusCode)
                {

                }
            }
        }

        public async Task SendOrderConfirmationEmail(string recipientEmail, Order order)
        {
            // Konstruera e-postmeddelandet
            var subject = "Order Confirmation";
            var htmlMessage = $"Thank you for your order! Order ID: {order.Id}, ShipTo: ";

            // Skicka e-postmeddelandet
            await _order.SendEmailOrderAsync(recipientEmail, subject, htmlMessage);
        }


    
}
}
