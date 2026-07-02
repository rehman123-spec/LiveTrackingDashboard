using LiveTrackingDashboard.Hubs;
using LiveTrackingDashboard.Services;
using LiveTrackingDashboard.Models;

var builder = WebApplication.CreateBuilder(args);

// Add SignalR services
builder.Services.AddSignalR();

// Add hosted service for telemetry simulation
builder.Services.AddHostedService<TelemetryWorker>();

// Add DriverService as singleton
builder.Services.AddSingleton<DriverService>();

// Add CORS for API access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");

// Enable static files
app.UseStaticFiles();

// Map SignalR Hub
app.MapHub<TrackingHub>("/trackingHub");

// Driver Management API Endpoints
var driverService = app.Services.GetRequiredService<DriverService>();

// Get all drivers
app.MapGet("/api/drivers", () => driverService.GetAllDrivers());

// Get driver by ID
app.MapGet("/api/drivers/{id}", (string id) => driverService.GetDriverById(id));

// Get drivers by region
app.MapGet("/api/drivers/region/{region}", (string region) => driverService.GetDriversByRegion(region));

// Add new driver
app.MapPost("/api/drivers", (Driver driver) => driverService.AddDriver(driver));

// Update driver
app.MapPut("/api/drivers/{id}", (string id, Driver driver) => driverService.UpdateDriver(id, driver));

// Delete driver
app.MapDelete("/api/drivers/{id}", (string id) => driverService.DeleteDriver(id));

// Assign vehicle to driver
app.MapPut("/api/drivers/{id}/assign-vehicle/{vehicleId}", (string id, string vehicleId) => driverService.AssignVehicle(id, vehicleId));

// Update driver status
app.MapPut("/api/drivers/{id}/status", (string id, DriverStatus status) => driverService.UpdateDriverStatus(id, status));

app.MapGet("/", () => "LiveTrackingDashboard is running. Open /index.html to view the dashboard.");

// Configure port from environment variable (for cloud deployment)
var port = Environment.GetEnvironmentVariable("PORT") ?? "5216";
app.Urls.Add($"http://0.0.0.0:{port}");

app.Run();
