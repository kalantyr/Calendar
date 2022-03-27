using System;
using System.Threading;
using System.Threading.Tasks;
using Kalantyr.Auth.Client;
using Kalantyr.Auth.Models.Config;
using Kalantyr.Calendar.Models;
using Kalantyr.Web;
using Microsoft.Extensions.Options;

namespace Kalantyr.Auth.Services.Impl
{
    public class CalendarService: ICalendarService
    {
        private readonly IAuthClient _authClient;
        private readonly CalendarServiceConfig _config;

        public CalendarService(IAuthClient authClient, IOptions<CalendarServiceConfig> config)
        {
            _authClient = authClient ?? throw new ArgumentNullException(nameof(authClient));
            _config = config.Value;
        }

        public async Task<ResultDto<Event[]>> GetAllEventsAsync(string authToken, CancellationToken cancellationToken)
        {
            var getUserResult = await _authClient.GetUserIdAsync(authToken, cancellationToken);
            if (getUserResult.Error != null)
                return new ResultDto<Event[]> { Error = getUserResult.Error };

            cancellationToken.ThrowIfCancellationRequested();

            var events = await GetAllEventsAsync(getUserResult.Result, cancellationToken);
            return new ResultDto<Event[]> { Result = events };
        }

        private Task<Event[]> GetAllEventsAsync(uint userId, CancellationToken cancellationToken)
        {
            var events = Array.Empty<Event>();
            switch (userId)
            {
                case 1:
                    events = new[]
                    {
                        new Event { Id = 1, Name = "Event1" },
                        new Event { Id = 11, Name = "Event11" }
                    };
                    break;
                case 2:
                    events = new[]
                    {
                        new Event { Id = 2, Name = "Event2" },
                        new Event { Id = 22, Name = "Event22" }
                    };
                    break;
            }

            return Task.FromResult(events);
        }
    }
}
