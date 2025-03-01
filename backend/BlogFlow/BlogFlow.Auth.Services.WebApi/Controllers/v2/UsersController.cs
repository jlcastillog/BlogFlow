using Asp.Versioning;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.UseCases;
using BlogFlow.Auth.Services.WebApi.Helpers;
using BlogFlow.Core.Transversal.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogFlow.Core.Domain.Entities;
using BlogFlow.Core.Application.UseCases.RefreshTokens;
using Newtonsoft.Json;

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
                    var (accessToken, refreshToken) = BuildToken(respose);

                    // Configurar cookies
                    var jwtCookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_appSettings.AccessTokenExpiration)) // Same like JWT
                    };

                    var newRefreshToken = new RefreshTokenDTO
                    {
                        Token = refreshToken,
                        AccessToken = accessToken,
                        JwtId = Guid.NewGuid().ToString(),
                        IsRevoked = false,
                        IsUsed = false,
                        ExpiryDate = DateTime.UtcNow.AddDays(Convert.ToDouble(_appSettings.RefreshTokenExpiration)),
                        UserId = respose.Data.UserId
                    };

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

        private (string AccessToken, string RefreshToken) BuildToken(Response<UserResponseDTO> usersDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usersDto.Data.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_appSettings.AccessTokenExpiration)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = Guid.NewGuid().ToString();

            return (tokenHandler.WriteToken(token), refreshToken);
        }
    }
}
