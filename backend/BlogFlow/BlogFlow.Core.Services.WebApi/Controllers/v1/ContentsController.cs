using Asp.Versioning;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogFlow.Core.Services.WebApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ContentsController : Controller
    {
        private readonly IContentsApplication _ContentsApplication;

        public ContentsController(IContentsApplication ContentsApplication)
        {
            _ContentsApplication = ContentsApplication;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _ContentsApplication.GetAllAsync();

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpGet("Get/{contentId}")]
        public async Task<IActionResult> GetAsync(string contentId)
        {
            if (string.IsNullOrEmpty(contentId))
            {
                return BadRequest("ContentId is required");
            }

            var response = await _ContentsApplication.GetAsync(contentId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> InsertAsync([FromBody] ContentDTO content)
        {
            if (content == null)
            {
                return BadRequest("Content is required");
            }
            var response = await _ContentsApplication.InsertAsync(content);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpGet("Count")]
        public async Task<IActionResult> CountAsync()
        {
            var response = await _ContentsApplication.CountAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpPost("Update/{contentId}")]
        public async Task<IActionResult> UpdateAsync(string contentId, [FromBody] ContentDTO content)
        {
            if (string.IsNullOrEmpty(contentId))
            {
                return BadRequest("ContentId is required");
            }
            if (content == null)
            {
                return BadRequest("Content is required");
            }
            var response = await _ContentsApplication.UpdateAsync(contentId, content);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("delete/{contentId}")]
        public async Task<IActionResult> DeleteAsync(string contentId)
        {
            if (string.IsNullOrEmpty(contentId))
            {
                return BadRequest("ContentId is required");
            }
            var response = await _ContentsApplication.DeleteAsync(contentId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }
    }
}
