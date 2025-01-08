using Asp.Versioning;
using BlogFlow.Auth.Application.DTO;
using BlogFlow.Auth.Application.Interface.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogFlow.Auth.Services.WebApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsersController : Controller
    {
        private readonly IUsersApplication _usersApplication;

        public UsersController(IUsersApplication usersApplication)
        {
            _usersApplication = usersApplication;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserRequestDTO usersRequestDto)
        {
            var respose = await _usersApplication.Authenticate(usersRequestDto.UserName, usersRequestDto.Password);

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
