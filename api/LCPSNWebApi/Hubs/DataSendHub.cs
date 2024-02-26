using Microsoft.AspNetCore.SignalR;

namespace LCPSNWebApi.Hubs
{
    public class DataSendHub : Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("ReceiveMessage");
        }
    }
}