using LiveTrackingDashboard.Hubs;
using LiveTrackingDashboard.Services;

var builder = WebApplication.CreateBuilder(args);

// Add SignalR services
builder.Services.AddSignalR();

// Add hosted service for telemetry simulation
builder.Services.AddHostedService<TelemetryWorker>();

var app = builder.Build();

// Enable static files
app.UseStaticFiles();

// Map SignalR Hub
app.MapHub<TrackingHub>("/trackingHub");

app.MapGet("/", () => "LiveTrackingDashboard is running. Open /index.html to view the dashboard.");

// Configure Kestrel to listen on all interfaces for mobile access
app.Urls.Add("http://0.0.0.0:5216");

app.Run();
