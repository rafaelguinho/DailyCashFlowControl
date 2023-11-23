using DailyCashFlowControl.Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace DailyCashFlowControl.Hubs
{
    public class ConsolidatedResultNotificationHub : Hub, IConsolidatedResultNotification
    {
        private static Dictionary<string, string> _clientConnections = new Dictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            _clientConnections[Context.ConnectionId] = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _clientConnections.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendNotification(string clientId, string message)
        {
            if (_clientConnections.ContainsKey(clientId))
            {
                await Clients.Client(_clientConnections[clientId]).SendAsync("ReceiveNotification", message);
            }
        }
    }
}