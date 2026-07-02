using LiveTrackingDashboard.Models;
using System.Collections.Concurrent;

namespace LiveTrackingDashboard.Services
{
    public class DriverService
    {
        private static readonly ConcurrentDictionary<string, Driver> _drivers = new();

        static DriverService()
        {
            // Initialize with existing drivers
            _drivers.TryAdd("DRV-001", new Driver
            {
                Id = "DRV-001",
                Name = "Asif Khan",
                Phone = "+92-300-1234567",
                Email = "asif.khan@fleet.com",
                LicenseNumber = "LIC-KHI-001",
                LicenseExpiry = new DateTime(2026, 12, 31),
                Address = "Karachi, Pakistan",
                AssignedVehicleId = "TRK-01",
                Region = "South-Fleet",
                Status = DriverStatus.Active,
                HireDate = new DateTime(2023, 1, 15),
                EmergencyContact = "Fatima Khan",
                EmergencyPhone = "+92-300-1234568",
                TotalDistanceKm = 15000,
                TotalTrips = 245,
                LastTripDate = DateTime.UtcNow.AddDays(-1)
            });

            _drivers.TryAdd("DRV-002", new Driver
            {
                Id = "DRV-002",
                Name = "Bilal Ahmed",
                Phone = "+92-301-2345678",
                Email = "bilal.ahmed@fleet.com",
                LicenseNumber = "LIC-KHI-002",
                LicenseExpiry = new DateTime(2025, 11, 30),
                Address = "Karachi, Pakistan",
                AssignedVehicleId = "TRK-02",
                Region = "South-Fleet",
                Status = DriverStatus.Active,
                HireDate = new DateTime(2023, 3, 20),
                EmergencyContact = "Ayesha Ahmed",
                EmergencyPhone = "+92-301-2345679",
                TotalDistanceKm = 12000,
                TotalTrips = 198,
                LastTripDate = DateTime.UtcNow
            });

            _drivers.TryAdd("DRV-003", new Driver
            {
                Id = "DRV-003",
                Name = "Zain Ali",
                Phone = "+92-302-3456789",
                Email = "zain.ali@fleet.com",
                LicenseNumber = "LIC-LHE-001",
                LicenseExpiry = new DateTime(2026, 6, 15),
                Address = "Lahore, Pakistan",
                AssignedVehicleId = "TRK-03",
                Region = "North-Fleet",
                Status = DriverStatus.Active,
                HireDate = new DateTime(2023, 2, 10),
                EmergencyContact = "Sara Ali",
                EmergencyPhone = "+92-302-3456790",
                TotalDistanceKm = 18000,
                TotalTrips = 312,
                LastTripDate = DateTime.UtcNow.AddDays(-2)
            });

            _drivers.TryAdd("DRV-004", new Driver
            {
                Id = "DRV-004",
                Name = "Dawood Shah",
                Phone = "+92-303-4567890",
                Email = "dawood.shah@fleet.com",
                LicenseNumber = "LIC-LHE-002",
                LicenseExpiry = new DateTime(2025, 10, 20),
                Address = "Lahore, Pakistan",
                AssignedVehicleId = "TRK-04",
                Region = "North-Fleet",
                Status = DriverStatus.Active,
                HireDate = new DateTime(2023, 4, 5),
                EmergencyContact = "Zainab Shah",
                EmergencyPhone = "+92-303-4567891",
                TotalDistanceKm = 14000,
                TotalTrips = 220,
                LastTripDate = DateTime.UtcNow.AddDays(-3)
            });
        }

        public IEnumerable<Driver> GetAllDrivers()
        {
            return _drivers.Values;
        }

        public Driver? GetDriverById(string id)
        {
            _drivers.TryGetValue(id, out var driver);
            return driver;
        }

        public IEnumerable<Driver> GetDriversByRegion(string region)
        {
            return _drivers.Values.Where(d => d.Region == region);
        }

        public Driver AddDriver(Driver driver)
        {
            if (string.IsNullOrEmpty(driver.Id))
            {
                driver.Id = "DRV-" + (_drivers.Count + 1).ToString("D3");
            }

            _drivers.TryAdd(driver.Id, driver);
            return driver;
        }

        public bool UpdateDriver(string id, Driver driver)
        {
            if (!_drivers.ContainsKey(id))
                return false;

            driver.Id = id;
            _drivers.TryUpdate(id, driver, _drivers[id]);
            return true;
        }

        public bool DeleteDriver(string id)
        {
            return _drivers.TryRemove(id, out _);
        }

        public bool AssignVehicle(string driverId, string vehicleId)
        {
            if (!_drivers.TryGetValue(driverId, out var driver))
                return false;

            driver.AssignedVehicleId = vehicleId;
            return true;
        }

        public bool UpdateDriverStatus(string driverId, DriverStatus status)
        {
            if (!_drivers.TryGetValue(driverId, out var driver))
                return false;

            driver.Status = status;
            return true;
        }
    }
}
