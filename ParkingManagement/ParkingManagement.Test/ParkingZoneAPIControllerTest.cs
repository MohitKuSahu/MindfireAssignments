using System.Collections.Generic;
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
    public class ParkingZoneAPIControllerTests
    {
        private readonly Mock<IBL> _mockBL;
        private readonly Mock<ILog> _mockLog;
        private readonly ParkingZoneAPIController _controller;

        public ParkingZoneAPIControllerTests()
        {
            _mockBL = new Mock<IBL>();
            _mockLog = new Mock<ILog>();
            _controller = new ParkingZoneAPIController(_mockBL.Object, _mockLog.Object);
        }

        [Fact]
        public async Task GetParkingZone_Returns_OkObjectResult_With_Data()
        {
            // Arrange
            var expectedData = new List<ParkingZoneModel> { new ParkingZoneModel() };
            _mockBL.Setup(bl => bl.ListParkingZoneAsync()).ReturnsAsync(expectedData);

            // Act
            var result = await _controller.GetParkingZone();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualData = Assert.IsType<List<ParkingZoneModel>>(okResult.Value);
            Assert.Equal(expectedData, actualData);
        }

        [Fact]
        public async Task AddParkingZone_Returns_OkObjectResult_With_Created_Model()
        {
            // Arrange
            var modelToAdd = new ParkingZoneModel();
            var expectedId = 1;
            _mockBL.Setup(bl => bl.AddParkingZoneAsync(It.IsAny<ParkingZoneModel>())).ReturnsAsync(expectedId);

            // Act
            var result = await _controller.AddParkingZone(modelToAdd);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedModel = Assert.IsType<ParkingZoneModel>(okResult.Value);
            Assert.Equal(expectedId, returnedModel.ParkingZoneId);
        }

        [Fact]
        public async Task DeleteParkingZone_Returns_OkResult_On_Success()
        {
            // Arrange
            var idToDelete = 1;

            // Act
            var result = await _controller.DeleteParkingZone(idToDelete);

            // Assert
            Assert.IsType<OkResult>(result.Result);
        }

        
    }
}
