using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParkingManagement.DAL.Models;
using ParkingManagement.Models;

namespace ParkingManagement.DAL
{
    public class DAL : IDAL
    {
        private readonly ParkingManagementContext _parkingManagementContext;

        public DAL(ParkingManagementContext parkingManagementContext)
        {
            this._parkingManagementContext = parkingManagementContext;
        }

        public async Task<int> AddParkingZoneAsync(ParkingZoneModel model)
        {
            var existingModel = _parkingManagementContext.ParkingZones.FirstOrDefault(s => s.ParkingZoneTitle == model.ParkingZoneTitle);
            if (existingModel != null)
            {
                return existingModel.ParkingZoneId;
            }

            else
            {
                var newModel = new ParkingZone()
                {
                    ParkingZoneTitle = model.ParkingZoneTitle

                };
                _parkingManagementContext.ParkingZones.Add(newModel);
                await _parkingManagementContext.SaveChangesAsync();
                return newModel.ParkingZoneId;
            }

        }

        public async Task<List<ParkingZoneModel>> ListParkingZoneAsync()
        {
            var result = await _parkingManagementContext.ParkingZones
                              .OrderBy(zone => zone.ParkingZoneTitle)
                              .ToListAsync();

            return result.Select(zone => new ParkingZoneModel
            {
                ParkingZoneId = zone.ParkingZoneId,
                ParkingZoneTitle = zone.ParkingZoneTitle
            }).ToList();

        }

        public async Task<bool> DeleteParkingZoneAsync(int parkingZoneId)
        {

            var zoneToDelete = await _parkingManagementContext.ParkingZones.FindAsync(parkingZoneId);
            if (zoneToDelete == null)
                return false;

            _parkingManagementContext.ParkingZones.Remove(zoneToDelete);
            await _parkingManagementContext.SaveChangesAsync();
            return true;

        }

        public async Task<bool> AddParkingSpaceAsync(ParkingSpaceModel model)
        {
            var existingModel = _parkingManagementContext.ParkingSpaces.FirstOrDefault(s => s.ParkingSpaceTitle == model.ParkingSpaceTitle);
            if (existingModel != null)
            {
                return false;
            }

            var newModel = new ParkingSpace()
            {
                ParkingSpaceTitle = model.ParkingSpaceTitle,
                ParkingZoneId = model.ParkingZoneId,
            };
            _parkingManagementContext.ParkingSpaces.Add(newModel);
            await _parkingManagementContext.SaveChangesAsync();
            return true;

        }

        public async Task<List<ParkingSpaceModel>> ListParkingSpaceAsync()
        {
            var result = await _parkingManagementContext.ParkingSpaces.ToListAsync();

            return result.Select(zone => new ParkingSpaceModel
            {
                ParkingZoneId = zone.ParkingZoneId,
                ParkingSpaceTitle = zone.ParkingSpaceTitle,
                ParkingSpaceId = zone.ParkingSpaceId,
            }).ToList();
        }


        public async Task<List<ParkingSpaceModel>> ListParkingSpaceByIdAsync(int ParkingZoneId)
        {
            var parkingSpaces = await _parkingManagementContext.ParkingSpaces
           .Where(p => p.ParkingZoneId == ParkingZoneId)
           .OrderBy(zone => zone.ParkingSpaceTitle)
           .Select(zone => new ParkingSpaceModel
           {
               ParkingZoneId = zone.ParkingZoneId,
               ParkingSpaceTitle = zone.ParkingSpaceTitle,
               ParkingSpaceId = zone.ParkingSpaceId,
           }).ToListAsync();

            return parkingSpaces;
        }


        public async Task<bool> DeleteParkingSpaceAsync(string title)
        {
            var spaceToDelete = await _parkingManagementContext.ParkingSpaces.FirstOrDefaultAsync(s => s.ParkingSpaceTitle == title);
            if (spaceToDelete == null)
                return false;
            else
            {
               await DeleteVehicleParkingAsync(spaceToDelete.ParkingSpaceId);
                _parkingManagementContext.ParkingSpaces.Remove(spaceToDelete);
                await _parkingManagementContext.SaveChangesAsync();
                return true;
            }
        }


        public async Task<List<VehicleParkingModel>> ListVehicleParkingAsync()
        {
            var result = await _parkingManagementContext.VehicleParkings.ToListAsync();

            return result.Select(zone => new VehicleParkingModel
            {
                VehicleParkingId = zone.VehicleParkingId,
                ParkingZoneId = zone.ParkingZoneId,
                ParkingSpaceId = zone.ParkingSpaceId,
                BookingDateTime = zone.BookingDateTime,
                ReleaseDateTime = zone.ReleaseDateTime,
                VehicleRegistration = zone.VehicleRegistration,
            }).ToList();
        }

        public async Task<List<VehicleParkingModel>> ListVehicleParkingByIdAsync(int parkingSpaceId)
        {
            var vehicleParking = await _parkingManagementContext.VehicleParkings
          .Where(p => p.ParkingSpaceId == parkingSpaceId)
          .Select(zone => new VehicleParkingModel
          {
              VehicleParkingId = zone.VehicleParkingId,
              ParkingZoneId = zone.ParkingZoneId,
              ParkingSpaceId = zone.ParkingSpaceId,
              BookingDateTime = zone.BookingDateTime,
              ReleaseDateTime = zone.ReleaseDateTime,
              VehicleRegistration = zone.VehicleRegistration,

          }).ToListAsync();

            return vehicleParking;
        }

        public async Task<bool> VehicleAsync(VehicleParkingModel model)
        {

            var result = await _parkingManagementContext.VehicleParkings
       .FirstOrDefaultAsync(v => v.ParkingZoneId == model.ParkingZoneId && v.ParkingSpaceId == model.ParkingSpaceId);
            if (result != null)
            {
                result.BookingDateTime = model.BookingDateTime;
                result.ReleaseDateTime = model.ReleaseDateTime;
                result.VehicleRegistration = model.VehicleRegistration;
                await _parkingManagementContext.SaveChangesAsync();
                var today = DateOnly.FromDateTime(DateTime.Now);

                await AddBookings(model.ParkingSpaceId, model.ParkingZoneId, today);

                return true;

            }
            else
            {
                var newModel = new VehicleParking()
                {
                    ParkingZoneId = model.ParkingZoneId,
                    ParkingSpaceId = model.ParkingSpaceId,
                    BookingDateTime = DateTime.Now,
                    ReleaseDateTime = DateTime.Now,
                    VehicleRegistration = model.VehicleRegistration,

                };
                _parkingManagementContext.VehicleParkings.Add(newModel);
                await _parkingManagementContext.SaveChangesAsync();

                var today = DateOnly.FromDateTime(DateTime.Now);
                await AddBookings(model.ParkingSpaceId, model.ParkingZoneId, today);
                return true;
            }
        }


        public async Task<bool> DeleteVehicleParkingAsync(int parkingSpaceId)
        {

            var spaceToDelete = await _parkingManagementContext.VehicleParkings.FirstOrDefaultAsync(s => s.ParkingSpaceId == parkingSpaceId);
            if (spaceToDelete == null)
                return false;

            var today = DateOnly.FromDateTime(DateTime.Now);
            await AddBookings(spaceToDelete.ParkingSpaceId, spaceToDelete.ParkingZoneId, today);
            _parkingManagementContext.VehicleParkings.Remove(spaceToDelete);
            await _parkingManagementContext.SaveChangesAsync();
            return true;

        }

        public async Task AddBookings(int? parkingSpaceId, int? parkingZoneId, DateOnly today)
        {
            var existingBooking = await _parkingManagementContext.DailyBookings
                .FirstOrDefaultAsync(b => b.ParkingZoneId == parkingZoneId &&
                                            b.ParkingSpaceId == parkingSpaceId &&
                                            b.BookingDate == today);

            if (existingBooking != null)
            {
                existingBooking.TotalBookings++;
            }
            else
            {
                var newBooking = new DailyBooking()
                {
                    ParkingZoneId = parkingZoneId,
                    ParkingSpaceId = parkingSpaceId,
                    TotalBookings = 1,
                    BookingDate = today
                };
                _parkingManagementContext.DailyBookings.Add(newBooking);
            }

            await _parkingManagementContext.SaveChangesAsync();
        }


        public async Task<int> CheckIfUserExists(UserModel user)
        {
            int userId = -1;
          
                var users = await _parkingManagementContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

                if (users != null && users.Password == user.Password && users.Type==user.Type)
                {
                     
                    userId = users.UserId;
                }
            
            return userId;
        }
        public async Task<List<ReportModel>> GetParkingReportAsync(DateOnly startDate, DateOnly endDate)
        {
            var parkingSpaces = await _parkingManagementContext.ParkingSpaces.ToListAsync();
            var report = new List<ReportModel>();
            var currentTime = DateTime.Now;

            foreach (var ps in parkingSpaces)
            {
                var zone = await _parkingManagementContext.ParkingZones.FindAsync(ps.ParkingZoneId);
                if (zone == null)
                    continue;

                var bookings = await _parkingManagementContext.DailyBookings
                    .Where(db => db.ParkingSpaceId == ps.ParkingSpaceId &&
                                 db.BookingDate >= startDate &&
                                 db.BookingDate <= endDate)
                    .ToListAsync();

                var totalBookings = bookings.Sum(db => db.TotalBookings);

                var parkedSpaces = from vp in _parkingManagementContext.VehicleParkings
                                   where vp.BookingDateTime != null &&
                                         (vp.ReleaseDateTime == null || vp.ReleaseDateTime > currentTime)
                                   select vp.ParkingSpaceId;

                var isSpaceParked = await _parkingManagementContext.VehicleParkings
      .AnyAsync(vp => vp.ParkingSpaceId == ps.ParkingSpaceId &&
                      vp.BookingDateTime != null &&
                      ((vp.ReleaseDateTime == null && currentTime >= vp.BookingDateTime) ||
                      (vp.ReleaseDateTime != null && vp.BookingDateTime <= currentTime && currentTime < vp.ReleaseDateTime)));



                report.Add(new ReportModel
                {
                    ParkingZoneTitle = zone.ParkingZoneTitle,
                    ParkingSpaceTitle = ps.ParkingSpaceTitle,
                    TotalBookings = totalBookings,
                    VehicleParked = isSpaceParked ? 1 : 0
                });
            }

            return report;
        }

    }
}
