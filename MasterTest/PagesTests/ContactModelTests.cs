using Xunit;
using Moq;
using MasterArtsWeb.Pages;
using MasterArtsLibrary.Services;
using MasterArtsLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MasterArtsWeb.Tests
{
    public class ContactModelTests
    {
        [Fact]
        public async Task OnPost_ValidFormData_ShouldSendEmailAndRedirect()
        {
            // Arrange
            var emailSenderMock = new Mock<IEmailSender>();
           
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            var controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { }
            };
            controllerContext.HttpContext.Items["__TempDataDictionaryFactory"] = new TempDataDictionaryFactory(Mock.Of<ITempDataProvider>());
            var contactModel = new ContactModel(emailSenderMock.Object)
            {
                ControllerContext = controllerContext,
                TempData = tempData
            };
            contactModel.Input = new ContactFormModel
            {
                // Here you can assign values to the ContactFormModel properties to test a valid form
                CompanyName = "TestCompany",
                Name = "TestUser",
                Phone = "123456789",
                Email = "test@example.com",
                Message = "Test message"
            };

            // Act
            var result = await contactModel.OnPost();

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
            emailSenderMock.Verify(
                sender => sender.SendEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
            Assert.Equal("Your message has been sent", tempData["SuccessMessage"]);
        }

        // Here you can write more tests for different scenarios, for example, when the form is not valid
    }
}
