using Microsoft.EntityFrameworkCore;
using Relay.DBUtility;
using Relay.Models;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Relay.Server.Services
{
    public class UserUtilities
    {
        private readonly ApplicationDbContext _context;

        public UserUtilities(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Connect(Guid userId, string userName)
        {
            if (await IsUserExists(userName))
            {
                Log.Information("User {user} already exists.", userName);
                return;
            }

            _context.Users.Add(new User { Id = userId, Name = userName });
            await _context.SaveChangesAsync();
            Log.Information("User {user} connected.", userName);
        }

        public async Task<bool> IsUserExists(string userName) =>
            await _context.Users.AnyAsync(u => u.Name == userName);

        public async Task<int> GetNewUserId() => await _context.Users.CountAsync();
    }
}
