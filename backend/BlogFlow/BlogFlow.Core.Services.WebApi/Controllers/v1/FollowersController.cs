using Asp.Versioning;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace BlogFlow.Core.Services.WebApi.Controllers.v1
{
    /// <summary>
    /// Controller for managing CRUD operations for Followers.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class FollowersController : Controller
    {
        private readonly IFollowersApplication _followersApplication;

        /// <summary>
        /// Constructor that injects the followers application service.
        /// </summary>
        /// <param name="followersApplication">Application service for followers.</param>
        public FollowersController(IFollowersApplication followersApplication)
        {
            _followersApplication = followersApplication;
        }

        /// <summary>
        /// Gets all followers. 
        /// </summary>
        /// <returns>List of followers.</returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _followersApplication.GetAllAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        /// <summary>
        /// Gets a follower by its identifier.
        /// </summary>
        /// <param name="followerId">Follower identifier.</param>
        /// <returns>Follower data.</returns>
        [HttpGet("Get/{followerId}")]
        public async Task<IActionResult> GetAsync(string followerId)
        {
            if (string.IsNullOrEmpty(followerId))
            {
                return BadRequest("FollowerId is required");
            }

            var response = await _followersApplication.GetAsync(followerId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        /// <summary>
        /// Inserts a new follower.
        /// </summary>
        /// <param name="follower">Follower data to insert.</param>
        /// <returns>Operation result.</returns>
        [HttpPost("Insert")]
        public async Task<IActionResult> InsertAsync([FromForm] FollowerDTO follower)
        {
            if (follower == null)
            {
                return BadRequest("Follower is required");
            }

            var response = await _followersApplication.InsertAsync(follower);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        /// <summary>
        /// Gets the total number of followers.
        /// </summary>
        /// <returns>Followers count.</returns>
        [HttpGet("Count")]
        public async Task<IActionResult> CountAsync()
        {
            var response = await _followersApplication.CountAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        /// <summary>
        /// Updates an existing follower.
        /// </summary>
        /// <param name="followerId">Follower identifier to update.</param>
        /// <param name="follower">Updated follower data.</param>
        /// <returns>Operation result.</returns>
        [HttpPost("Update/{followerId}")]
        public async Task<IActionResult> UpdateAsync(string followerId, [FromForm] FollowerDTO follower)
        {
            if (string.IsNullOrEmpty(followerId))
            {
                return BadRequest("FollowerId is required");
            }
            if (follower == null)
            {
                return BadRequest("Follower is required");
            }

            var response = await _followersApplication.UpdateAsync(followerId, follower);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        /// <summary>
        /// Deletes a follower by its identifier.
        /// </summary>
        /// <param name="followerId">Follower identifier to delete.</param>
        /// <returns>Operation result.</returns>
        [HttpDelete("Delete/{followerId}")]
        public async Task<IActionResult> DeleteAsync(string followerId)
        {
            if (string.IsNullOrEmpty(followerId))
            {
                return BadRequest("FollowerId is required");
            }

            var response = await _followersApplication.DeleteAsync(followerId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }
    }
}
