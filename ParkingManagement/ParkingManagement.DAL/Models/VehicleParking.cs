using System;
using System.Collections.Generic;

namespace ParkingManagement.DAL.Models;

public partial class VehicleParking
{
    public int VehicleParkingId { get; set; }

    public int? ParkingZoneId { get; set; }

    public int? ParkingSpaceId { get; set; }

    public DateTime? BookingDateTime { get; set; }

    public DateTime? ReleaseDateTime { get; set; }

    public string? VehicleRegistration { get; set; }

    public virtual ParkingSpace? ParkingSpace { get; set; }

    public virtual ParkingZone? ParkingZone { get; set; }
}
