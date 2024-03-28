using Xunit;
using Moq;
using ParkingManagement.Models;
using ParkingManagement.BL;
using ParkingManagement.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ParkingManagement.WebAPI.Controllers;

namespace ParkingManagement.WebAPI.Tests
{
    public class ReportAPIControllerTests
    {
        private readonly Mock<IBL> _mockBL;
        private readonly Mock<ILog> _mockLog;
        private readonly ReportAPIController _controller;

        public ReportAPIControllerTests()
        {
            _mockBL = new Mock<IBL>();
            _mockLog = new Mock<ILog>();
            _controller = new ReportAPIController(_mockBL.Object, _mockLog.Object);
        }

        [Fact]
        public async Task GetReportAsync_ValidDates_ReturnsOkResult()
        {
            // Arrange
            var startDate = "2024-03-01";
            var endDate = "2024-03-31";
            var expectedReportData = new List<ReportModel> { new() };
            _mockBL.Setup(b => b.GetParkingReportAsync(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
                   .ReturnsAsync(expectedReportData);

            // Act
            var result = await _controller.GetReportAsync(startDate, endDate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedReportData = Assert.IsAssignableFrom<List<ReportModel>>(okResult.Value);
            Assert.Equal(expectedReportData, returnedReportData);
        }

        [Fact]
        public async Task GetReportAsync_InvalidStartDate_ReturnsBadRequest()
        {
            // Arrange
            var startDate = "invalid-date";
            var endDate = "2024-01-31";

            // Act
            var result = await _controller.GetReportAsync(startDate, endDate);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Contains("Invalid start date format", badRequestResult.Value.ToString());
        }


    }
}
