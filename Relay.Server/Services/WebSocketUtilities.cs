using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Relay.Server.Services;

public class WebSocketUtilities : Hub
{
    [HubMethodName("SendMessage")]
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task SendMessageToCaller(string user, string message) =>
        await Clients.Caller.SendAsync("ReceiveMessage", user, message);

    public async Task SendMessageToGroup(string user, string message, string groupName) =>
        await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
}
