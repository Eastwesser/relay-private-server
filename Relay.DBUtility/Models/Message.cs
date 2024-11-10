using System;

namespace Relay.Models
{
    /// <summary>
    /// Модель для представления сообщения.
    /// </summary>
    public class Message
    {
        /// <summary> Идентификатор сообщения. </summary>
        public Guid Id { get; set; }

        /// <summary> Дата и время отправки сообщения. </summary>
        public DateTime Timestamp { get; set; }

        /// <summary> Содержимое сообщения. </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary> Идентификатор пользователя, который отправил сообщение. </summary>
        public int UserId { get; set; }
    }
}
