using Asp.Versioning;
using Azure;
using BlogFlow.Auth.Services.WebApi.Helpers;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.UseCases;
using BlogFlow.Core.Transversal.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogFlow.Auth.Services.WebApi.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class UsersController : Controller
    {
        private readonly IUsersApplication _usersApplication;
        private readonly IRefreshTokenApplication _refreshTokenApplication;
        private readonly AppSettings _appSettings;

        public UsersController(IUsersApplication usersApplication, IRefreshTokenApplication refreshTokenApplication, IOptions<AppSettings> appSettings)
        {
            _usersApplication = usersApplication;
            _refreshTokenApplication = refreshTokenApplication;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthUserRequestDTO usersRequestDto)
        {
            var respose = await _usersApplication.Authenticate(usersRequestDto.UserName, usersRequestDto.Password);

            if (respose.IsSuccess)
            {
                if (respose.Data != null)
                {
                    var tokenService = new TokenService(_appSettings);

                    // Configurar cookies
                    var jwtCookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_appSettings.AccessTokenExpiration)) // Same like JWT
                    };

                    var newRefreshToken = tokenService.CreateNewToken(respose.Data.UserId);

                    var refreshTokenResponse = await _refreshTokenApplication.InsertAsync(newRefreshToken);

                    if (!refreshTokenResponse.IsSuccess)
                    {
                        respose.Data = null;
                        respose.IsSuccess = false;
                        respose.Message = "Authenticate fail!!";

                        return BadRequest(respose);
                    }

                    Response.Cookies.Append("jwt", JsonConvert.SerializeObject(newRefreshToken), jwtCookieOptions);

                    return Ok(respose);
                }
                else
                {
                    return NotFound(respose);
                }
            }

            return BadRequest(respose);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDTO request)
        {
            var existRefreshToken = await _refreshTokenApplication.ExistsRefreshAsync(request);

            if (!existRefreshToken.IsSuccess)
            {
                return Unauthorized(new { message = "Invalid or expired Refresh Token" });
            }

            var markRefreshTokenAsUsed = await _refreshTokenApplication.MarkRefreshTokenAsUsed(request);

            if (!markRefreshTokenAsUsed.IsSuccess)
            {
                return Unauthorized(new { message = "Invalid or expired Refresh Token" });
            }

            var tokenService = new TokenService(_appSettings);

            // Configurar cookies
            var jwtCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_appSettings.AccessTokenExpiration)) // Same like JWT
            };

            var newRefreshToken = tokenService.CreateNewToken(request.UserId);

            var refreshTokenResponse = await _refreshTokenApplication.InsertAsync(newRefreshToken);

            if (!refreshTokenResponse.IsSuccess)
            {
                return BadRequest();
            }

            Response.Cookies.Append("jwt", "", jwtCookieOptions);
            Response.Cookies.Append("jwt", JsonConvert.SerializeObject(newRefreshToken), jwtCookieOptions);

            return Ok();
        }

        [Authorize]
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            var jwtCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(-1)
            };

            Response.Cookies.Append("jwt", "", jwtCookieOptions);

            return Ok();
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

        [Authorize]
        [HttpPost("Update/{userId}")]
        public async Task<IActionResult> UpdateAsync(string userId, [FromBody] UserDTO userDto)
        {
            if (userDto == null || userDto.HasValue())
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

        [Authorize]
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
