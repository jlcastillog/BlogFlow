﻿using Asp.Versioning;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.UseCases;
using Microsoft.AspNetCore.Authorization;
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
    }
}
