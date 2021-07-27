using AutoMapper;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Model;
using GetPet.WebApi.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GetPet.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationsController> _logger;
        private readonly INotificationHandler _notificationHandler;

        public NotificationsController(
            ILogger<NotificationsController> logger,
            IMapper mapper,
            INotificationHandler notificationHandler)
        {
            _logger = logger;
            _mapper = mapper;            
            _notificationHandler = notificationHandler;            
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Search([FromBody] PetFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }           
            await _notificationHandler.UpsertNotificationAsync(CurrentUser.Id, filter);

            return Ok();
        }
    }
}