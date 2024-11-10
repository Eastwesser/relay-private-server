namespace Relay.DTOs
{
    /// <summary>
    /// DTO для создания нового сервера
    /// </summary>
    public class ServerCreateDto
    {
        /// <summary> Название сервера </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary> IP-адрес или доменное имя сервера </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary> Порт для подключения к серверу </summary>
        public int Port { get; set; }

        /// <summary> Пароль для доступа к серверу (опционально) </summary>
        public string? Password { get; set; }
    }
}
