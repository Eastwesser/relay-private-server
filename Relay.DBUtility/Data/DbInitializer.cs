using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Relay.Models;

namespace Relay.Data
{
    /// <summary>
    /// Класс для инициализации базы данных.
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Инициализирует базу данных, применяет миграции и добавляет начальные данные.
        /// </summary>
        public static async Task InitializeAsync(ApplicationDbContext context, ILogger logger)
        {
            logger.LogInformation("Начало миграции базы данных...");

            // Применение миграций
            await context.Database.MigrateAsync();

            // Проверка на наличие пользователей
            if (!await context.Users.AnyAsync())
            {
                logger.LogInformation("Создание пользователя 'admin'...");

                // Создание пользователя "admin" со статусом online
                await context.Users.AddAsync(new User
                {
                    Name = "admin",
                    Status = "online"
                });
                await context.SaveChangesAsync();
                logger.LogInformation("Администратор успешно добавлен.");
            }
            else
            {
                logger.LogInformation("База данных уже инициализирована.");
            }

            logger.LogInformation("Инициализация завершена.");
        }
    }
}
