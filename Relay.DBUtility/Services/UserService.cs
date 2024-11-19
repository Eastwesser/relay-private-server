using System.Collections.Generic;
using System.Threading.Tasks;
using Relay.Models;
using Relay.DTOs;
using Relay.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Relay.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> RegisterAsync(UserCreateDto userDto)
        {
            if (userDto?.Name == null) throw new ArgumentNullException(nameof(userDto.Name), "Имя пользователя не должно быть null.");

            var user = new User { Name = userDto.Name };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Пользователь зарегистрировался под ID {UserId}", user.Id);
            return user;
        }

        public async Task<User?> GetUserAsync(Guid id) =>
            await _context.Users.FindAsync(id)
            ?? throw new KeyNotFoundException($"Пользователь с ID {id} не найден.");

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() =>
            await _context.Users.ToListAsync();
    }
}
