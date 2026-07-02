# LiveTrackingDashboard

A real-time enterprise-level vehicle tracking dashboard built with .NET 10, SignalR, and Leaflet.js.

## Features

- **Real-time Vehicle Tracking**: Live telemetry updates using SignalR WebSockets
- **Regional Fleet Segregation**: Separate tracking for South-Fleet (Karachi) and North-Fleet (Lahore)
- **Interactive Map**: Leaflet.js integration with OpenStreetMap tiles
- **Dynamic Markers**: Smooth vehicle movement with trailing paths
- **Alert System**: Automatic speed threshold alerts (>75 km/h) with visual notifications
- **Historical Data**: In-memory cache of last 5 telemetry snapshots per vehicle
- **Responsive Design**: Two-column layout with dark blue theme

## Technology Stack

- **Backend**: .NET 10, ASP.NET Core
- **Real-time Communication**: SignalR
- **Frontend**: HTML5, JavaScript, Leaflet.js
- **Map Provider**: OpenStreetMap

## Project Structure

```
LiveTrackingDashboard/
├── Models/
│   └── VehicleTelemetry.cs    # Vehicle telemetry data model
├── Hubs/
│   └── TrackingHub.cs           # SignalR hub with group management
├── Services/
│   └── TelemetryWorker.cs       # Background service for telemetry simulation
├── wwwroot/
│   └── index.html               # Frontend dashboard UI
└── Program.cs                   # Application startup configuration
```

## Getting Started

### Prerequisites

- .NET 10 SDK
- A web browser

### Installation

1. Clone the repository
2. Navigate to the project directory
3. Restore dependencies:
   ```bash
   dotnet restore
   ```

### Running the Application

1. Build the project:
   ```bash
   dotnet build
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

3. Open your browser and navigate to:
   ```
   http://localhost:5216/index.html
   ```

## Vehicle Fleet

### South-Fleet (Karachi Region)
- **TRK-01**: Driver - Asif Khan
- **TRK-02**: Driver - Bilal Ahmed

### North-Fleet (Lahore Region)
- **TRK-03**: Driver - Zain Ali
- **TRK-04**: Driver - Dawood Shah

## Features in Detail

### Regional Selection
- Switch between South-Fleet and North-Fleet using the region selector buttons
- Map automatically re-centers to the selected region
- Only vehicles from the selected region are displayed

### Alert System
- Vehicles exceeding 75 km/h trigger an alert
- Alert indicators appear on vehicle cards with glowing red borders
- Toast notifications display alert messages in real-time

### Historical Data
- New connections receive historical telemetry data
- Last 5 position snapshots per vehicle are cached
- Enables immediate visualization of vehicle paths on connection

## Configuration

The application runs on `http://localhost:5216` by default. This can be modified in `Properties/launchSettings.json`.

## License

This project is open source and available for educational and commercial use.
