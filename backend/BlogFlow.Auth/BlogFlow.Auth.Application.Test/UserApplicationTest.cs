using BlogFlow.Auth.Application.DTO;
using BlogFlow.Auth.Application.Interface.UseCases;
using BlogFlow.Auth.Services.WebApi.Controllers.v1;
using BlogFlow.Auth.Transversal.Common;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BlogFlow.Auth.Application.Test
{
    [TestClass]
    public sealed class UserApplicationTest
    {
        private Mock<IUsersApplication> _mockProductService;
        private UsersController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockProductService = new Mock<IUsersApplication>();
            _controller = new UsersController(_mockProductService.Object);
        }

        [TestMethod]
        public async Task Authenticate_ReturnOK_WhenUserExistsAsync()
        {
            // Arrange
            var userName = "test";
            var userPassword = "test";
            var userRequestDto = new UserRequestDTO { UserName = userName, Password = userPassword };

            // Configure the mock
            _mockProductService.Setup(x => x.Authenticate(userName, userPassword, default))
                               .ReturnsAsync(new Response<UserResponseDTO>
                               {
                                   IsSuccess = true,
                                   Data = new UserResponseDTO()
                                   {
                                       FirstName = "Test",
                                       LastName = "Test",
                                       UserName = userName,
                                       Token = "token"
                                   }
                               });
            // Run
            var result = await _controller.Authenticate(userRequestDto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(Response<UserResponseDTO>));
            Assert.AreEqual(userName, ((Response<UserResponseDTO>)okResult.Value).Data.UserName);

            _mockProductService.Verify(service => service.Authenticate(userName, userPassword, default), Times.Once);
        }

        [TestMethod]
        public async Task Authenticate_ReturnNotFound_WhenUserNotExistsAsync()
        {
            // Arrange
            var userName = "test";
            var userPassword = "test";
            var userRequestDto = new UserRequestDTO { UserName = userName, Password = userPassword };

            // Configure the mock
            _mockProductService.Setup(x => x.Authenticate(userName, userPassword, default))
                               .ReturnsAsync(new Response<UserResponseDTO> { IsSuccess = true, Data = null });
            // Run
            var result = await _controller.Authenticate(userRequestDto);

            // Assert
            var resultNotFound = result as NotFoundObjectResult;
            Assert.IsNotNull(resultNotFound);
            Assert.AreEqual(404, resultNotFound.StatusCode);
            Assert.IsNotNull(resultNotFound.Value);
            Assert.IsInstanceOfType(resultNotFound.Value, typeof(Response<UserResponseDTO>));
            Assert.IsNull(((Response<UserResponseDTO>)resultNotFound.Value).Data);
        }

        [TestMethod]
        public async Task Authenticate_ReturnBadRequest_WhenUserNotExistsAsync()
        {
            // Arrange
            var userName = "test";
            var userRequestDto = new UserRequestDTO { UserName = userName };

            // Configure the mock
            _mockProductService.Setup(x => x.Authenticate(userName, null, default))
                               .ReturnsAsync(new Response<UserResponseDTO> { IsSuccess = false });

            // Run
            var result = await _controller.Authenticate(userRequestDto);

            // Assert
            var resultBadRequest = result as BadRequestObjectResult;
            Assert.IsNotNull(resultBadRequest);

        }
    }
}
