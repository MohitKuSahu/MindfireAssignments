using System;
using System.Collections.Generic;

namespace ParkingManagement.DAL.Models;

public partial class ParkingZone
{
    public int ParkingZoneId { get; set; }

    public string? ParkingZoneTitle { get; set; }

    public virtual ICollection<DailyBooking> DailyBookings { get; set; } = new List<DailyBooking>();

    public virtual ICollection<ParkingSpace> ParkingSpaces { get; set; } = new List<ParkingSpace>();

    public virtual ICollection<VehicleParking> VehicleParkings { get; set; } = new List<VehicleParking>();
}
