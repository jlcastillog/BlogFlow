using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.UseCases;
using BlogFlow.Core.Services.WebApi.Controllers.v1;
using BlogFlow.Core.Transversal.Common;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BlogFlow.Auth.Application.Test.Controllers
{

    [TestClass]
    public class PostsControllerTests
    {
        private Mock<IPostsApplication> _postsAppMock = null!;
        private PostsController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _postsAppMock = new Mock<IPostsApplication>();
            _controller = new PostsController(_postsAppMock.Object);
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsOk_WhenSuccess()
        {
            _postsAppMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<IEnumerable<PostDTO>> { IsSuccess = true, Data = new List<PostDTO>() });
            var result = await _controller.GetAllAsync();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsBadRequest_WhenFailure()
        {
            _postsAppMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<IEnumerable<PostDTO>> { IsSuccess = false, Message = "Error" });
            var result = await _controller.GetAllAsync();
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task GetByBlogAsync_ReturnsBadRequest_WhenBlogIdIsNull()
        {
            var result = await _controller.GetByBlogAsync(null!);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task GetByBlogAsync_ReturnsOk_WhenSuccess()
        {
            _postsAppMock.Setup(x => x.GetByIdBlogAsync("blogId", It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<IEnumerable<PostDTO>> { IsSuccess = true, Data = new List<PostDTO>() });
            var result = await _controller.GetByBlogAsync("blogId");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAsync_ReturnsBadRequest_WhenPostIdIsNull()
        {
            var result = await _controller.GetAsync(null!);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOk_WhenSuccess()
        {
            _postsAppMock.Setup(x => x.GetAsync("postId", It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<PostDTO> { IsSuccess = true, Data = new PostDTO() });
            var result = await _controller.GetAsync("postId");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task InsertAsync_ReturnsBadRequest_WhenPostIsNull()
        {
            var result = await _controller.InsertAsync(null!);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task InsertAsync_ReturnsOk_WhenSuccess()
        {
            _postsAppMock.Setup(x => x.InsertAsync(It.IsAny<PostDTO>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<bool> { IsSuccess = true, Data = true });
            var result = await _controller.InsertAsync(new PostDTO());
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CountAsync_ReturnsOk_WhenSuccess()
        {
            _postsAppMock.Setup(x => x.CountAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<int> { IsSuccess = true, Data = 1 });
            var result = await _controller.CountAsync();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UpdateAsync_ReturnsBadRequest_WhenPostIdIsNull()
        {
            var result = await _controller.UpdateAsync(null!, new PostDTO());
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task UpdateAsync_ReturnsBadRequest_WhenPostIsNull()
        {
            var result = await _controller.UpdateAsync("id", null!);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task UpdateAsync_ReturnsOk_WhenSuccess()
        {
            _postsAppMock.Setup(x => x.UpdateAsync("id", It.IsAny<PostDTO>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<bool> { IsSuccess = true, Data = true });
            var result = await _controller.UpdateAsync("id", new PostDTO());
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnsBadRequest_WhenPostIdIsNull()
        {
            var result = await _controller.DeleteAsync(null!);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnsOk_WhenSuccess()
        {
            _postsAppMock.Setup(x => x.DeleteAsync("id", It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<bool> { IsSuccess = true, Data = true });
            var result = await _controller.DeleteAsync("id");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}