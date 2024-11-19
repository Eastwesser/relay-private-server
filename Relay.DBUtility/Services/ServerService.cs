using System.Collections.Generic;
using System.Threading.Tasks;
using Relay.DBUtility.Models;
using Microsoft.EntityFrameworkCore;

namespace Relay.Services
{
    public class ServerService : IServerService
    {
        private readonly ApplicationDbContext _context;

        public ServerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServerDbModel?> GetServerAsync(Guid id) =>
            await _context.Servers.FindAsync(id) ?? throw new KeyNotFoundException($"Сервер с ID {id} не найден.");

        public async Task<ServerDbModel> CreateServerAsync(ServerDbModel server)
        {
            _context.Servers.Add(server);
            await _context.SaveChangesAsync();
            return server;
        }

        public async Task<IEnumerable<ServerDbModel>> GetAllServersAsync() =>
            await _context.Servers.ToListAsync();
    }
}
