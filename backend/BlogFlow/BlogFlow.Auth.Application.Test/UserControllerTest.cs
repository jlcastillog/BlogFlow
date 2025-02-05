using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.UseCases;
using BlogFlow.Auth.Services.WebApi.Controllers.v1;
using BlogFlow.Core.Transversal.Common;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BlogFlow.Auth.Application.Test
{
    [TestClass]
    public sealed class UserControllerTest
    {
        private Mock<IUsersApplication> _mockUserApplication;
        private UsersController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockUserApplication = new Mock<IUsersApplication>();
            _controller = new UsersController(_mockUserApplication.Object);
        }

        [TestMethod]
        public async Task Authenticate_ReturnOK_WhenUserExistsAsync()
        {
            // Arrange
            var userName = "test";
            var userPassword = "test";
            var userRequestDto = new AuthUserRequestDTO { UserName = userName, Password = userPassword };

            // Configure the mock
            _mockUserApplication.Setup(x => x.Authenticate(userName, userPassword, default))
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
            // Act
            var result = await _controller.Authenticate(userRequestDto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(Response<UserResponseDTO>));
            Assert.AreEqual(userName, ((Response<UserResponseDTO>)okResult.Value).Data.UserName);
        }

        [TestMethod]
        public async Task Authenticate_ReturnNotFound_WhenUserNotExistsAsync()
        {
            // Arrange
            var userName = "test";
            var userPassword = "test";
            var userRequestDto = new AuthUserRequestDTO { UserName = userName, Password = userPassword };

            // Configure the mock
            _mockUserApplication.Setup(x => x.Authenticate(userName, userPassword, default))
                               .ReturnsAsync(new Response<UserResponseDTO> { IsSuccess = true, Data = null });
            // Act
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
            var userRequestDto = new AuthUserRequestDTO { UserName = userName };

            // Configure the mock
            _mockUserApplication.Setup(x => x.Authenticate(userName, null, default))
                               .ReturnsAsync(new Response<UserResponseDTO> { IsSuccess = false });

            // Act
            var result = await _controller.Authenticate(userRequestDto);

            // Assert
            var resultBadRequest = result as BadRequestObjectResult;
            Assert.IsNotNull(resultBadRequest);
        }

        [TestMethod]
        public async Task InsertAsync_ReturnOK_WhenUserExistsAsync()
        {
            // Arrange
            var userDto = new UserDTO { UserName = "test" };

            // Configure the mock
            _mockUserApplication.Setup(x => x.InsertAsync(userDto, default))
                               .ReturnsAsync(new Response<bool> { IsSuccess = true, Data = true });
            // Act
            var result = await _controller.InsertAsync(userDto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(Response<bool>));
            Assert.IsTrue(((Response<bool>)okResult.Value).Data);
        }

        [TestMethod]
        public async Task InsertAsync_ReturnBadRequest_WhenUserNotExistsAsync()
        {
            // Arrange
            UserDTO userDto = null;

            // Act
            var result = await _controller.InsertAsync(userDto);

            // Assert
            var resultBadRequest = result as BadRequestResult;
            Assert.IsNotNull(resultBadRequest);
        }

        [TestMethod]
        public async Task InsertAsync_ReturnBadRequest_WhenResponseIsNotSuccess()
        {
            // Arrange
            var userDto = new UserDTO { UserName = "test" };

            // Configure the mock
            _mockUserApplication.Setup(x => x.InsertAsync(userDto, default))
                               .ReturnsAsync(new Response<bool> { IsSuccess = false });

            // Act
            var result = await _controller.InsertAsync(userDto);
            // Assert
            var resultBadRequest = result as BadRequestObjectResult;
            Assert.IsNotNull(resultBadRequest);
            Assert.AreEqual(400, resultBadRequest.StatusCode);
            Assert.IsNotNull(resultBadRequest.Value);
            Assert.IsInstanceOfType(resultBadRequest.Value, typeof(Response<bool>));
            Assert.IsFalse(((Response<bool>)resultBadRequest.Value).Data);
        }

        [TestMethod]
        public async Task UpdateAsync_ReturnOK_WhenUserExistsAsync()
        {
            // Arrange
            var userDto = new UserDTO { UserId = 1, UserName = "test" };

            // Configure the mock
            _mockUserApplication.Setup(x => x.UpdateAsync("1", userDto, default))
                               .ReturnsAsync(new Response<bool> { IsSuccess = true, Data = true });
            // Act
            var result = await _controller.UpdateAsync("1", userDto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(Response<bool>));
            Assert.IsTrue(((Response<bool>)okResult.Value).Data);
        }

        [TestMethod]
        public async Task UpdateAsync_ReturnBadRequest_WhenUserNotExistsAsync()
        {
            // Arrange
            UserDTO userDto = null;

            // Act
            var result = await _controller.UpdateAsync("1", userDto);

            // Assert
            var resultBadRequest = result as BadRequestResult;
            Assert.IsNotNull(resultBadRequest);
        }

        [TestMethod]
        public async Task UpdateAsync_ReturnBadRequest_WhenUserIdNotExistsAsync()
        {
            // Arrange
            var userDto = new UserDTO { UserName = "test" };

            // Act
            var result = await _controller.UpdateAsync(null, userDto);

            // Assert
            var resultBadRequest = result as BadRequestResult;
            Assert.IsNotNull(resultBadRequest);
        }

        [TestMethod]
        public async Task UpdateAsync_ReturnBadRequest_WhenResponseIsNotSuccess()
        {
            // Arrange
            var userDto = new UserDTO { UserId = 1, UserName = "test" };

            // Configure the mock
            _mockUserApplication.Setup(x => x.UpdateAsync("1", userDto, default))
                               .ReturnsAsync(new Response<bool> { IsSuccess = false});

            // Act
            var result = await _controller.UpdateAsync("1", userDto);

            // Assert
            var resultBadRequest = result as BadRequestObjectResult;
            Assert.IsNotNull(resultBadRequest);
            Assert.AreEqual(400, resultBadRequest.StatusCode);
            Assert.IsNotNull(resultBadRequest.Value);
            Assert.IsInstanceOfType(resultBadRequest.Value, typeof(Response<bool>));
            Assert.IsFalse(((Response<bool>)resultBadRequest.Value).Data);
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnOK_WhenUserExistsAsync()
        {
            // Arrange
            var userId = "1";

            // Configure the mock
            _mockUserApplication.Setup(x => x.DeleteAsync(userId, default))
                               .ReturnsAsync(new Response<bool> { IsSuccess = true, Data = true });

            // Act
            var result = await _controller.DeleteAsync(userId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(Response<bool>));
            Assert.IsTrue(((Response<bool>)okResult.Value).Data);
            _mockUserApplication.Verify(service => service.DeleteAsync(userId, default), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnBadRequest_WhenUserIdNotExistsAsync()
        {
            // Arrange
            var userId = string.Empty;

            // Act
            var result = await _controller.DeleteAsync(userId);

            // Assert
            var resultBadRequest = result as BadRequestResult;
            Assert.IsNotNull(resultBadRequest);
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnBadRequest_WhenResponseIsNotSuccess()
        {
            // Arrange
            var userId = "1";

            // Configure the mock
            _mockUserApplication.Setup(x => x.DeleteAsync(userId, default))
                               .ReturnsAsync(new Response<bool> { IsSuccess = false});
            // Act
            var result = await _controller.DeleteAsync(userId);

            // Assert
            var resultBadRequest = result as BadRequestObjectResult;
            Assert.IsNotNull(resultBadRequest);
            Assert.AreEqual(400, resultBadRequest.StatusCode);
            Assert.IsNotNull(resultBadRequest.Value);
            Assert.IsInstanceOfType(resultBadRequest.Value, typeof(Response<bool>));
            Assert.IsFalse(((Response<bool>)resultBadRequest.Value).Data);
        }

        [TestMethod]
        public async Task GetAsync_ReturnOK_WhenUserExistsAsync()
        {
            // Arrange
            var userId = "1";

            // Configure the mock
            _mockUserApplication.Setup(x => x.GetAsync(userId, default))
                               .ReturnsAsync(new Response<UserResponseDTO>
                               {
                                   IsSuccess = true,
                                   Data = new UserResponseDTO
                                   {
                                       UserId = 1,
                                       UserName = "test"
                                   }
                               });

            // Act
            var result = await _controller.GetAsync(userId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(Response<UserResponseDTO>));
            Assert.AreEqual(userId, ((Response<UserResponseDTO>)okResult.Value).Data.UserId.ToString());
        }

        [TestMethod]
        public async Task GetAsync_ReturnBadRequest_WhenUserIdNotExistsAsync()
        {
            // Arrange
            var userId = string.Empty;

            // Act
            var result = await _controller.GetAsync(userId);

            // Assert
            var resultBadRequest = result as BadRequestResult;
            Assert.IsNotNull(resultBadRequest);
        }

        [TestMethod]
        public async Task GetAsync_ReturnBadRequest_WhenResponseIsNotSuccess()
        {
            // Arrange
            var userId = "1";

            // Configure the mock
            _mockUserApplication.Setup(x => x.GetAsync(userId, default))
                               .ReturnsAsync(new Response<UserResponseDTO>
                               {
                                   IsSuccess = false
                               });

            // Act
            var result = await _controller.GetAsync(userId);

            // Assert
            var resultBadRequest = result as BadRequestObjectResult;
            Assert.IsNotNull(resultBadRequest);
            Assert.AreEqual(400, resultBadRequest.StatusCode);
            Assert.IsNotNull(resultBadRequest.Value);
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnOK_WhenUserExistsAsync()
        {
            // Arrange
            var userId = "1";

            // Configure the mock
            _mockUserApplication.Setup(x => x.GetAllAsync(default))
                               .ReturnsAsync(new Response<IEnumerable<UserResponseDTO>>
                               {
                                   IsSuccess = true,
                                   Data = new List<UserResponseDTO>
                                   {
                                       new UserResponseDTO
                                       {
                                           UserId = 1,
                                           UserName = "test"
                                       }
                                   }
                               });

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(Response<IEnumerable<UserResponseDTO>>));
            Assert.AreEqual(userId, ((Response<IEnumerable<UserResponseDTO>>)okResult.Value).Data.FirstOrDefault().UserId.ToString());
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnBadRequest_WhenResponseIsNotSuccess()
        {
            // Configure the mock
            _mockUserApplication.Setup(x => x.GetAllAsync(default))
                               .ReturnsAsync(new Response<IEnumerable<UserResponseDTO>> { IsSuccess = false });

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            var resultBadRequest = result as BadRequestObjectResult;
            Assert.IsNotNull(resultBadRequest);
            Assert.AreEqual(400, resultBadRequest.StatusCode);
            Assert.IsNotNull(resultBadRequest.Value);
        }
    }
}
