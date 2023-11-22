using Microsoft.AspNetCore.SignalR;

namespace DailyCashFlowControl.Hubs
{
    public class NotificationHub : Hub
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

        public async Task SendNotificationToClient(string clientId, string message)
        {
            if (_clientConnections.ContainsKey(clientId))
            {
                await Clients.Client(_clientConnections[clientId]).SendAsync("ReceiveNotification", message);
            }
        }
    }
}