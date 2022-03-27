using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Kalantyr.Auth.Client;
using Kalantyr.Calendar.Models;
using Kalantyr.Web;

namespace Kalantyr.Calendar.Client
{
    public class CalendarClient: HttpClientBase
    {
        private readonly string _appKey;

        public CalendarClient(IHttpClientFactory httpClientFactory, string appKey) : base(httpClientFactory, new RequestEnricher())
        {
            _appKey = appKey ?? throw new ArgumentNullException(nameof(appKey));
        }

        public async Task<ResultDto<Event[]>> GetAllEventsAsync(string authToken, CancellationToken cancellationToken)
        {
            var enricher = (RequestEnricher)base.RequestEnricher;
            enricher.TokenEnricher.Token = authToken;
            enricher.AppKeyEnricher.AppKey = _appKey;

            return await Get<ResultDto<Event[]>>("/events/all", cancellationToken);
        }

        protected class RequestEnricher : IRequestEnricher
        {
            public TokenRequestEnricher TokenEnricher { get; } = new TokenRequestEnricher();

            public AppKeyRequestEnricher AppKeyEnricher { get; } = new AppKeyRequestEnricher();

            public void Enrich(HttpRequestHeaders requestHeaders)
            {
                TokenEnricher.Enrich(requestHeaders);
                AppKeyEnricher.Enrich(requestHeaders);
            }
        }
    }
}
