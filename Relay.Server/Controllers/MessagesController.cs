using Microsoft.AspNetCore.Mvc;
using Relay.Services;
using Relay.Models;
using Relay.DTOs;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Threading.Tasks;

namespace Relay.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly ILogger<MessagesController> _logger;

    public MessagesController(IMessageService messageService, ILogger<MessagesController> logger)
    {
        _messageService = messageService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMessages()
    {
        var culture = HttpContext.Items["RequestCulture"] as CultureInfo ?? new CultureInfo("ru");
        _logger.LogInformation("Получение всех сообщений на языке: {Culture}", culture.Name);

        var messages = await _messageService.GetAllMessagesAsync();
        return Ok(messages);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMessage(int id)
    {
        var culture = HttpContext.Items["RequestCulture"] as CultureInfo ?? new CultureInfo("ru");
        _logger.LogInformation("Получение сообщения на языке: {Culture}", culture.Name);

        try
        {
            var message = await _messageService.GetMessageAsync(id);
            return Ok(message);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Сообщение с ID {MessageId} не найдено", id);
            return NotFound(new { Error = "Сообщение не найдено", MessageId = id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage([FromBody] MessageCreateDto messageDto)
    {
        var createdMessage = await _messageService.CreateMessageAsync(messageDto);
        return CreatedAtAction(nameof(GetMessage), new { id = createdMessage.Id }, createdMessage);
    }
}
