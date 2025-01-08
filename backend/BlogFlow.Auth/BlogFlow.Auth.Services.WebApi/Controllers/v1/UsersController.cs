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

        [HttpPost("Insert")]
        public async Task<IActionResult> InsertAsync([FromBody] UserDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }

            var response = await _usersApplication.InsertAsync(userDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("Update({userId}")]
        public async Task<IActionResult> UpdateAsync(string userId, [FromBody] UserDTO userDto)
        {
            if (userDto == null || userDto.UserId == null)
            {
                return BadRequest();
            }
            var response = await _usersApplication.UpdateAsync(userId, userDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("Delete/{userId}")]
        public async Task<IActionResult> DeleteAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }
            var response = await _usersApplication.DeleteAsync(userId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("Get/{userId}")]
        public async Task<IActionResult> GetAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }
            var response = await _usersApplication.GetAsync(userId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _usersApplication.GetAllAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
