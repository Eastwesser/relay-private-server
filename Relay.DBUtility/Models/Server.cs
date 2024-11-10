namespace Relay.DBUtility.Models
{
    /// <summary>
    /// Модель для представления сервера.
    /// </summary>
    public class ServerDbModel
    {
        /// <summary> Идентификатор сервера. </summary>
        public int Id { get; set; }

        /// <summary> Название сервера. </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary> IP-адрес или домен сервера. </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary> Порт, используемый для подключения к серверу. </summary>
        public int Port { get; set; }

        /// <summary> Пароль для доступа к серверу. </summary>
        public string? Password { get; set; }
    }
}
