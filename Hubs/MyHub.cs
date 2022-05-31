using Microsoft.AspNetCore.SignalR;

namespace advanced_programming_2_server_side_exercise.Hubs
{
    public class MyHub : Hub
    {
        public async Task NewMessage()
        {
            await Clients.All.SendAsync("NewMessage");
        }

        public async Task NewContact()
        {
            await Clients.All.SendAsync("NewContact");
        }
    }
}
