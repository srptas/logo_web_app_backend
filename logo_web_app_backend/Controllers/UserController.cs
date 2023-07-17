using logo_web_app_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace logo_web_app_backend.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /*
        private User AuthenticateUser(User user)
        {

            User _user = null;

            if (user.UserName == "Admin" && user.Password == "12345")
            {
                _user = new User { UserName = "Serap Tas"};
                
            }
            return _user;

        }

        private string GenerateToken(User users)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt: Key"]));

            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt: Issuer"], _configuration["Jwt: Audience"], null, 
                    expires: DateTime.Now.AddMinutes(1),
                    signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(User user)
        {
            IActionResult response = Unauthorized();

            var user_ = AuthenticateUser(user);

            if (user_ != null) 
            {
                var token = GenerateToken(user_);

                response = Ok(new { token = token });
            }

            return response;
        }
        */

        
        //[Authorize]

        [HttpGet]
        [Route("users")]

        public Response GetAllUsers() {

            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("conn").ToString());


            Response response = new Response();

            DAL dal = new DAL();

            response = dal.GetAllUsers(conn);

            return response;
        }

        [HttpPost]
        [Route("registration")]

        public Response registration(User registration)
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("conn").ToString());

            Response response = new Response();

            DAL dal = new DAL();

            response = dal.RegistrationUser(conn, registration);

            return response;  
        }

        [HttpPost]
        [Route("login")]

        public Response login(User registration)
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("conn").ToString());
            Response response = new Response();

            DAL dal = new DAL();

            response = dal.Login(conn, registration);

            return response;

        }


        [HttpPut]
        [Route("userupdate/{id}")]

        public Response userupdate(User registration, int id)
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("conn").ToString());

            Response response = new Response();

            DAL dal = new DAL();

            response = dal.UpdateUser(conn, registration, id);

            return response;

        }


        [HttpDelete]
        [Route("userdelete/{id}")]

        public Response userdelete(int id)
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("conn").ToString());

            Response response = new Response();

            DAL dal = new DAL();

            response = dal.DeleteUser(conn, id);

            return response;

        }




    }
}
