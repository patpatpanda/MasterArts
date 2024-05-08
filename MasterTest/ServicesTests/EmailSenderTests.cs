using MasterArtsLibrary.Services;
using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MasterTest.ServicesTests
{
    public class EmailSenderTests
    {
        private readonly EmailSender _emailSender;
        private readonly Mock<IConfiguration> _mockConfiguration;

        public EmailSenderTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            // Setup the indexer to return a value for the key
            _mockConfiguration.Setup(c => c["SendGrid:SecretKey"]).Returns("test-key");

            _emailSender = new EmailSender(_mockConfiguration.Object);
        }

        [Fact]
        public async Task SendEmailAsync_ValidResponse_DoesNotThrow()
        {
            var mockSendGridClient = new Mock<ISendGridClient>();
            mockSendGridClient
                .Setup(client => client.SendEmailAsync(It.IsAny<SendGridMessage>(), default))
                .ReturnsAsync(new Response(HttpStatusCode.OK, null, null)); // Ändra till OK eller Accepted

            var emailSender = new EmailSender(_mockConfiguration.Object, mockSendGridClient.Object);

            await emailSender.SendEmailAsync("ops@artslogistics.se", "Test Subject", "Test Message");
        }


        [Fact]
        public async Task SendEmailAsync_InvalidResponse_ThrowsInvalidOperationException()
        {
            var mockSendGridClient = new Mock<ISendGridClient>();
            mockSendGridClient
                .Setup(client => client.SendEmailAsync(It.IsAny<SendGridMessage>(), default))
                .ReturnsAsync(new Response(HttpStatusCode.BadRequest, null, null));

            // Make sure to inject the mock client into your EmailSender, or abstract its creation for testing.
            // _emailSender.SendGridClient = mockSendGridClient.Object;

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _emailSender.SendEmailAsync("ops@artslogistics.se", "Test Subject", "Test Message")
            );
        }
    }

}
