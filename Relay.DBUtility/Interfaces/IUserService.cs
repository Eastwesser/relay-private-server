using System.Collections.Generic;
using System.Threading.Tasks;
using Relay.Models;
using Relay.DTOs;

namespace Relay.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с пользователями.
    /// </summary>
    public interface IUserService
    {
        /// <summary> Регистрация нового пользователя. </summary>
        Task<User> RegisterAsync(UserCreateDto userDto);

        /// <summary> Получение пользователя по ID. </summary>
        Task<User?> GetUserAsync(int id);

        /// <summary> Создание нового пользователя. </summary>
        Task<User> CreateUserAsync(User user);

        /// <summary> Получение всех пользователей. </summary>
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
