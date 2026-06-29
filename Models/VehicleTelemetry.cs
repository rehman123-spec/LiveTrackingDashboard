namespace LiveTrackingDashboard.Models
{
    public class VehicleTelemetry
    {
        public string VehicleId { get; set; } = string.Empty;
        public string DriverName { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double SpeedKph { get; set; }
        public string Status { get; set; } = "In Transit";
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Region { get; set; } = string.Empty;
        public bool IsAlertActive { get; set; }
        public string AlertMessage { get; set; } = string.Empty;
    }
}
