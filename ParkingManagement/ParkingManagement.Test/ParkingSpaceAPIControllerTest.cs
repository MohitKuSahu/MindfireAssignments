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
    public class ParkingSpaceAPIControllerTest
    {
        [Fact]
        public async Task GetParkingSpace_Returns_OkObjectResult_With_Data()
        {
            // Arrange
            var mockBL = new Mock<IBL>();
            var mockLog = new Mock<ILog>();
            var controller = new ParkingSpaceAPIController(mockBL.Object, mockLog.Object);
            var expectedData = new List<ParkingSpaceModel>(); 

            mockBL.Setup(bl => bl.ListParkingSpaceAsync()).ReturnsAsync(expectedData);

            // Act
            var result = await controller.GetParkingSpace();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualData = Assert.IsType<List<ParkingSpaceModel>>(okResult.Value);
            Assert.Equal(expectedData, actualData);
        }

        [Fact]
        public async Task ListParkingSpaceById_Returns_OkObjectResult_With_Data()
        {
            // Arrange
            var mockBL = new Mock<IBL>();
            var mockLog = new Mock<ILog>();
            var controller = new ParkingSpaceAPIController(mockBL.Object, mockLog.Object);
            var expectedData = new List<ParkingSpaceModel>(); 
            var parkingZoneId = 1; 

            mockBL.Setup(bl => bl.ListParkingSpaceByIdAsync(parkingZoneId)).ReturnsAsync(expectedData);

            // Act
            var result = await controller.ListParkingSpaceById(parkingZoneId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualData = Assert.IsType<List<ParkingSpaceModel>>(okResult.Value);
            Assert.Equal(expectedData, actualData);
        }

        [Fact]
        public async Task AddParkingSpace_Returns_OkObjectResult_With_Success_True()
        {
            var mockBL = new Mock<IBL>();
            var mockLog = new Mock<ILog>();
            var controller = new ParkingSpaceAPIController(mockBL.Object, mockLog.Object);
            var modelToAdd = new ParkingSpaceModel(); 

            mockBL.Setup(bl => bl.AddParkingSpaceAsync(modelToAdd)).ReturnsAsync(true);

            // Act
            var result = await controller.AddParkingSpace(modelToAdd);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = okResult.Value; 
            var successProperty = response.GetType().GetProperty("success"); 
            var successValue = successProperty.GetValue(response); 

            Assert.IsType<bool>(successValue); 
            Assert.True((bool)successValue); 
        }
        [Fact]
        public async Task DeleteParkingSpace_Returns_OkObjectResult_With_Success_True()
        {
            // Arrange
            var mockBL = new Mock<IBL>();
            var mockLog = new Mock<ILog>();
            var controller = new ParkingSpaceAPIController(mockBL.Object, mockLog.Object);
            var titleToDelete = "A01"; 

            mockBL.Setup(bl => bl.DeleteParkingSpaceAsync(titleToDelete)).ReturnsAsync(true);

            // Act
            var result = await controller.DeleteParkingSpace(titleToDelete);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = okResult.Value; 
            var successProperty = response.GetType().GetProperty("success"); 
            var successValue = successProperty.GetValue(response); 

            Assert.IsType<bool>(successValue); 
            Assert.True((bool)successValue); 
        }


    }
}
