using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Models
{
    public class ParkingSpaceModel
    {
        public int ParkingSpaceId { get; set; }

        public string? ParkingSpaceTitle { get; set; }

        public int? ParkingZoneId { get; set; }
    }
}
