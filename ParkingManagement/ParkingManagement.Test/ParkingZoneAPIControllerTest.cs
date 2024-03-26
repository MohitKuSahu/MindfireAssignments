using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ParkingManagement.BL;
using ParkingManagement.Models;
using ParkingManagement.Utils;
using ParkingManagement.WebAPI.Controllers;

namespace ParkingManagement.Tests.Controllers
{
    public class ParkingZoneAPIControllerTest
    {
        [Fact]
        public async Task GetParkingZone_Returns_OkObjectResult_With_Data()
        {
            // Arrange
            var mockBL = new Mock<IBL>();
            var mockLog = new Mock<ILog>();
            var controller = new ParkingZoneAPIController(mockBL.Object, mockLog.Object);
            var expectedData = new List<ParkingZoneModel>(); // Provide expected data here

            mockBL.Setup(bl => bl.ListParkingZoneAsync()).ReturnsAsync(expectedData);

            // Act
            var result = await controller.GetParkingZone();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualData = Assert.IsType<List<ParkingZoneModel>>(okResult.Value);
            Assert.Equal(expectedData, actualData);
        }

        [Fact]
        public async Task AddParkingZone_Returns_OkObjectResult_With_Created_Model()
        {
            // Arrange
            var mockBL = new Mock<IBL>();
            var mockLog = new Mock<ILog>();
            var controller = new ParkingZoneAPIController(mockBL.Object, mockLog.Object);
            var modelToAdd = new ParkingZoneModel(); // Provide model to add here
            var expectedId = 1; // Provide expected id here

            mockBL.Setup(bl => bl.AddParkingZoneAsync(It.IsAny<ParkingZoneModel>())).ReturnsAsync(expectedId);

            // Act
            var result = await controller.AddParkingZone(modelToAdd);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedModel = Assert.IsType<ParkingZoneModel>(okResult.Value);
            Assert.Equal(expectedId, returnedModel.ParkingZoneId);
            // Add additional assertions as needed
        }

        [Fact]
        public async Task DeleteParkingZone_Returns_OkResult_On_Success()
        {
            // Arrange
            var mockBL = new Mock<IBL>();
            var mockLog = new Mock<ILog>();
            var controller = new ParkingZoneAPIController(mockBL.Object, mockLog.Object);
            var idToDelete = 1; // Provide id to delete here

            // Act
            var result = await controller.DeleteParkingZone(idToDelete);

            // Assert
            Assert.IsType<OkResult>(result.Result);
        }

 
    }
}

