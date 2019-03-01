using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub {
        public async Task SendMessage(string swishNr, int amount, string message, string pin) {
            await Clients.OthersInGroup(pin).SendAsync("ReceiveMessage", swishNr, amount, message);
            await Clients.Caller.SendAsync("messageSent", message);
        }

        public async Task AddToGroup(string groupName) {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.OthersInGroup(groupName).SendAsync("connectionAdded", Context.ConnectionId);
        }
    }
}