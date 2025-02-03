using Asp.Versioning;
using BlogFlow.Common.Application.Interface.UserCases;
using BlogFlow.Core.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BlogFlow.Core.Services.WebApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BlogsController : Controller
    {
        private readonly IBlogsApplication _blogsApplication;

        public BlogsController(IBlogsApplication blogsApplication)
        {
            _blogsApplication = blogsApplication;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _blogsApplication.GetAllAsync();

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpGet("Get/{blogId}")]
        public async Task<IActionResult> GetAsync(string blogId)
        {
            if (string.IsNullOrEmpty(blogId))
            {
                return BadRequest("BlogId is required");
            }

            var response = await _blogsApplication.GetAsync(blogId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> InsertAsync([FromBody] BlogDTO blog)
        {
            if (blog == null)
            {
                return BadRequest("Blog is required");
            }
            var response = await _blogsApplication.InsertAsync(blog);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpGet("Count")]
        public async Task<IActionResult> CountAsync()
        {
            var response = await _blogsApplication.CountAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpPost("Update/{blogId}")]
        public async Task<IActionResult> UpdateAsync(string blogId, [FromBody] BlogDTO blog)
        {
            if (string.IsNullOrEmpty(blogId))
            {
                return BadRequest("BlogId is required");
            }
            if (blog == null)
            {
                return BadRequest("Blog is required");
            }
            var response = await _blogsApplication.UpdateAsync(blogId, blog);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("delete/{blogId}")]
        public async Task<IActionResult> DeleteAsync(string blogId)
        {
            if (string.IsNullOrEmpty(blogId))
            {
                return BadRequest("BlogId is required");
            }
            var response = await _blogsApplication.DeleteAsync(blogId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }
    }
}
