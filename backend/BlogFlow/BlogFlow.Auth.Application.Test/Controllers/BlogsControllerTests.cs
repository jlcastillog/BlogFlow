using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.UseCases;
using BlogFlow.Core.Services.WebApi.Controllers.v1;
using BlogFlow.Core.Transversal.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BlogFlow.Auth.Application.Test.Controllers
{

    [TestClass]
    public class BlogsControllerTests
    {
        private Mock<IBlogsApplication> _blogsAppMock = null!;
        private BlogsController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _blogsAppMock = new Mock<IBlogsApplication>();
            _controller = new BlogsController(_blogsAppMock.Object);
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsOk_WhenSuccess()
        {
            _blogsAppMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<IEnumerable<BlogDTO>> { IsSuccess = true, Data = new List<BlogDTO>() });
            var result = await _controller.GetAllAsync();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsBadRequest_WhenFailure()
        {
            _blogsAppMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<IEnumerable<BlogDTO>> { IsSuccess = false, Message = "Error" });
            var result = await _controller.GetAllAsync();
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task GetAsync_ReturnsBadRequest_WhenBlogIdIsNull()
        {
            var result = await _controller.GetAsync(default!);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOk_WhenSuccess()
        {
            _blogsAppMock.Setup(x => x.GetAsync("blogId", It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<BlogDTO> { IsSuccess = true, Data = new BlogDTO() });
            var result = await _controller.GetAsync("blogId");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task InsertAsync_ReturnsBadRequest_WhenBlogIsNull()
        {
            var result = await _controller.InsertAsync(default!, default!);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task InsertAsync_ReturnsBadRequest_WhenImageIsNull()
        {
            var result = await _controller.InsertAsync(new BlogDTO(), default!);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task InsertAsync_ReturnsOk_WhenSuccess()
        {
            var blogDto = new BlogDTO();
            var imageMock = new Mock<IFormFile>();
            _blogsAppMock.Setup(x => x.InsertAsync(blogDto, It.IsAny<ImageStorageDTO>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<bool> { IsSuccess = true, Data = true });
            var result = await _controller.InsertAsync(blogDto, imageMock.Object);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CountAsync_ReturnsOk_WhenSuccess()
        {
            _blogsAppMock.Setup(x => x.CountAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<int> { IsSuccess = true, Data = 1 });
            var result = await _controller.CountAsync();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CountAsync_ReturnsBadRequest_WhenFailure()
        {
            _blogsAppMock.Setup(x => x.CountAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<int> { IsSuccess = false, Message = "Error" });
            var result = await _controller.CountAsync();
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task UpdateAsync_ReturnsBadRequest_WhenBlogIdIsNull()
        {
            var result = await _controller.UpdateAsync(default!, new BlogDTO(), default!);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task UpdateAsync_ReturnsBadRequest_WhenBlogIsNull()
        {
            var result = await _controller.UpdateAsync("id", default!, default!);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task UpdateAsync_ReturnsOk_WhenSuccess()
        {
            var blogDto = new BlogDTO();
            var imageMock = new Mock<IFormFile>();
            _blogsAppMock.Setup(x => x.UpdateAsync("id", blogDto, It.IsAny<ImageStorageDTO>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<bool> { IsSuccess = true, Data = true });
            var result = await _controller.UpdateAsync("id", blogDto, imageMock.Object);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnsBadRequest_WhenBlogIdIsNull()
        {
            var result = await _controller.DeleteAsync(default!);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnsOk_WhenSuccess()
        {
            _blogsAppMock.Setup(x => x.DeleteAsync("id", It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<bool> { IsSuccess = true, Data = true });
            var result = await _controller.DeleteAsync("id");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}