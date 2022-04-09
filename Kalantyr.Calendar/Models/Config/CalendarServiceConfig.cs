namespace Kalantyr.Auth.Models.Config
{
    public class CalendarServiceConfig
    {
        public string ApiKey { get; set; }

        public string AuthService { get; set; } = "https://kalantyr.ru/auth";
    }
}
