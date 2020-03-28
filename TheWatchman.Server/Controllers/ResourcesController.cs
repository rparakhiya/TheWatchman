using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheWatchman.Server.Authentication;
using TheWatchman.Server.Services;

namespace TheWatchman.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourceMonitor _resourceMonitor;

        public ResourcesController(IResourceMonitor resourceMonitor)
        {
            _resourceMonitor = resourceMonitor;
        }

        [Authorize(AuthenticationSchemes = TOtpAuthenticationHandler.TOtpAuthenticationScheme)]
        [HttpPost("{resourceId}/heartbeat")]
        public async Task<ActionResult> RegisterHeartbeat([FromRoute]string resourceId)
        {
            await _resourceMonitor.RegisterHeartbeat(resourceId);
            return Ok();
        }

        [HttpGet("{resourceId}/status")]
        public async Task<ActionResult> GetStatus([FromRoute]string resourceId)
        {
            var status = await _resourceMonitor.GetResourceStatus(resourceId);
            return Ok(status);
        }

        [HttpGet("status")]
        public async Task<ActionResult> GetStatus()
        {
            var status = await _resourceMonitor.GetResourceStatus();
            return Ok(status);
        }
    }
}