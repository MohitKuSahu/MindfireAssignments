using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using ParkingManagement.BL;
using ParkingManagement.Models;
using ParkingManagement.Utils;
using Xunit;

namespace ParkingManagement.Tests.Controllers
{
    public class LoginAPIControllerTests
    {
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly Mock<IBL> _mockUserBAL;
        private readonly Mock<ILog> _mockLog;
        private readonly LoginAPIController _controller;

        public LoginAPIControllerTests()
        {
            _mockConfig = new Mock<IConfiguration>();
            _mockUserBAL = new Mock<IBL>();
            _mockLog = new Mock<ILog>();
            _controller = new LoginAPIController(_mockConfig.Object, _mockUserBAL.Object, _mockLog.Object);
        }

        [Fact]
        public async Task LoginAsync_Returns_OkObjectResult_With_Token_For_Valid_User()
        {
            // Arrange
            var user = new UserModel { 
                Name = "Test",      
                Email = "test@example.com", 
                Password = "password" ,
                Type= "Booking Counter Agent"
            };
            var expectedUserId = 1;
            _mockUserBAL.Setup(bl => bl.CheckIfUserExists(user)).ReturnsAsync(expectedUserId);
            _mockConfig.Setup(config => config["Jwt:Key"]).Returns("TestKey");
            _mockConfig.Setup(config => config["Jwt:Issuer"]).Returns("TestIssuer");
            _mockConfig.Setup(config => config["Jwt:Audience"]).Returns("TestAudience");

            // Act
            var result = await _controller.LoginAsync(user);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;
            Assert.NotNull(response);
            Assert.NotNull(response.Token);
            Assert.Equal(expectedUserId, response.UserId);
        }

        [Fact]
        public async Task LoginAsync_Returns_Unauthorized_For_Invalid_User()
        {
            // Arrange
            var user = new UserModel { Email = "invalid@example.com", Password = "wrongpassword" };
            _mockUserBAL.Setup(bl => bl.CheckIfUserExists(user)).ReturnsAsync(-1);

            // Act
            var result = await _controller.LoginAsync(user);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
