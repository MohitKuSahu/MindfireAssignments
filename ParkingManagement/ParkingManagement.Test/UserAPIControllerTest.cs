using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ParkingManagement.BL;
using ParkingManagement.Models;
using ParkingManagement.Utils;
using ParkingManagement.WebAPI.Controllers;
using Xunit;

namespace ParkingManagement.Tests.Controllers
{
    public class UserAPIControllerTests
    {
        private readonly Mock<IBL> _mockBAL;
        private readonly Mock<ILog> _mockLog;
        private readonly UserAPIController _controller;

        public UserAPIControllerTests()
        {
            _mockBAL = new Mock<IBL>();
            _mockLog = new Mock<ILog>();
            _controller = new UserAPIController(_mockBAL.Object, _mockLog.Object);
        }

        [Fact]
        public async Task InsertUser_Returns_Ok_For_Successful_Insertion()
        {
            // Arrange
            var user = new UserModel { Email = "test@example.com", Password = "password" };
            _mockBAL.Setup(bal => bal.InsertUser(user)).ReturnsAsync(true);

            // Act
            var result = await _controller.InsertUser(user);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var success = Assert.IsType<bool>(okResult.Value);
            Assert.True(success);
        }

        [Fact]
        public async Task InsertUser_Returns_NotFound_For_Unsuccessful_Insertion()
        {
            // Arrange
            var user = new UserModel { Email = "test@example.com", Password = "password" };
            _mockBAL.Setup(bal => bal.InsertUser(user)).ReturnsAsync(false);

            // Act
            var result = await _controller.InsertUser(user);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CheckEmail_Returns_Ok_With_Success_True_For_Existing_Email()
        {
            // Arrange
            var email = "existing@example.com";
            _mockBAL.Setup(bal => bal.CheckIfEmailAlreadyExists(email)).ReturnsAsync(true);

            // Act
            var result = await _controller.CheckEmail(email);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic; 
            bool success = response.GetType().GetProperty("success").GetValue(response, null);
            Assert.True(success);
        }

        [Fact]
        public async Task CheckEmail_Returns_Ok_With_Success_False_For_NonExisting_Email()
        {
            // Arrange
            var email = "InvalidEmail";
            _mockBAL.Setup(bal => bal.CheckIfEmailAlreadyExists(email)).ReturnsAsync(false);

            // Act
            var result = await _controller.CheckEmail(email);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic; // Cast to dynamic to access properties
            bool success = response.GetType().GetProperty("success").GetValue(response, null);
            Assert.False(success);
        }




    }
}

