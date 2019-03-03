using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Services;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub {
        private readonly SwishQRService service;
        public ChatHub(SwishQRService service) {
            this.service = service;
        }
        public async Task SendMessage(string swishNr, string amount, string message, string pin) {
            string img = await service.GetQRImage(swishNr, amount, message);
            await Clients.OthersInGroup(pin).SendAsync("ReceiveMessage", swishNr, amount, message, img);
            await Clients.Caller.SendAsync("messageSent", message);
        }

        public async Task AddToGroup(string groupName) {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.OthersInGroup(groupName).SendAsync("connectionAdded", Context.ConnectionId);
        }

        public async Task RemoveFromGroup(string groupName) {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.OthersInGroup(groupName).SendAsync("connectionRemoved", Context.ConnectionId);
        }
    }
}