using System.Collections.Generic;
using System.Threading.Tasks;
using Relay.Models;

namespace Relay.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с чатами.
    /// </summary>
    public interface IChatService
    {
        /// <summary> Получение чата по ID. </summary>
        Task<Chat> GetChatAsync(int id);

        /// <summary> Создание нового чата. </summary>
        Task<Chat> CreateChatAsync(Chat chat);

        /// <summary> Получение всех чатов. </summary>
        Task<IEnumerable<Chat>> GetAllChatsAsync();
    }
}
