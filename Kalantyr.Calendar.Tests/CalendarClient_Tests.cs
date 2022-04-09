using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kalantyr.Auth.Client;
using Kalantyr.Auth.Models;
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
                .Setup(hcf => hcf.CreateClient(nameof(AuthClient)))
                .Returns(new HttpClient
                {
                    BaseAddress = new Uri("https://kalantyr.ru/auth")
                });
            _httpClientFactory
                .Setup(hcf => hcf.CreateClient(nameof(CalendarClient)))
                .Returns(new HttpClient
                {
                    BaseAddress = new Uri("https://kalantyr.ru/calendar")
                });

            var authClient = new AuthClient(_httpClientFactory.Object);
            var loginResult = await authClient.LoginByPasswordAsync(new LoginPasswordDto{Login = "user1", Password = "qwerty1" }, cancellationToken);
            var token = loginResult.Result.Value;

            var client = new CalendarClient(_httpClientFactory.Object, "asjdFbh67");
            var allEventsResult = await client.GetAllEventsAsync(token, cancellationToken);
            Assert.IsNotNull(allEventsResult);
        }
    }
}
