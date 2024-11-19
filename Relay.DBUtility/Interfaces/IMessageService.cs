using System.Collections.Generic;
using System.Threading.Tasks;
using Relay.DTOs;
using Relay.Models;

namespace Relay.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с сообщениями.
    /// </summary>
    public interface IMessageService
    {
        /// <summary> Получение сообщения по ID. </summary>
        Task<Message> GetMessageAsync(Guid id);

        /// <summary> Создание нового сообщения. </summary>
        Task<Message> CreateMessageAsync(MessageCreateDto messageDto);

        /// <summary> Получение всех сообщений. </summary>
        Task<IEnumerable<Message>> GetAllMessagesAsync();
    }
}