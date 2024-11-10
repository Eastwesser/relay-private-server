namespace Relay.DTOs
{
    /// <summary>
    /// DTO для создания нового чата
    /// </summary>
    public class ChatCreateDto
    {
        /// <summary>
        /// Название чата
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Список идентификаторов участников чата
        /// </summary>
        public List<int> MemberIds { get; set; } = new();
    }
}
