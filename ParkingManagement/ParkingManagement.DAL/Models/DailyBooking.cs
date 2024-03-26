using System;
using System.Collections.Generic;

namespace ParkingManagement.DAL.Models;

public partial class DailyBooking
{
    public int Id { get; set; }

    public DateOnly? BookingDate { get; set; }

    public int? ParkingZoneId { get; set; }

    public int? ParkingSpaceId { get; set; }

    public int? TotalBookings { get; set; }

    public virtual ParkingSpace? ParkingSpace { get; set; }

    public virtual ParkingZone? ParkingZone { get; set; }
}
