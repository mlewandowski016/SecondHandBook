using Microsoft.AspNetCore.Mvc;
using SecondHandBook.Entities;
using SecondHandBook.Models;
using SecondHandBook.Services;
using System.Security.Claims;

namespace SecondHandBook.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserContextService _userContextService;

        public UserController(IUserService userService, IUserContextService userContextService)
        {
            _userService = userService;
            _userContextService = userContextService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody]RegisterUserDto dto)
        {
            _userService.RegisterUser(dto);

            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDto dto)
        {
            UserDto token = _userService.GenerateToken(dto);

            return Ok(token);
        }
    }
}
