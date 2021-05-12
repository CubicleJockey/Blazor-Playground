using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MsBlazorServerPlayGround.SignalR.Hubs
{
    public class BotChatHub : Hub
    {
        public static readonly string RECEIVE_MESSAGE = "ReceiveMessage";

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync(RECEIVE_MESSAGE, user, message);
        }
    }
}
