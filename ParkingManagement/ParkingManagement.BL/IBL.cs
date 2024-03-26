using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingManagement.Models;

namespace ParkingManagement.BL
{
    public interface IBL
    {
        Task<int> AddParkingZoneAsync(ParkingZoneModel model);
        Task<List<ParkingZoneModel>> ListParkingZoneAsync();
        Task<bool> DeleteParkingZoneAsync(int parkingZoneId);

        Task<bool> AddParkingSpaceAsync(ParkingSpaceModel model);
        Task<List<ParkingSpaceModel>> ListParkingSpaceAsync();

        Task<List<ParkingSpaceModel>> ListParkingSpaceByIdAsync(int ParkingZoneId);
        Task<bool> DeleteParkingSpaceAsync(string title);

        Task<List<VehicleParkingModel>> ListVehicleParkingAsync();

        Task<List<VehicleParkingModel>> ListVehicleParkingByIdAsync(int parkingSpaceId);

        Task<bool> VehicleAsync(VehicleParkingModel model);

        Task<bool> DeleteVehicleParkingAsync(int parkingSpaceId);

        Task<int> CheckIfUserExists(UserModel user);
        Task<List<ReportModel>> GetParkingReportAsync(DateOnly startDate, DateOnly endDate);
    }
}
