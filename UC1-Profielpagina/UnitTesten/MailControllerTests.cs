using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using ShowcaseAPI.Controllers;
using MailAPI;

namespace ShowcaseAPI.Tests
{
    [TestFixture]
    public class MailControllerTests
    {
        private MailController _controller;
        private Mock<IConfiguration> _configurationMock;

        [SetUp]
        public void Setup()
        {
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c["SMTP:Username"]).Returns("test_username");
            _configurationMock.Setup(c => c["SMTP:Password"]).Returns("test_password");

            _controller = new MailController(_configurationMock.Object);
        }

        [Test]
        public async Task SendMail_ValidRequest_ReturnsOk()
        {
            // Arrange
            var request = new ContactRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                Subject = "Test",
                Message = "Hello"
            };

            // Act
            var result = await _controller.SendMail(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}