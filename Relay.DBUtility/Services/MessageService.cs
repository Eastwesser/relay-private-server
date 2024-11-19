using System.Collections.Generic;
using System.Threading.Tasks;
using Relay.Models;
using Relay.Data;
using Relay.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Relay.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MessageService> _logger;

        public MessageService(ApplicationDbContext context, ILogger<MessageService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Message> GetMessageAsync(Guid id)
        {
            _logger.LogInformation("Получаем сообщение с ID {MessageId}", id);
            return await _context.Messages.FindAsync(id)
                ?? throw new InvalidOperationException("Сообщение не найдено");
        }

        public async Task<Message> CreateMessageAsync(MessageCreateDto messageDto)
        {
            var message = new Message
            {
                Content = messageDto.Content,
                UserId = messageDto.UserId,
                Timestamp = DateTime.UtcNow
            };
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Сообщение с ID {MessageId}", message.Id);
            return message;
        }

        public async Task<IEnumerable<Message>> GetAllMessagesAsync() =>
            await _context.Messages.ToListAsync();
    }
}
