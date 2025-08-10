using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.UseCases;
using BlogFlow.Core.Services.WebApi.Controllers.v1;
using BlogFlow.Core.Transversal.Common;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BlogFlow.Auth.Application.Test.Controllers
{
    [TestClass]
    public class FollowersControllerTests
    {
        private Mock<IFollowersApplication> _followersAppMock = null!;
        private FollowersController _controller = null!;

        /// <summary>
        /// Initializes mocks and controller before each test.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _followersAppMock = new Mock<IFollowersApplication>();
            _controller = new FollowersController(_followersAppMock.Object);
        }

        /// <summary>
        /// Should return Ok when GetAllAsync is successful.
        /// </summary>
        [TestMethod]
        public async Task GetAllAsync_ReturnsOk_WhenSuccess()
        {
            var followerList = new List<FollowerDTO>
            {
                new FollowerDTO
                {
                    Id = 1,
                    UserId = 1,
                    User = new UserDTO { UserId = 1, FirstName = "John", LastName = "Doe", UserName = "johndoe", Email = "john@example.com" },
                    BlogID = 1,
                    Blog = new BlogDTO { Id = 1, Title = "Test Blog", Description = "Desc", Category = "Tech", UserId = 1 }
                }
            };

            _followersAppMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<IEnumerable<FollowerDTO>> { IsSuccess = true, Data = followerList });
            var result = await _controller.GetAllAsync();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        /// <summary>
        /// Should return BadRequest when GetAllAsync fails.
        /// </summary>
        [TestMethod]
        public async Task GetAllAsync_ReturnsBadRequest_WhenFailure()
        {
            _followersAppMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<IEnumerable<FollowerDTO>> { IsSuccess = false, Message = "Error" });
            var result = await _controller.GetAllAsync();
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        /// <summary>
        /// Should return BadRequest when followerId is null in GetAsync.
        /// </summary>
        [TestMethod]
        public async Task GetAsync_ReturnsBadRequest_WhenFollowerIdIsNull()
        {
            var result = await _controller.GetAsync(default!);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        /// <summary>
        /// Should return Ok when GetAsync is successful.
        /// </summary>
        [TestMethod]
        public async Task GetAsync_ReturnsOk_WhenSuccess()
        {
            var followerDto = new FollowerDTO
            {
                Id = 1,
                UserId = 1,
                User = new UserDTO { UserId = 1, FirstName = "John", LastName = "Doe", UserName = "johndoe", Email = "john@example.com" },
                BlogID = 1,
                Blog = new BlogDTO { Id = 1, Title = "Test Blog", Description = "Desc", Category = "Tech", UserId = 1 }
            };

            _followersAppMock.Setup(x => x.GetAsync("followerId", It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<FollowerDTO> { IsSuccess = true, Data = followerDto });
            var result = await _controller.GetAsync("followerId");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        /// <summary>
        /// Should return BadRequest when FollowerDTO is null in InsertAsync.
        /// </summary>
        [TestMethod]
        public async Task InsertAsync_ReturnsBadRequest_WhenFollowerIsNull()
        {
            var result = await _controller.InsertAsync(default!);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        /// <summary>
        /// Should return Ok when InsertAsync is successful.
        /// </summary>
        [TestMethod]
        public async Task InsertAsync_ReturnsOk_WhenSuccess()
        {
            var followerDto = new FollowerDTO
            {
                Id = 1,
                UserId = 1,
                User = new UserDTO { UserId = 1, FirstName = "John", LastName = "Doe", UserName = "johndoe", Email = "john@example.com" },
                BlogID = 1,
                Blog = new BlogDTO { Id = 1, Title = "Test Blog", Description = "Desc", Category = "Tech", UserId = 1 }
            };

            _followersAppMock.Setup(x => x.InsertAsync(followerDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<bool> { IsSuccess = true, Data = true });
            var result = await _controller.InsertAsync(followerDto);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        /// <summary>
        /// Should return Ok when CountAsync is successful.
        /// </summary>
        [TestMethod]
        public async Task CountAsync_ReturnsOk_WhenSuccess()
        {
            _followersAppMock.Setup(x => x.CountAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<int> { IsSuccess = true, Data = 1 });
            var result = await _controller.CountAsync();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        /// <summary>
        /// Should return BadRequest when CountAsync fails.
        /// </summary>
        [TestMethod]
        public async Task CountAsync_ReturnsBadRequest_WhenFailure()
        {
            _followersAppMock.Setup(x => x.CountAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<int> { IsSuccess = false, Message = "Error" });
            var result = await _controller.CountAsync();
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        /// <summary>
        /// Should return BadRequest when followerId is null in UpdateAsync.
        /// </summary>
        [TestMethod]
        public async Task UpdateAsync_ReturnsBadRequest_WhenFollowerIdIsNull()
        {
            var followerDto = new FollowerDTO
            {
                Id = 1,
                UserId = 1,
                User = new UserDTO { UserId = 1, FirstName = "John", LastName = "Doe", UserName = "johndoe", Email = "john@example.com" },
                BlogID = 1,
                Blog = new BlogDTO { Id = 1, Title = "Test Blog", Description = "Desc", Category = "Tech", UserId = 1 }
            };

            var result = await _controller.UpdateAsync(default!, followerDto);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        /// <summary>
        /// Should return BadRequest when FollowerDTO is null in UpdateAsync.
        /// </summary>
        [TestMethod]
        public async Task UpdateAsync_ReturnsBadRequest_WhenFollowerIsNull()
        {
            var result = await _controller.UpdateAsync("id", default!);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        /// <summary>
        /// Should return Ok when UpdateAsync is successful.
        /// </summary>
        [TestMethod]
        public async Task UpdateAsync_ReturnsOk_WhenSuccess()
        {
            var followerDto = new FollowerDTO
            {
                Id = 1,
                UserId = 1,
                User = new UserDTO { UserId = 1, FirstName = "John", LastName = "Doe", UserName = "johndoe", Email = "john@example.com" },
                BlogID = 1,
                Blog = new BlogDTO { Id = 1, Title = "Test Blog", Description = "Desc", Category = "Tech", UserId = 1 }
            };

            _followersAppMock.Setup(x => x.UpdateAsync("id", followerDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<bool> { IsSuccess = true, Data = true });
            var result = await _controller.UpdateAsync("id", followerDto);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        /// <summary>
        /// Should return BadRequest when followerId is null in DeleteAsync.
        /// </summary>
        [TestMethod]
        public async Task DeleteAsync_ReturnsBadRequest_WhenFollowerIdIsNull()
        {
            var result = await _controller.DeleteAsync(default!);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        /// <summary>
        /// Should return Ok when DeleteAsync is successful.
        /// </summary>
        [TestMethod]
        public async Task DeleteAsync_ReturnsOk_WhenSuccess()
        {
            _followersAppMock.Setup(x => x.DeleteAsync("id", It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<bool> { IsSuccess = true, Data = true });
            var result = await _controller.DeleteAsync("id");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}
