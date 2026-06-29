using LiveTrackingDashboard.Hubs;
using LiveTrackingDashboard.Models;
using Microsoft.AspNetCore.SignalR;

namespace LiveTrackingDashboard.Services
{
    public class TelemetryWorker : BackgroundService
    {
        private readonly IHubContext<TrackingHub> _hubContext;
        private readonly List<VehicleTelemetry> _vehicles;
        private readonly Random _random;

        public TelemetryWorker(IHubContext<TrackingHub> hubContext)
        {
            _hubContext = hubContext;
            _random = new Random();
            _vehicles = new List<VehicleTelemetry>
            {
                // South-Fleet vehicles (Karachi region)
                new VehicleTelemetry
                {
                    VehicleId = "TRK-01",
                    DriverName = "Asif Khan",
                    Latitude = 24.86,
                    Longitude = 67.00,
                    SpeedKph = 45.0,
                    Status = "In Transit",
                    Timestamp = DateTime.UtcNow,
                    Region = "South-Fleet"
                },
                new VehicleTelemetry
                {
                    VehicleId = "TRK-02",
                    DriverName = "Bilal Ahmed",
                    Latitude = 24.87,
                    Longitude = 67.01,
                    SpeedKph = 52.0,
                    Status = "In Transit",
                    Timestamp = DateTime.UtcNow,
                    Region = "South-Fleet"
                },
                // North-Fleet vehicles (Lahore region)
                new VehicleTelemetry
                {
                    VehicleId = "TRK-03",
                    DriverName = "Zain Ali",
                    Latitude = 31.52,
                    Longitude = 74.35,
                    SpeedKph = 48.0,
                    Status = "In Transit",
                    Timestamp = DateTime.UtcNow,
                    Region = "North-Fleet"
                },
                new VehicleTelemetry
                {
                    VehicleId = "TRK-04",
                    DriverName = "Dawood Shah",
                    Latitude = 31.53,
                    Longitude = 74.36,
                    SpeedKph = 55.0,
                    Status = "In Transit",
                    Timestamp = DateTime.UtcNow,
                    Region = "North-Fleet"
                }
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var vehicle in _vehicles)
                {
                    // Simulate dynamic driving behavior by altering coordinates slightly
                    vehicle.Latitude += (_random.NextDouble() - 0.5) * 0.001;
                    vehicle.Longitude += (_random.NextDouble() - 0.5) * 0.001;
                    
                    // Update speed with random variation
                    vehicle.SpeedKph = Math.Max(20, Math.Min(90, vehicle.SpeedKph + (_random.NextDouble() - 0.5) * 10));
                    
                    // Update timestamp
                    vehicle.Timestamp = DateTime.UtcNow;

                    // Business rule: Check for over-speed threshold
                    if (vehicle.SpeedKph > 75)
                    {
                        vehicle.IsAlertActive = true;
                        vehicle.AlertMessage = "CRITICAL: Over-speed Threshold Exceeded!";
                    }
                    else
                    {
                        vehicle.IsAlertActive = false;
                        vehicle.AlertMessage = string.Empty;
                    }

                    // Update telemetry history cache
                    TrackingHub.UpdateTelemetryHistory(vehicle);

                    // Push updated telemetry to specific regional group
                    await _hubContext.Clients.Group(vehicle.Region).SendAsync("ReceiveVehicleLocation", vehicle, stoppingToken);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
