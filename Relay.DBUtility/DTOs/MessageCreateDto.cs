namespace Relay.DTOs
{
    /// <summary>
    /// DTO для создания нового сообщения
    /// </summary>
    public class MessageCreateDto
    {
        /// <summary>
        /// Содержимое сообщения
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор пользователя, создавшего сообщение
        /// </summary>
        public int UserId { get; set; }
    }
}
