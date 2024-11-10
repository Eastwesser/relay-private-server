using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Relay.Services;
using Relay.DTOs;
using Relay.Models;
using System.Globalization;
using System.Threading.Tasks;

namespace Relay.Server.ControllersEastwesser;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserCreateDto userDto)
    {
        var culture = HttpContext.Items["RequestCulture"] as CultureInfo ?? new CultureInfo("ru");
        _logger.LogInformation("Регистрация нового пользователя с именем {Username} на языке {Culture}", userDto.Name, culture.Name);

        var user = await _userService.RegisterAsync(userDto);
        return Ok(user);
    }
}
