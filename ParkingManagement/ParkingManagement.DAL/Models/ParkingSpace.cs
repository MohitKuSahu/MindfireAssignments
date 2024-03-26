using System;
using System.Collections.Generic;

namespace ParkingManagement.DAL.Models;

public partial class ParkingSpace
{
    public int ParkingSpaceId { get; set; }

    public string? ParkingSpaceTitle { get; set; }

    public int? ParkingZoneId { get; set; }

    public virtual ICollection<DailyBooking> DailyBookings { get; set; } = new List<DailyBooking>();

    public virtual ParkingZone? ParkingZone { get; set; }

    public virtual ICollection<VehicleParking> VehicleParkings { get; set; } = new List<VehicleParking>();
}
