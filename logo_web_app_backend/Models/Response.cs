using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace logo_web_app_backend.Models
{
    public class Response
    {
        public int statusCode { get; set; }

        public string statusMessage { get; set; }

        public User User { get; set; }

        public List<User> users {get; set; }


    }
}
