using Microsoft.AspNetCore.Mvc;
using Relay.Services;
using Relay.DTOs;
using Relay.DBUtility.Models;
using System;
using System.Threading.Tasks;

namespace Relay.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServerController : ControllerBase
    {
        private readonly IServerService _serverService;

        public ServerController(IServerService serverService)
        {
            _serverService = serverService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServer(Guid id)
        {
            var srv = await _serverService.GetServerAsync(id);
            return srv == null ? NotFound() : Ok(srv);
        }

        [HttpPost]
        public async Task<IActionResult> CreateServer([FromBody] ServerCreateDto serverDto)
        {
            var srv = new ServerDbModel
            {
                Name = serverDto.Name,
                Address = serverDto.Address,
                Port = serverDto.Port,
                Password = serverDto.Password
            };

            var createdServer = await _serverService.CreateServerAsync(srv);
            return CreatedAtAction(nameof(GetServer), new { id = createdServer.Id }, createdServer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServers()
        {
            var servers = await _serverService.GetAllServersAsync();
            return Ok(servers);
        }
    }
}
