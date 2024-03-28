using ParkingManagement.DAL;
using ParkingManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.BL
{
    public class BL : IBL
    {
        private IDAL _dataService;

        public BL(IDAL dataAccess)
        {
            _dataService = dataAccess;
        }


        public async Task<int> AddParkingZoneAsync(ParkingZoneModel model)
        {
            return await _dataService.AddParkingZoneAsync(model);
        }

        public async Task<List<ParkingZoneModel>> ListParkingZoneAsync()
        {
            return await _dataService.ListParkingZoneAsync();
        }

        public async Task<bool> DeleteParkingZoneAsync(int parkingZoneId)
        {
            return await _dataService.DeleteParkingZoneAsync(parkingZoneId);
        }

        public async Task<bool> AddParkingSpaceAsync(ParkingSpaceModel model)
        {
            return await _dataService.AddParkingSpaceAsync(model);
        }

        public async Task<List<ParkingSpaceModel>> ListParkingSpaceAsync()
        {
            return await _dataService.ListParkingSpaceAsync();
        }

        public async Task<List<ParkingSpaceModel>> ListParkingSpaceByIdAsync(int ParkingZoneId)
        {
            return await _dataService.ListParkingSpaceByIdAsync(ParkingZoneId);
        }
        public async Task<bool> DeleteParkingSpaceAsync(string title)
        {
            return await _dataService.DeleteParkingSpaceAsync(title);
        }


        public async Task<List<VehicleParkingModel>> ListVehicleParkingAsync()
        {
            return await _dataService.ListVehicleParkingAsync();
        }

        public async Task<List<VehicleParkingModel>> ListVehicleParkingByIdAsync(int parkingSpaceId)
        {
            return await _dataService.ListVehicleParkingByIdAsync(parkingSpaceId);
        }
        public async Task<bool> UpdateVehicleAsync(VehicleParkingModel model)
        {
            return await _dataService.UpdateVehicleAsync(model);
        }

        public async Task<bool> DeleteVehicleParkingAsync(int parkingSpaceId)
        {
            return await _dataService.DeleteVehicleParkingAsync(parkingSpaceId);
        }

        public Task<bool> CheckIfEmailAlreadyExists(string userEmail)
        {
            return _dataService.CheckIfEmailAlreadyExists(userEmail);
        }
        public Task<bool> InsertUser(UserModel user)
        {
            return _dataService.InsertUser(user);
        }

        public async Task<int> CheckIfUserExists(UserModel user)
        {
            return await _dataService.CheckIfUserExists(user);
        }
        public async Task<List<ReportModel>> GetParkingReportAsync(DateOnly startDate, DateOnly endDate)
        {
            return await _dataService.GetParkingReportAsync(startDate, endDate);
        }
    }
}
