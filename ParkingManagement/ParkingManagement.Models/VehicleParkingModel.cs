using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Models
{
    public class VehicleParkingModel
    {
        public int VehicleParkingId { get; set; }

        public int? ParkingZoneId { get; set; }

        public int? ParkingSpaceId { get; set; }

        public DateTime? BookingDateTime { get; set; }

        public DateTime? ReleaseDateTime { get; set; }

        public string? VehicleRegistration { get; set; }
    }
}
