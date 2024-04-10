using LCPSNWebApi.Classes;
using Microsoft.AspNetCore.SignalR;

namespace LCPSNWebApi.Hubs
{
    public class ChatHub : Hub, IChatHub
    {
        private readonly IHubContext<ChatHub> hubContext;
        public ChatHub(IHubContext<ChatHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task SendMessage()
        {
            await hubContext.Clients.All.SendAsync("ReceiveChanges");
        }
    }
}