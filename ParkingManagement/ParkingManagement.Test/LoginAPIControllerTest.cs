using Microsoft.AspNetCore.Mvc;
using Moq;
using ParkingManagement.Models;
using ParkingManagement.BL;
using ParkingManagement.Utils;
using ParkingManagement.WebAPI.Controllers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ParkingManagement.Tests.Controllers
{
    public class LoginAPIControllerTest
    {
        [Fact]
        public async Task LoginAsync_Returns_Unauthorized_When_User_Not_Authenticated()
        {
            // Arrange
            var mockConfig = new Mock<IConfiguration>();
            var mockUserBAL = new Mock<IBL>();
            var mockLog = new Mock<ILog>();

            var controller = new LoginAPIController(mockConfig.Object, mockUserBAL.Object, mockLog.Object);
            var user = new UserModel(); // Provide user model for testing

            mockUserBAL.Setup(ub => ub.CheckIfUserExists(user)).ReturnsAsync(-1); // Simulate user not found

            // Act
            var result = await controller.LoginAsync(user);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        
    }
}
