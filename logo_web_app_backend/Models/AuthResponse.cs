namespace logo_web_app_backend.Models
{
    public class AuthResponse
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        //public string IsActive{ get; set; }

        public int statusCode { get; set; }

        public string statusMessage { get; set; }

        public string token { get; set; }
    }
}
