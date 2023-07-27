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

        public AuthResponse login(User registration)
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("conn").ToString());
            AuthResponse response = new AuthResponse();

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
