using System.Threading;
using System.Threading.Tasks;
using Kalantyr.Calendar.Models;
using Kalantyr.Web;

namespace Kalantyr.Auth.Services
{
    public interface ICalendarService
    {
        Task<ResultDto<Event[]>> GetAllEventsAsync(string authToken, CancellationToken cancellationToken);
    }
}
