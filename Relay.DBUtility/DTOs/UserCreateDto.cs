namespace Relay.DTOs
{
    /// <summary>
    /// DTO для регистрации нового пользователя
    /// </summary>
    public class UserCreateDto
    {
        /// <summary> Имя пользователя. </summary>
        public string? Name { get; set; }

        /// <summary> Статус пользователя. </summary>
        public string Status { get; set; } = "online";
    }
}
