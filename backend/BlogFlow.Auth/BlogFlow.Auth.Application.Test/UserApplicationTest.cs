using AutoMapper;
using BlogFlow.Auth.Application.DTO;
using BlogFlow.Common.Application.Interface.Persistence;
using BlogFlow.Auth.Application.UseCases.Common.Mappings;
using BlogFlow.Auth.Application.UseCases.Users;
using BlogFlow.Auth.Domain.Entities;
using Moq;

namespace BlogFlow.Auth.Application.Test;

[TestClass]
public class UserApplicationTest
{
    private UsersApplication _userApplication;
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private IMapper _mapper;

    [TestInitialize]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = config.CreateMapper();

        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _userApplication = new UsersApplication(_mockUnitOfWork.Object, _mapper);
    }

    [TestMethod]
    public async Task Authenticate_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var userName = "test";
        var password = "test";
        var user = new User
        {
            UserName = userName,
            Password = password
        };

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.Authenticate(userName, password, default)).ReturnsAsync(user);

        // Act
        var response = await _userApplication.Authenticate(userName, password);

        // Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(user.UserName, response.Data.UserName);
    }

    [TestMethod]
    public async Task Authenticate_ReturnsSuccessNull_WhenUserDoesntExist()
    {
        // Arrange
        var userName = "test";
        var password = "test";

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.Authenticate(userName, password, default)).ReturnsAsync((User)null);

        // Act
        var response = await _userApplication.Authenticate(userName, password);

        // Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsNull(response.Data);
    }

    [TestMethod]
    public async Task Authenticate_ReturnNotSuccessNull_WhenRepositoryThrowInvalidOperationException()
    {
        // Arrange
        var userName = "test";
        var password = "test";

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.Authenticate(userName, password, default)).ThrowsAsync(new InvalidOperationException());

        // Act
        var response = await _userApplication.Authenticate(userName, password);

        // Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
    }

    [TestMethod]
    public async Task Authenticate_ReturnNotSuccessNull_WhenRepositoryThrowException()
    {
        // Arrange
        var userName = "test";
        var password = "test";

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.Authenticate(userName, password, default)).ThrowsAsync(new Exception());

        // Act
        var response = await _userApplication.Authenticate(userName, password);

        // Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
    }

    [TestMethod]
    public async Task CountAsync_ReturnsCount_WhenCountUsers()
    {
        // Arrange
        var countUsers = 10;

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.CountAsync(default)).ReturnsAsync(countUsers);

        // Act
        var response = await _userApplication.CountAsync();

        // Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(countUsers, response.Data);
    }

    [TestMethod]
    public async Task CountAsync_ReturnsNotSuccess_WhenRepositoryThrowException()
    {
        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.CountAsync(default)).ThrowsAsync(new Exception());

        // Act
        var response = await _userApplication.CountAsync();

        // Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.AreEqual(0, response.Data);
    }

    [TestMethod]
    public async Task DeleteAsync_ReturnOk_WhenDeleteUser()
    {
        // Arrange
        var id = "1";

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.DeleteAsync(id)).Returns(Task.FromResult(true));
        _mockUnitOfWork.Setup(x => x.Save(default)).ReturnsAsync(1);

        // Act
        var response = await _userApplication.DeleteAsync(id);

        // Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data);
    }

    [TestMethod]
    public async Task DeleteAsync_ReturnNotOk_WhenRepositoryThrowException()
    {
        // Arrange
        var id = "1";

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.DeleteAsync(id)).ThrowsAsync(new Exception());

        // Act
        var response = await _userApplication.DeleteAsync(id);

        // Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task DeleteAsync_ReturnNotOk_WhenRepositoryThrowInvalidOperationException()
    {
        // Arrange
        var id = "1";

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.DeleteAsync(id)).ThrowsAsync(new InvalidOperationException());

        // Act
        var response = await _userApplication.DeleteAsync(id);

        // Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task DeleteAsync_ReturnNotOk_WhenSaveReturnZero()
    {
        // Arrange
        var id = "1";

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.DeleteAsync(id)).Returns(Task.FromResult(true));
        _mockUnitOfWork.Setup(x => x.Save(default)).ReturnsAsync(0);

        // Act
        var response = await _userApplication.DeleteAsync(id);

        // Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task DeleteAsync_ReturnNotOk_WhenUserDoesntExist()
    {
        // Arrange
        var id = "1";

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.DeleteAsync(id)).Returns(Task.FromResult(false));

        // Act
        var response = await _userApplication.DeleteAsync(id);

        // Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task InsertAsync_ReturnsOk_WhenInsertUser()
    {
        // Arrange
        var userDto = new UserDTO
        {
            UserId = 1,
            UserName = "test",
            Password = "test",
            FirstName = "test",
            LastName = "test"
        };

        var user = new User
        {
            UserId = 1,
            UserName = "test",
            Password = "test",
            FirstName = "test",
            LastName = "test"
        };

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.InsertAsync(It.IsAny<User>())).Returns(Task.FromResult(true));
        _mockUnitOfWork.Setup(x => x.Save(default)).ReturnsAsync(1);

        // Act
        var response = await _userApplication.InsertAsync(userDto);

        // Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data);
    }

    [TestMethod]
    public async Task InsertAsync_ReturnsNotSuccess_WhenRepositoryThrowException()
    {
        // Arrange
        var userDto = new UserDTO
        {
            UserId = 1,
            UserName = "test",
            Password = "test",
            FirstName = "test",
            LastName = "test"
        };

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.InsertAsync(It.IsAny<User>())).ThrowsAsync(new Exception());

        // Act
        var response = await _userApplication.InsertAsync(userDto);

        // Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task InsertAsync_ReturnsNotSuccess_WhenSaveReturnZero()
    {
        // Arrange
        var userDto = new UserDTO
        {
            UserId = 1,
            UserName = "test",
            Password = "test",
            FirstName = "test",
            LastName = "test"
        };

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.InsertAsync(It.IsAny<User>())).Returns(Task.FromResult(true));
        _mockUnitOfWork.Setup(x => x.Save(default)).ReturnsAsync(0);

        // Act
        var response = await _userApplication.InsertAsync(userDto);

        // Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task UpdateAsync_ReturnsOk_WhenUpdateUser()
    {
        // Asserts
        int id = 1;
        var userDto = new UserDTO
        {
            UserId = id,
            UserName = "test",
            Password = "test",
            FirstName = "test",
            LastName = "test"
        };
        var user = new User
        {
            UserId = id,
            UserName = "test",
            Password = "test",
            FirstName = "test",
            LastName = "test"
        };

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.GetAsync(id.ToString(), default)).Returns(Task.FromResult(user));
        _mockUnitOfWork.Setup(x => x.Users.UpdateAsync(It.IsAny<User>())).Returns(Task.FromResult(true));
        _mockUnitOfWork.Setup(x => x.Save(default)).ReturnsAsync(1);

        // Act
        var response = await _userApplication.UpdateAsync(id.ToString(), userDto);

        // Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data);
    }

    [TestMethod]
    public async Task UpdateAsync_ReturnsNotSuccess_WhenUserDoesntExist()
    {
        // Arrange
        int id = 1;
        var userDto = new UserDTO
        {
            UserId = id,
            UserName = "test",
            Password = "test",
            FirstName = "test",
            LastName = "test"
        };

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.UpdateAsync(It.IsAny<User>())).Returns(Task.FromResult(true));

        // Act
        var response = await _userApplication.UpdateAsync(id.ToString(), userDto);

        // Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task UpdateAsync_ReturnsNotSuccess_WhenRepositoryThrowException()
    {
        // Arrange
        int id = 1;
        var userDto = new UserDTO
        {
            UserId = id,
            UserName = "test",
            Password = "test",
            FirstName = "test",
            LastName = "test"
        };

        var user = new User
        {
            UserId = id,
            UserName = "test",
            Password = "test",
            FirstName = "test",
            LastName = "test"
        };

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.GetAsync(id.ToString(), default)).Returns(Task.FromResult(user));
        _mockUnitOfWork.Setup(x => x.Users.UpdateAsync(It.IsAny<User>())).ThrowsAsync(new Exception());

        // Act
        var response = await _userApplication.UpdateAsync(id.ToString(), userDto);

        // Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task UpdateAsync_ReturnsNotSuccess_WhenSaveReturnZero()
    {
        // Arrange
        int id = 1;
        var userDto = new UserDTO
        {
            UserId = id,
            UserName = "test",
            Password = "test",
            FirstName = "test",
            LastName = "test"
        };

        var user = new User
        {
            UserId = id,
            UserName = "test",
            Password = "test",
            FirstName = "test",
            LastName = "test"
        };

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.GetAsync(id.ToString(), default)).Returns(Task.FromResult(user));
        _mockUnitOfWork.Setup(x => x.Users.UpdateAsync(It.IsAny<User>())).Returns(Task.FromResult(true));
        _mockUnitOfWork.Setup(x => x.Save(default)).ReturnsAsync(0);

        // Act
        var response = await _userApplication.UpdateAsync(id.ToString(), userDto);

        // Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task GetAllAsync_ReturnAllUsers()
    {
        // Arrange
        var userDtos = new List<UserDTO>()
        {
            new UserDTO { UserId = 1, UserName = "test", FirstName = "test", LastName = "test" },
            new UserDTO { UserId = 2, UserName = "test", FirstName= "test", LastName = "test" },
            new UserDTO { UserId = 3, UserName = "test", FirstName= "test", LastName = "test" }
        };

        var users = new List<User>()
        {
            new User { UserId = 1, UserName = "test", FirstName = "test", LastName = "test" },
            new User { UserId = 2, UserName = "test", FirstName= "test", LastName = "test" },
            new User { UserId = 3, UserName = "test", FirstName= "test", LastName = "test" }
        };

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.GetAllAsync(default)).Returns(Task.FromResult(users as IEnumerable<User>));

        // Act
        var response = await _userApplication.GetAllAsync();

        // Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(userDtos.Count, response.Data.Count());
    }

    [TestMethod]
    public async Task GetAllAsync_ReturnNotSuccess_WhenRepositoryThrowException()
    {
        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.GetAllAsync(default)).ThrowsAsync(new Exception());

        // Act
        var response = await _userApplication.GetAllAsync();

        // Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
    }

    [TestMethod]
    public async Task GetAsync_ReturnUser_WhenUserIdExist()
    {
        // Arrange
        Task<User> user = Task.FromResult(new User { UserId = 1, UserName = "test", FirstName = "test", LastName = "test" });

        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.GetAsync("1", default)).Returns(user);

        // Act
        var response = await _userApplication.GetAsync("1");

        // Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod]
    public async Task GetAsync_ReturnNotSuccess_WhenRepositoryThrowException()
    {
        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.GetAsync("1", default)).ThrowsAsync(new Exception());

        // Act
        var response = await _userApplication.GetAsync("1");

        // Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
    }

    [TestMethod]
    public async Task GetAsync_ReturnNotSuccess_WhenRepositoryThrowInvalidOperationException()
    {
        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.GetAsync("1", default)).ThrowsAsync(new InvalidOperationException());

        // Act
        var response = await _userApplication.GetAsync("1");

        // Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
    }

    [TestMethod]
    public async Task GetAsync_ReturnNotSuccess_WhenUserDoesntExist()
    {
        // Configure the mock
        _mockUnitOfWork.Setup(x => x.Users.GetAsync("1", default)).Returns(Task.FromResult((User)null));

        // Act
        var response = await _userApplication.GetAsync("1");

        // Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
    }
}
