using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\fuseyboi.wav");
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            await Clients.All.SendAsync("ReceiveMessage", "Server", "Hello");
            await Clients.All.SendAsync("RecieveAudio", player);
            await Clients.All.SendAsync("ReceiveMessage", "Server", "World");
        }
    }
}