using BlogFlow.Auth.Application.DTO;
using BlogFlow.Auth.Application.Interface.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogFlow.Auth.Services.WebApi.Controllers.v1
{
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersApplication _usersApplication;

        public UsersController(IUsersApplication usersApplication)
        {
            _usersApplication = usersApplication;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UserDTO usersDto)
        {
            var respose = _usersApplication.Authenticate(usersDto.UserName, usersDto.Password);

            if (respose.IsSuccess)
            {
                if (respose.Data != null)
                {
                    return Ok(respose);
                }
                else
                {
                    return NotFound(respose);
                }
            }

            return BadRequest(respose);
        }
    }
}
