using System;
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
    public class VehicleParkingAPIControllerTests
    {
        private readonly Mock<IBL> _mockBAL;
        private readonly Mock<ILog> _mockLog;
        private readonly VehicleParkingAPIController _controller;

        public VehicleParkingAPIControllerTests()
        {
            _mockBAL = new Mock<IBL>();
            _mockLog = new Mock<ILog>();
            _controller = new VehicleParkingAPIController(_mockBAL.Object, _mockLog.Object);
        }

        [Fact]
        public async Task GetVehicleParking_Returns_OkObjectResult_With_Data()
        {
            // Arrange
            var expectedData = new List<VehicleParkingModel> { new VehicleParkingModel() };
            _mockBAL.Setup(bal => bal.ListVehicleParkingAsync()).ReturnsAsync(expectedData);

            // Act
            var result = await _controller.GetVehicleParking();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualData = Assert.IsType<List<VehicleParkingModel>>(okResult.Value);
            Assert.Equal(expectedData, actualData);
        }

        [Fact]
        public async Task GetVehicleParkingById_Returns_OkObjectResult_With_Data()
        {
            // Arrange
            var expectedData = new List<VehicleParkingModel> { new VehicleParkingModel() };
            var parkingSpaceId = 1;
            _mockBAL.Setup(bal => bal.ListVehicleParkingByIdAsync(parkingSpaceId)).ReturnsAsync(expectedData);

            // Act
            var result = await _controller.GetVehicleParkingById(parkingSpaceId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualData = Assert.IsType<List<VehicleParkingModel>>(okResult.Value);
            Assert.Equal(expectedData, actualData);
        }

        [Fact]
        public async Task UpdateVehicle_Returns_OkResult()
        {
            // Arrange
            bool expectedResult = true;
            var model = new VehicleParkingModel();
            _mockBAL.Setup(bal => bal.UpdateVehicleAsync(model)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.UpdateVehicle(model);

            // Assert
            Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public async Task DeleteVehicle_Returns_OkResult()
        {
            // Arrange
            var id = 1;
            bool expectedResult = true;
            _mockBAL.Setup(bal => bal.DeleteVehicleParkingAsync(id)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DeleteVehicle(id);

            // Assert
            Assert.IsType<OkResult>(result.Result);
        }
    }
}
