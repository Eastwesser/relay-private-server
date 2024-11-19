using Microsoft.AspNetCore.SignalR;
using Relay.DBUtility;
using Relay.Models;
using System;
using System.Threading.Tasks;

namespace Relay.Server.Services
{
    public class WebSocketUtilities : Hub
    {
        private readonly ApplicationDbContext _context;

        public WebSocketUtilities(ApplicationDbContext context)
        {
            _context = context;
        }

        [HubMethodName("SendMessage")]
        public async Task SendMessage(Guid userId, string messageText)
        {
            var message = new Message
            {
                UserId = userId,
                Content = messageText,
                Timestamp = DateTime.Now
            };

            // Сохраняем сообщение в базе данных
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            // Отправляем сообщение всем подключенным клиентам
            await Clients.All.SendAsync("ReceiveMessage", userId, messageText);
        }

        public async Task SendMessageToCaller(Guid userId, string messageText) =>
            await Clients.Caller.SendAsync("ReceiveMessage", userId, messageText);

        public async Task SendMessageToGroup(Guid userId, string messageText, string groupName) =>
            await Clients.Group(groupName).SendAsync("ReceiveMessage", userId, messageText);
    }
}
