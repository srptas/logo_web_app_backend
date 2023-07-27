using logo_web_app_backend.Controllers;
using logo_web_app_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace logo_web_app_backend
{
    public class TokenService
    {
        public const string SECRET_KEY = "eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IkphdmFJblVzZSIsImV4cCI6MTY4ODk2ODEwMSwiaWF0IjoxNjg4OTY4MTAxfQ.sU8q2EKgg9IOcWQUrfwflwZbgEgqrilJnGTFY8Xcw8w";
        public const string ISSUER = "https://localhost:44368";
        public const string AUDIENCE = "https://localhost:44368";


        public static string GenerateToken(string username)
        {
            byte[] key = Encoding.UTF8.GetBytes(SECRET_KEY);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim (ClaimTypes.Name, username)

            };


            JwtSecurityToken jwtSecurityToken = 
                new JwtSecurityToken(ISSUER, AUDIENCE, claims, null, expires: DateTime.Now.AddMinutes(30), credentials);

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;

        }


        public static AuthResponse ValidateToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);

            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                // Token has expired
                return new AuthResponse
                {
                    statusCode = 401,
                    statusMessage = "Token has expired"
                };
            }

            // Token is valid
            return new AuthResponse
            {
                statusCode = 200,
                statusMessage = "Token is valid"
            };

        }
    }
}
