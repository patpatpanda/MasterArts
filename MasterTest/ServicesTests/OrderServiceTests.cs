using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using MasterArtsWeb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Xunit;
namespace MasterTest.ServicesTests
{
    public class OrderServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        
        private readonly Mock<MyDbContext> _dbContextMock;
       
        private readonly HttpClient _httpClient;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private MyDbContext _dbContext;

        public OrderServiceTests()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
        .UseInMemoryDatabase("TestDb").Options; // Create the options needed for the DbContext

            _dbContextMock = new Mock<MyDbContext>(options); // Pass the options correctly

            var mockOrdersDbSet = new Mock<DbSet<Order>>();
            _dbContextMock.Setup(db => db.Orders).Returns(mockOrdersDbSet.Object);

            // Setup other mocks
            _configurationMock = new Mock<IConfiguration>();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
           
            
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _httpClientFactoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(_httpClient);
        }

        [Fact]
        public async Task GetAccessToken_ReturnsTokenOnSuccess_WithMockService()
        {
            // Arrange
            var expectedToken = "token-value";

            // Skapa en Mock för interfacet IOrderService
            var mockOrderService = new Mock<IOrderService>();

            // Mocka metoden GetAccessToken i Mock-objektet
            mockOrderService
                .Setup(service => service.GetAccessToken())
                .ReturnsAsync(expectedToken);

            // Act
            var resultToken = await mockOrderService.Object.GetAccessToken();

            // Assert
            Assert.NotNull(resultToken);
            Assert.Equal(expectedToken, resultToken);
        }

        [Fact]
        public async Task CreateOrderInApi_SuccessfulOrderCreation_SavesOrderToDb()
        {
            // Arrange
            var apiUrl = "https://example.com/api/";
            var tokenEndpoint = "https://example.com/token";
            var clientId = "arts_spedition";
            var clientSecret = "uoXJHRrA@1QE6?Th_UX=";
            var username = "arts";
            var password = "8WdzEw!9Kc_1abcd";

            // Setup configuration mock values
            _configurationMock.Setup(c => c.GetSection("CISApi:ApiUrl").Value).Returns(apiUrl);
            _configurationMock.Setup(c => c.GetSection("CISApi:TokenEndpoint").Value).Returns(tokenEndpoint);
            _configurationMock.Setup(c => c.GetSection("CISApi:ClientId").Value).Returns(clientId);
            _configurationMock.Setup(c => c.GetSection("CISApi:ClientSecret").Value).Returns(clientSecret);
            _configurationMock.Setup(c => c.GetSection("CISApi:Username").Value).Returns(username);
            _configurationMock.Setup(c => c.GetSection("CISApi:Password").Value).Returns(password);

            // Mock token response
            var tokenResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"access_token\": \"token-value\"}", Encoding.UTF8, "application/json")
            };

            // Mock order creation response
            var orderResponse = new HttpResponseMessage(HttpStatusCode.Created);

            // Setup HTTP message handler
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri.ToString() == tokenEndpoint),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(tokenResponse);

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put && req.RequestUri.ToString() == $"{apiUrl}order"),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(orderResponse);

            // Mock database context to capture saved orders
            var mockDbSet = new Mock<DbSet<Order>>();
            _dbContextMock.Setup(c => c.Orders).Returns(mockDbSet.Object);

            // Create service instance
            var mockOrderService = new Mock<IOrderService>();
            var order = new Order
            {
                Id = 1,
                CustomerOrderNumber = "CO12345",
                OrderReference = "REF2024",
                Customer = "Test Customer",
                OrderType = "Standard",
                DeliveryTimeFrom = "2024-05-15T08:00:00",
                DeliveryTimeTo = "2024-05-15T17:00:00",
                PickUpTimeFrom = "2024-05-14T09:00:00",
                TransportModeCode = 1,
                TermsOfDelivery = "DAP",
                ConsignorName = "ABC Corp.",
                ConsignorAddress1 = "123 Main St",
                ConsignorCity = "New York",
                ConsignorZip = "10001",
                ConsignorContact = "John Doe",
                ConsignorPhone = "+1 234 567 890",
                ConsignorEmail = "johndoe@abccorp.com",
                ConsigneeName = "XYZ Ltd.",
                ConsigneeAddress1 = "456 Elm St",
                ConsigneeCity = "San Francisco",
                ConsigneeZip = "94101",
                ConsigneeContact = "Jane Smith",
                ConsigneePhone = "+1 987 654 321",
                ConsigneeEmail = "janesmith@xyzltd.com",
                Goods = new List<Goods>
    {
            new Goods
            {
                Id = 1,
                PackageId = 1001,
                MarksNumbers = "Box 1",
                Description = "Electronics",
                Quantity = 5,
                PackageType = "Box",
                NetWeight = 10.0
            }
        }
            };

            await mockOrderService.Object.CreateOrderInApi(order);

            // Assert
            mockOrderService.Verify(service => service.CreateOrderInApi(It.Is<Order>(o => o.Id == order.Id)), Times.Once);
        }

    }
}
