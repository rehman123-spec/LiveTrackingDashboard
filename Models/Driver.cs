namespace LiveTrackingDashboard.Models
{
    public class Driver
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public DateTime LicenseExpiry { get; set; }
        public string Address { get; set; } = string.Empty;
        public string AssignedVehicleId { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public DriverStatus Status { get; set; } = DriverStatus.Active;
        public DateTime HireDate { get; set; }
        public string EmergencyContact { get; set; } = string.Empty;
        public string EmergencyPhone { get; set; } = string.Empty;
        public double TotalDistanceKm { get; set; }
        public int TotalTrips { get; set; }
        public DateTime LastTripDate { get; set; }
    }

    public enum DriverStatus
    {
        Active,
        Inactive,
        OnLeave,
        Suspended
    }
}
