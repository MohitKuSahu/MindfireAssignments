using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Models
{
    public class ReportModel
    {
        public string? ParkingZoneTitle { get; set; }
        public string? ParkingSpaceTitle { get; set; }

        public int ? TotalBookings { get; set; }

        public int? VehicleParked { get; set;}
    }
}
