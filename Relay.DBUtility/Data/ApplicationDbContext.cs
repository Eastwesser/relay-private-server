using Microsoft.EntityFrameworkCore;
using Relay.DBUtility.Models;
using Relay.Models;

/// <summary>
/// Контекст базы данных, определяющий доступ к таблицам пользователей, сообщений, чатов и серверов.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ApplicationDbContext"/> с заданными параметрами.
    /// </summary>
    /// <param name="options">Параметры конфигурации контекста базы данных.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    /// <summary>
    /// Таблица пользователей.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Таблица сообщений.
    /// </summary>
    public DbSet<Message> Messages { get; set; }

    /// <summary>
    /// Таблица чатов.
    /// </summary>
    public DbSet<Chat> Chats { get; set; }

    /// <summary>
    /// Таблица серверов.
    /// </summary>
    public DbSet<ServerDbModel> Servers { get; set; }
}
