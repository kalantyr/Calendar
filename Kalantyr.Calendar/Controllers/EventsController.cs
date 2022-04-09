using System;
using System.Net;
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
            return await Wrap(ct => 
                _calendarService.GetAllEventsAsync(Request.GetAuthToken(), ct),
                cancellationToken);
        }

        private async Task<IActionResult> Wrap<T>(Func<CancellationToken, Task<T>> f, CancellationToken cancellationToken)
        {
            try
            {
                var result = await f(cancellationToken);
                return Ok(result);
            }
            catch (Exception e)
            {
                return base.StatusCode((int)HttpStatusCode.InternalServerError, e.GetBaseException().Message);
            }
        }
    }
}
