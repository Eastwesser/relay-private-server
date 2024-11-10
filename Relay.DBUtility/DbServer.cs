using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Relay.DBUtility.Models;
using Relay.Models;

namespace Relay.DBUtility
{
    /// <summary>
    /// Контекст базы данных для использования с Entity Framework.
    /// </summary>
    public class DbServer : DbContext
    {
        private readonly ILogger<DbServer> _logger;

        public DbServer(DbContextOptions<DbServer> options, ILogger<DbServer> logger) : base(options)
        {
            _logger = logger;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ServerDbModel> Servers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql();
            }
        }
    }
}
