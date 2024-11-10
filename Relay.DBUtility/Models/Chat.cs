using System.Collections.Generic;

namespace Relay.Models
{
    /// <summary>
    /// Модель для представления чата.
    /// </summary>
    public class Chat
    {
        /// <summary> Идентификатор чата. </summary>
        public int Id { get; set; }

        /// <summary> Название чата. </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary> Признак того, является ли чат групповым. </summary>
        public bool IsGroupChat { get; set; }

        /// <summary> Список сообщений в чате. </summary>
        public List<Message> Messages { get; set; } = new List<Message>();

        /// <summary> Список пользователей в чате. </summary>
        public List<User> Users { get; set; } = new List<User>();
    }
}
