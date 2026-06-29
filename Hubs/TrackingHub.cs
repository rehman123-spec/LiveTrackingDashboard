using LiveTrackingDashboard.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace LiveTrackingDashboard.Hubs
{
    public class TrackingHub : Hub
    {
        // Static thread-safe memory store for historical telemetry (last 5 snapshots per vehicle)
        private static readonly ConcurrentDictionary<string, List<VehicleTelemetry>> _telemetryHistory 
            = new ConcurrentDictionary<string, List<VehicleTelemetry>>();

        private const int MaxHistoryEntries = 5;

        public async Task JoinFleetRegion(string regionName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, regionName);
        }

        public override async Task OnConnectedAsync()
        {
            // Push cached historical telemetry to the newly connected client
            await Clients.Caller.SendAsync("ReceiveHistoricalTelemetry", _telemetryHistory);
            await base.OnConnectedAsync();
        }

        // Method to update telemetry history (called by TelemetryWorker)
        public static void UpdateTelemetryHistory(VehicleTelemetry telemetry)
        {
            _telemetryHistory.AddOrUpdate(
                telemetry.VehicleId,
                new List<VehicleTelemetry> { telemetry },
                (key, existingList) =>
                {
                    existingList.Add(telemetry);
                    // Keep only last 5 entries
                    if (existingList.Count > MaxHistoryEntries)
                    {
                        existingList.RemoveAt(0);
                    }
                    return existingList;
                }
            );
        }
    }
}
