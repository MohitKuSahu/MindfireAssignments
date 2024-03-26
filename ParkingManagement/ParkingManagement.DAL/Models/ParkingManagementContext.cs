using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ParkingManagement.DAL.Models;

public partial class ParkingManagementContext : DbContext
{
    public ParkingManagementContext()
    {
    }

    public ParkingManagementContext(DbContextOptions<ParkingManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DailyBooking> DailyBookings { get; set; }

    public virtual DbSet<ParkingSpace> ParkingSpaces { get; set; }

    public virtual DbSet<ParkingZone> ParkingZones { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VehicleParking> VehicleParkings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DailyBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__daily_bo__3213E83F546D7D45");

            entity.ToTable("daily_bookings");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookingDate).HasColumnName("booking_date");
            entity.Property(e => e.ParkingSpaceId).HasColumnName("parking_space_id");
            entity.Property(e => e.ParkingZoneId).HasColumnName("parking_zone_id");
            entity.Property(e => e.TotalBookings).HasColumnName("total_bookings");

            entity.HasOne(d => d.ParkingSpace).WithMany(p => p.DailyBookings)
                .HasForeignKey(d => d.ParkingSpaceId)
                .HasConstraintName("FK__daily_boo__parki__45F365D3");

            entity.HasOne(d => d.ParkingZone).WithMany(p => p.DailyBookings)
                .HasForeignKey(d => d.ParkingZoneId)
                .HasConstraintName("FK__daily_boo__parki__44FF419A");
        });

        modelBuilder.Entity<ParkingSpace>(entity =>
        {
            entity.HasKey(e => e.ParkingSpaceId).HasName("PK__parking___D4A7E9DF3CC5D6D1");

            entity.ToTable("parking_space");

            entity.HasIndex(e => e.ParkingSpaceTitle, "UQ__parking___C1C05BBDAF9BEC29").IsUnique();

            entity.Property(e => e.ParkingSpaceId).HasColumnName("parking_space_id");
            entity.Property(e => e.ParkingSpaceTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("parking_space_title");
            entity.Property(e => e.ParkingZoneId).HasColumnName("parking_zone_id");

            entity.HasOne(d => d.ParkingZone).WithMany(p => p.ParkingSpaces)
                .HasForeignKey(d => d.ParkingZoneId)
                .HasConstraintName("FK__parking_s__parki__3E52440B");
        });

        modelBuilder.Entity<ParkingZone>(entity =>
        {
            entity.HasKey(e => e.ParkingZoneId).HasName("PK__parking___4F1153D52941FF49");

            entity.ToTable("parking_zone");

            entity.HasIndex(e => e.ParkingZoneTitle, "UQ__parking___4FC26FF18D86C2FB").IsUnique();

            entity.Property(e => e.ParkingZoneId).HasColumnName("parking_zone_id");
            entity.Property(e => e.ParkingZoneTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("parking_zone_title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__user__B9BE370FE12F0815");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "UQ__user__AB6E6164D03B3B72").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<VehicleParking>(entity =>
        {
            entity.HasKey(e => e.VehicleParkingId).HasName("PK__vehicle___3E6979518ECB0C77");

            entity.ToTable("vehicle_parking");

            entity.Property(e => e.VehicleParkingId).HasColumnName("vehicle_parking_id");
            entity.Property(e => e.BookingDateTime)
                .HasColumnType("datetime")
                .HasColumnName("booking_date_time");
            entity.Property(e => e.ParkingSpaceId).HasColumnName("parking_space_id");
            entity.Property(e => e.ParkingZoneId).HasColumnName("parking_zone_id");
            entity.Property(e => e.ReleaseDateTime)
                .HasColumnType("datetime")
                .HasColumnName("release_date_time");
            entity.Property(e => e.VehicleRegistration)
                .HasMaxLength(50)
                .HasColumnName("vehicle_registration");

            entity.HasOne(d => d.ParkingSpace).WithMany(p => p.VehicleParkings)
                .HasForeignKey(d => d.ParkingSpaceId)
                .HasConstraintName("FK__vehicle_p__parki__4222D4EF");

            entity.HasOne(d => d.ParkingZone).WithMany(p => p.VehicleParkings)
                .HasForeignKey(d => d.ParkingZoneId)
                .HasConstraintName("FK__vehicle_p__parki__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
