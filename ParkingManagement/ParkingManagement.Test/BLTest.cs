using Moq;
using ParkingManagement.DAL;
using ParkingManagement.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingManagement.Models;

namespace ParkingManagement.Test
{
    public class BLTest
    {
        private readonly Mock<IDAL> _mockDAL;
        private readonly BL.BL _test;

        public BLTest()
        {
            _mockDAL = new Mock<IDAL>();
            _test = new BL.BL(_mockDAL.Object);
        }

        [Fact]
        public async Task AddParkingZoneAsync_ReturnsIdOfAddedZone()
        {
            // Arrange
            var model = new ParkingZoneModel { 
               ParkingZoneTitle = "Z"
            };
            var expectedId = 10;
            _mockDAL.Setup(d => d.AddParkingZoneAsync(model)).ReturnsAsync(expectedId);

            //Act
            var result = await _test.AddParkingZoneAsync(model);

            // Assert
            Assert.Equal(expectedId, result);
        }

        [Fact]
        public async Task ListParkingZoneAsync_ReturnsListOfParkingZones()
        {
            // Arrange
            var expectedList = new List<ParkingZoneModel>();
            _mockDAL.Setup(d => d.ListParkingZoneAsync()).ReturnsAsync(expectedList);
      

            // Act
            var result = await _test.ListParkingZoneAsync();

            // Assert
            Assert.Equal(expectedList, result);
        }

        [Fact]
        public async Task DeleteParkingZoneAsync_ReturnsTrue_WhenZoneExistsAndIsDeleted()
        {
            // Arrange
            var parkingZoneId = 1;
            _mockDAL.Setup(d => d.DeleteParkingZoneAsync(parkingZoneId)).ReturnsAsync(true);

            // Act
            var result = await _test.DeleteParkingZoneAsync(parkingZoneId);

            // Assert
            Assert.True(result);
        }


        [Fact]
        public async Task AddParkingSpaceAsync_ReturnsTrue_WhenSpaceIsAddedSuccessfully()
        {
            // Arrange
            var modelToAdd = new ParkingSpaceModel();
            _mockDAL.Setup(d => d.AddParkingSpaceAsync(modelToAdd)).ReturnsAsync(true);

            // Act
            var result = await _test.AddParkingSpaceAsync(modelToAdd);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ListParkingSpaceAsync_ReturnsListOfParkingSpaces()
        {
            // Arrange
            var expectedSpaces = new List<ParkingSpaceModel> { new ParkingSpaceModel() };
            _mockDAL.Setup(d => d.ListParkingSpaceAsync()).ReturnsAsync(expectedSpaces);

            // Act
            var result = await _test.ListParkingSpaceAsync();

            // Assert
            Assert.Equal(expectedSpaces, result);
        }

        [Fact]
        public async Task ListParkingSpaceByIdAsync_ReturnsListOfParkingSpaces()
        {
            // Arrange
            var parkingZoneId = 123;
            var expectedSpaces = new List<ParkingSpaceModel> { new ParkingSpaceModel() };
            _mockDAL.Setup(d => d.ListParkingSpaceByIdAsync(parkingZoneId)).ReturnsAsync(expectedSpaces);

            // Act
            var result = await _test.ListParkingSpaceByIdAsync(parkingZoneId);

            // Assert
            Assert.Equal(expectedSpaces, result);
        }

        [Fact]
        public async Task DeleteParkingSpaceAsync_ReturnsTrue_WhenSpaceIsDeletedSuccessfully()
        {
            // Arrange
            var titleToDelete = "Space1";
            _mockDAL.Setup(d => d.DeleteParkingSpaceAsync(titleToDelete)).ReturnsAsync(true);

            // Act
            var result = await _test.DeleteParkingSpaceAsync(titleToDelete);

            // Assert
            Assert.True(result);
        }


        [Fact]
        public async Task ListVehicleParkingAsync_ReturnsListOfVehicleParking()
        {
            // Arrange
            var expectedList = new List<VehicleParkingModel> { new VehicleParkingModel() };
            _mockDAL.Setup(d => d.ListVehicleParkingAsync()).ReturnsAsync(expectedList);

            // Act
            var result = await _test.ListVehicleParkingAsync();

            // Assert
            Assert.Equal(expectedList, result);
        }

        [Fact]
        public async Task ListVehicleParkingByIdAsync_ReturnsListOfVehicleParkingForGivenSpaceId()
        {
            // Arrange
            var parkingSpaceId = 123;
            var expectedList = new List<VehicleParkingModel> { new VehicleParkingModel()};
            _mockDAL.Setup(d => d.ListVehicleParkingByIdAsync(parkingSpaceId)).ReturnsAsync(expectedList);

            // Act
            var result = await _test.ListVehicleParkingByIdAsync(parkingSpaceId);

            // Assert
            Assert.Equal(expectedList, result);
        }

        [Fact]
        public async Task UpdateVehicleAsync_ReturnsTrue_WhenVehicleIsUpdatedSuccessfully()
        {
            // Arrange
            var model = new VehicleParkingModel();
            _mockDAL.Setup(d => d.UpdateVehicleAsync(model)).ReturnsAsync(true);

            // Act
            var result = await _test.UpdateVehicleAsync(model);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteVehicleParkingAsync_ReturnsTrue_WhenVehicleParkingIsDeletedSuccessfully()
        {
            // Arrange
            var parkingSpaceId = 123;
            _mockDAL.Setup(d => d.DeleteVehicleParkingAsync(parkingSpaceId)).ReturnsAsync(true);

            // Act
            var result = await _test.DeleteVehicleParkingAsync(parkingSpaceId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CheckIfEmailAlreadyExists_WhenEmailExists()
        {
            // Arrange
            var userEmail = "test@example.com";
            _mockDAL.Setup(d => d.CheckIfEmailAlreadyExists(userEmail)).ReturnsAsync(true);

            // Act
            var result = await _test.CheckIfEmailAlreadyExists(userEmail);

            // Assert
            Assert.True(result);
        }


        [Fact]
        public async Task InsertUser_ReturnsTrue_WhenUserIsInsertedSuccessfully()
        {
            // Arrange
            var user = new UserModel();
            _mockDAL.Setup(d => d.InsertUser(user)).ReturnsAsync(true);

            // Act
            var result = await _test.InsertUser(user);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CheckIfUserExists_ReturnsUserId_WhenUserExists()
        {
            // Arrange
            var user = new UserModel();
            var expectedUserId = 123;
            _mockDAL.Setup(d => d.CheckIfUserExists(user)).ReturnsAsync(expectedUserId);

            // Act
            var result = await _test.CheckIfUserExists(user);

            // Assert
            Assert.Equal(expectedUserId, result);
        }

        [Fact]
        public async Task CheckIfUserExists_ReturnsZero_WhenUserDoesNotExist()
        {
            // Arrange
            var user = new UserModel();
            _mockDAL.Setup(d => d.CheckIfUserExists(user)).ReturnsAsync(0);

            // Act
            var result = await _test.CheckIfUserExists(user);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task GetParkingZoneandGetParkingSpaceAsync_ReturnsListOfReportModels()
        {
            // Arrange
            var expectedList = new List<ReportModel> { new ReportModel() };
            _mockDAL.Setup(d => d.GetParkingZoneandGetParkingSpaceAsync()).ReturnsAsync(expectedList);

            // Act
            var result = await _test.GetParkingZoneandGetParkingSpaceAsync();

            // Assert
            Assert.Equal(expectedList, result);
        }

        [Fact]
        public async Task GetParkingReportAsync_ReturnsListOfReportModels()
        {
            // Arrange
            var startDate = new DateOnly(2024, 1, 1); 
            var endDate = new DateOnly(2024, 1, 31);
            var expectedList = new List<ReportModel> { new ReportModel() };
            _mockDAL.Setup(d => d.GetParkingReportAsync(startDate, endDate)).ReturnsAsync(expectedList);

            // Act
            var result = await _test.GetParkingReportAsync(startDate, endDate);

            // Assert
            Assert.Equal(expectedList, result);
        }



    }
}
