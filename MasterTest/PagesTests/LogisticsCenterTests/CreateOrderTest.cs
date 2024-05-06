using Xunit;
using Moq;
using MasterArtsWeb.Pages.LogisticsCenter;
using MasterArtsLibrary.Services;
using MasterArtsLibrary.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace MasterTest.PagesTests.LogisticsCenterTests
{
    public class CreateOrderTest
    {
        [Fact]
        public async Task OnPostAsync_ValidOrder_ShouldSendOrderToApiAndRedirect()
        {
            // Arrange
            var orderServiceMock = new Mock<OrderService>();
            var loggerMock = new Mock<ILogger<ShipmentCalculatorModel>>();
            var languageServiceMock = new Mock<LanguageService>();
            var configurationMock = new Mock<IConfiguration>();
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();

            var model = new ShipmentCalculatorModel(languageServiceMock.Object, httpClientFactoryMock.Object, orderServiceMock.Object, null, null, loggerMock.Object, configurationMock.Object)
            {
                TempData = new TempDataDictionary(null, null), // You can mock TempData as needed
                Order = new Order() // Provide valid order data
            };

            // Act
            var result = await model.OnPostAsync();

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
            orderServiceMock.Verify(
                service => service.CreateOrderInApi(It.IsAny<Order>()),
                Times.Once);
            Assert.Equal("Your order has been sent", model.TempData["SuccessMessage"]);
        }


        [Fact]
        public async Task CreateOrderInApi_ValidOrder_ShouldCallApiAndLogSuccess()
        {
            // Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var loggerMock = new Mock<ILogger<OrderService>>();
            var configurationMock = new Mock<IConfiguration>();

            var expectedOrder = new Order { /* Set order properties */ };

            var mockHttpClient = new Mock<HttpClient>();
            var responseContent = new StringContent(JsonSerializer.Serialize(new TokenResponse { Access_token = "mockAccessToken" }), Encoding.UTF8, "application/json");
            mockHttpClient.Setup(client => client.PostAsync(It.IsAny<string>(), It.IsAny<FormUrlEncodedContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) { Content = responseContent });

            httpClientFactoryMock.Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(mockHttpClient.Object);

            var orderService = new OrderService(httpClientFactoryMock.Object, loggerMock.Object, configurationMock.Object);

            // Act
            await orderService.CreateOrderInApi(expectedOrder);

            // Assert
            mockHttpClient.Verify(client => client.PutAsJsonAsync(It.IsAny<string>(), expectedOrder), Times.Once);
            loggerMock.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once);
        }

    }

   

}








