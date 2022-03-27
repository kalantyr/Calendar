using System;
using System.Threading;
using System.Threading.Tasks;
using Kalantyr.Auth.Services;
using Kalantyr.Auth.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Kalantyr.Calendar.Controllers
{
    [ApiController]
    [Route("events")]
    public class EventsController : ControllerBase
    {
        private readonly ICalendarService _calendarService;

        public EventsController(ICalendarService calendarService)
        {
            _calendarService = calendarService ?? throw new ArgumentNullException(nameof(calendarService));
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllEventsAsync(CancellationToken cancellationToken)
        {
            var result = await _calendarService.GetAllEventsAsync(Request.GetAuthToken(), cancellationToken);
            return Ok(result);
        }
    }
}
