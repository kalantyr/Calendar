using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kalantyr.Calendar.Client;
using Moq;
using NUnit.Framework;

namespace Kalantyr.Calendar.Tests
{
    [Explicit]
    public class CalendarClient_Tests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();

        [Test]
        public async Task CalendarClient_Test()
        {
            var cancellationToken = CancellationToken.None;

            _httpClientFactory
                .Setup(hcf => hcf.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient
                {
                    BaseAddress = new Uri("http://u1628270.plsk.regruhosting.ru/auth")
                });

            var client = new CalendarClient(_httpClientFactory.Object, "asjdFbh67");
            var allEventsResult = await client.GetAllEventsAsync("1234567", cancellationToken);
            Assert.IsNotNull(allEventsResult);
        }
    }
}
