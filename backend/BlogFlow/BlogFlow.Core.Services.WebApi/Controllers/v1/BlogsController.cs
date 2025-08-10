using Asp.Versioning;
using Azure;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace BlogFlow.Core.Services.WebApi.Controllers.v1
{
    /// <summary>
    /// Controller for managing CRUD operations for Blogs and related actions.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BlogsController : Controller
    {
        private readonly IBlogsApplication _blogsApplication;
        private readonly IFollowersApplication _followersApplication;

        /// <summary>
        /// Constructor that injects the blogs and followers application services.
        /// </summary>
        /// <param name="blogsApplication">Application service for blogs.</param>
        /// <param name="followersApplication">Application service for followers.</param>
        public BlogsController(IBlogsApplication blogsApplication, IFollowersApplication followersApplication)
        {
            _blogsApplication = blogsApplication;
            _followersApplication = followersApplication;
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
        public async Task<IActionResult> InsertAsync([FromForm] BlogDTO blog, IFormFile image)
        {
            if (blog == null)
            {
                return BadRequest("Blog is required");
            }

            if (image == null)
            {
                return BadRequest("Image is required");
            }

            //Insert image in cloud service
            var imageStoreageDto = new ImageStorageDTO()
            {
                File = image
            };

            var response = await _blogsApplication.InsertAsync(blog, imageStoreageDto);
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
        public async Task<IActionResult> UpdateAsync(string blogId, [FromForm] BlogDTO blog, IFormFile? image)
        {
            if (string.IsNullOrEmpty(blogId))
            {
                return BadRequest("BlogId is required");
            }
            if (blog == null)
            {
                return BadRequest("Blog is required");
            }

            var imageStoreageDto = new ImageStorageDTO() { File = image };

            //Update the blog
            var response = await _blogsApplication.UpdateAsync(blogId, blog, imageStoreageDto);
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

            //Remove blog
            var response = await _blogsApplication.DeleteAsync(blogId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        /// <summary>
        /// Follows a blog.
        /// </summary>
        /// <param name="blogId">Blog identifier to follow.</param>
        /// <param name="userId">User identifier who wants to follow.</param>
        /// <returns>Operation result.</returns>
        [HttpPost("{blogId}/followers")]
        public async Task<IActionResult> FollowBlogAsync(int blogId, [FromBody] FollowRequestDTO user)
        {
            if (blogId <= 0 || user?.UserId <= 0)
                return BadRequest("Valid blogId and userId are required.");

            var followerDto = new FollowerDTO
            {
                BlogID = blogId,
                UserId = user!.UserId,
                // Las propiedades de navegación pueden omitirse en la inserción
            };

            var response = await _followersApplication.InsertAsync(followerDto);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response.Message);
        }

        /// <summary>
        /// Unfollows a blog.
        /// </summary>
        /// <param name="blogId">Blog identifier to unfollow.</param>
        /// <param name="userId">User identifier who wants to unfollow.</param>
        /// <returns>Operation result.</returns>
        [HttpDelete("{blogId}/followers/{userId}")]
        public async Task<IActionResult> UnFollowBlogAsync(int blogId, int userId)
        {
            if (blogId <= 0 || userId <= 0)
                return BadRequest("Valid blogId and userId are required.");

            // Buscar el follower correspondiente
            var followersResponse = await _followersApplication.GetAllAsync();
            if (!followersResponse.IsSuccess || followersResponse.Data == null)
                return BadRequest(followersResponse.Message);

            var follower = followersResponse.Data.FirstOrDefault(f => f.BlogID == blogId && f.UserId == userId);
            if (follower == null)
                return NotFound("Follower not found.");

            var response = await _followersApplication.DeleteAsync(follower.Id.ToString());
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response.Message);
        }

        /// <summary>
        /// Gets all followers for a specific blog.
        /// </summary>
        /// <param name="blogId">Blog identifier.</param>
        /// <returns>List of followers for the blog.</returns>
        [HttpGet("{blogId}/followers")]
        public async Task<IActionResult> GetFollowersByBlogAsync(int blogId)
        {
            if (blogId <= 0)
                return BadRequest("Valid blogId is required.");

            var followersResponse = await _followersApplication.GetAllAsync();
            if (!followersResponse.IsSuccess || followersResponse.Data == null)
                return BadRequest(followersResponse.Message);

            var followersByBlog = followersResponse.Data.Where(f => f.BlogID == blogId);

            var response = new FollowResponseDTO()
            {
                Followers = followersByBlog.ToArray(),
                NumberOfFollowers = followersByBlog.Count()
            };  

            return Ok(response);
        }
    }
}
