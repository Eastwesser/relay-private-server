using System.Collections.Generic;
using System.Threading.Tasks;
using Relay.Models;
using Relay.Data;
using Microsoft.EntityFrameworkCore;

namespace Relay.Services
{
    public class ChatService : IChatService
    {
        private readonly ApplicationDbContext _context;

        public ChatService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Chat> GetChatAsync(int id) =>
            await _context.Chats
                .Include(c => c.Users)
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new KeyNotFoundException($"Chat with ID {id} not found.");

        public async Task<Chat> CreateChatAsync(Chat chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
            return chat;
        }

        public async Task<IEnumerable<Chat>> GetAllChatsAsync() =>
            await _context.Chats.Include(c => c.Users).ToListAsync();
    }
}
