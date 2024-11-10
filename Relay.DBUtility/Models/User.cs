namespace Relay.Models
{
    /// <summary>
    /// Модель для представления пользователя.
    /// </summary>
    public class User
    {
        /// <summary> Идентификатор пользователя. </summary>
        public int Id { get; set; }

        /// <summary> Имя пользователя. </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary> Ссылка на аватар пользователя. </summary>
        public string? AvatarUrl { get; set; } = null;

        /// <summary> Статус пользователя (online, offline и т.д.). </summary>
        public string Status { get; set; } = "online";

        /// <summary> Язык интерфейса пользователя по умолчанию. </summary>
        public string Language { get; set; } = "ru";
    }
}
