using System.Collections.Generic;
using System.Threading.Tasks;
using Relay.DBUtility.Models; // Обновлено пространство имен

namespace Relay.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с серверами.
    /// </summary>
    public interface IServerService
    {
        /// <summary> Получение сервера по ID. </summary>
        Task<ServerDbModel?> GetServerAsync(Guid id);

        /// <summary> Создание нового сервера. </summary>
        Task<ServerDbModel> CreateServerAsync(ServerDbModel server); // Изменено на ServerDbModel

        /// <summary> Получение всех серверов. </summary>
        Task<IEnumerable<ServerDbModel>> GetAllServersAsync(); // Изменено на ServerDbModel
    }
}