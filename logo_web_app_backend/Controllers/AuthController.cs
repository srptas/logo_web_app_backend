using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using logo_web_app_backend.Models;


namespace logo_web_app_backend.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]

        public IActionResult AuthenticateUser([FromBody] AuthResponse model)
        {
            if (model.UserName == "Admin" && model.Password == "123")
            {
                return Ok(TokenService.GenerateToken(model.UserName));
            }
            else
            {
                return BadRequest("Wrong Username or Password!"); //hata koduna bak
            }

        }
    }

}
