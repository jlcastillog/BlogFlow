using Asp.Versioning;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace BlogFlow.Core.Services.WebApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PostsController : Controller
    {
        private readonly IPostsApplication  _postsApplication;

        public PostsController(IPostsApplication postsApplication)
        {
            _postsApplication = postsApplication;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _postsApplication.GetAllAsync();

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpGet("Get/{postId}")]
        public async Task<IActionResult> GetAsync(string postId)
        {
            if (string.IsNullOrEmpty(postId))
            {
                return BadRequest("PostId is required");
            }

            var response = await _postsApplication.GetAsync(postId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> InsertAsync([FromBody] PostDTO post)
        {
            if (post == null)
            {
                return BadRequest("Post is required");
            }
            var response = await _postsApplication.InsertAsync(post);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpGet("Count")]
        public async Task<IActionResult> CountAsync()
        {
            var response = await _postsApplication.CountAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpPost("Update/{postId}")]
        public async Task<IActionResult> UpdateAsync(string postId, [FromBody] PostDTO post)
        {
            if (string.IsNullOrEmpty(postId))
            {
                return BadRequest("PostId is required");
            }
            if (post == null)
            {
                return BadRequest("Post is required");
            }
            var response = await _postsApplication.UpdateAsync(postId, post);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("delete/{postId}")]
        public async Task<IActionResult> DeleteAsync(string postId)
        {
            if (string.IsNullOrEmpty(postId))
            {
                return BadRequest("PostId is required");
            }
            var response = await _postsApplication.DeleteAsync(postId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }
    }
}
