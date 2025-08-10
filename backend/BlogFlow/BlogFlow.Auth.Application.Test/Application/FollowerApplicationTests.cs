using AutoMapper;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Application.UseCases.Common.Mappings;
using BlogFlow.Core.Application.UseCases.Followers;
using BlogFlow.Core.Domain.Entities;
using Moq;

namespace BlogFlow.Auth.Application.Test.Application;

[TestClass]
public class FollowerApplicationTests
{
    private FollowersApplication _followerApplication = null!;
    private Mock<IUnitOfWork> _mockUnitOfWork = null!;
    private IMapper _mapper = null!;

    [TestInitialize]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = config.CreateMapper();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _followerApplication = new FollowersApplication(_mockUnitOfWork.Object, _mapper);
    }

    /// <summary>
    /// Helper to create a mock Follower entity with navigation properties and distinct IDs.
    /// </summary>
    private Follower CreateMockFollower(int followerId = 1, int userId = 2, int blogId = 3)
    {
        return new Follower
        {
            Id = followerId,
            UserId = userId,
            User = new User
            {
                UserId = userId,
                UserName = $"user{userId}",
                FirstName = $"First{userId}",
                LastName = $"Last{userId}",
                Email = $"user{userId}@test.com",
                Password = "password"
            },
            BlogID = blogId,
            Blog = new Blog
            {
                Id = blogId,
                Title = $"Blog{blogId}",
                Description = $"Description{blogId}",
                Category = "Tech",
                UserId = userId,
                User = new User
                {
                    UserId = userId,
                    UserName = $"user{userId}",
                    FirstName = $"First{userId}",
                    LastName = $"Last{userId}",
                    Email = $"user{userId}@test.com",
                    Password = "password"
                },
                ImageId = 1,
                Image = new Image()
            }
        };
    }

    /// <summary>
    /// Helper to create a mock FollowerDTO with navigation properties and distinct IDs.
    /// </summary>
    private FollowerDTO CreateMockFollowerDTO(int followerId = 1, int userId = 2, int blogId = 3)
    {
        return new FollowerDTO
        {
            Id = followerId,
            UserId = userId,
            User = new UserDTO
            {
                UserId = userId,
                UserName = $"user{userId}",
                FirstName = $"First{userId}",
                LastName = $"Last{userId}",
                Email = $"user{userId}@test.com",
                Password = "password"
            },
            BlogID = blogId,
            Blog = new BlogDTO
            {
                Id = blogId,
                Title = $"Blog{blogId}",
                Description = $"Description{blogId}",
                Category = "Tech",
                UserId = userId
            }
        };
    }

    [TestMethod]
    public async Task CountAsync_ReturnsCount_WhenCountFollowers()
    {
        var countFollowers = 5;
        _mockUnitOfWork.Setup(x => x.Followers.CountAsync(default)).ReturnsAsync(countFollowers);

        var response = await _followerApplication.CountAsync();

        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(countFollowers, response.Data);
    }

    [TestMethod]
    public async Task CountAsync_ReturnsNotSuccess_WhenRepositoryThrowsException()
    {
        _mockUnitOfWork.Setup(x => x.Followers.CountAsync(default)).ThrowsAsync(new Exception());

        var response = await _followerApplication.CountAsync();

        Assert.IsFalse(response.IsSuccess);
        Assert.AreEqual(0, response.Data);
    }

    [TestMethod]
    public async Task DeleteAsync_ReturnOk_WhenDeleteFollower()
    {
        var id = "1";
        _mockUnitOfWork.Setup(x => x.Followers.GetAsync(id, default)).ReturnsAsync(CreateMockFollower(1));
        _mockUnitOfWork.Setup(x => x.Followers.DeleteAsync(id)).ReturnsAsync(true);
        _mockUnitOfWork.Setup(x => x.Save(default)).ReturnsAsync(1);

        var response = await _followerApplication.DeleteAsync(id);

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data);
    }

    [TestMethod]
    public async Task DeleteAsync_ReturnNotOk_WhenRepositoryThrowsException()
    {
        var id = "1";
        _mockUnitOfWork.Setup(x => x.Followers.GetAsync(id, default)).ReturnsAsync(CreateMockFollower(1));
        _mockUnitOfWork.Setup(x => x.Followers.DeleteAsync(id)).ThrowsAsync(new Exception());

        var response = await _followerApplication.DeleteAsync(id);

        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task DeleteAsync_ReturnNotOk_WhenRepositoryThrowsInvalidOperationException()
    {
        var id = "1";
        _mockUnitOfWork.Setup(x => x.Followers.GetAsync(id, default)).ReturnsAsync(CreateMockFollower(1));
        _mockUnitOfWork.Setup(x => x.Followers.DeleteAsync(id)).ThrowsAsync(new InvalidOperationException());

        var response = await _followerApplication.DeleteAsync(id);

        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task DeleteAsync_ReturnNotOk_WhenSaveReturnsZero()
    {
        var id = "1";
        _mockUnitOfWork.Setup(x => x.Followers.GetAsync(id, default)).ReturnsAsync(CreateMockFollower(1));
        _mockUnitOfWork.Setup(x => x.Followers.DeleteAsync(id)).ReturnsAsync(true);
        _mockUnitOfWork.Setup(x => x.Save(default)).ReturnsAsync(0);

        var response = await _followerApplication.DeleteAsync(id);

        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task DeleteAsync_ReturnNotOk_WhenFollowerDoesNotExist()
    {
        var id = "1";
        _mockUnitOfWork.Setup(x => x.Followers.GetAsync(id, default)).ReturnsAsync((Follower)null!);

        var response = await _followerApplication.DeleteAsync(id);

        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task InsertAsync_ReturnsOk_WhenInsertFollower()
    {
        var followerDto = CreateMockFollowerDTO(1);

        _mockUnitOfWork.Setup(x => x.Followers.InsertAsync(It.IsAny<Follower>())).ReturnsAsync(true);
        _mockUnitOfWork.Setup(x => x.Save(default)).ReturnsAsync(1);

        var response = await _followerApplication.InsertAsync(followerDto);

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data);
    }

    [TestMethod]
    public async Task InsertAsync_ReturnsNotSuccess_WhenRepositoryThrowsException()
    {
        var followerDto = CreateMockFollowerDTO(1);

        _mockUnitOfWork.Setup(x => x.Followers.InsertAsync(It.IsAny<Follower>())).ThrowsAsync(new Exception());

        var response = await _followerApplication.InsertAsync(followerDto);

        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task InsertAsync_ReturnsNotSuccess_WhenSaveReturnsZero()
    {
        var followerDto = CreateMockFollowerDTO(1);

        _mockUnitOfWork.Setup(x => x.Followers.InsertAsync(It.IsAny<Follower>())).ReturnsAsync(true);
        _mockUnitOfWork.Setup(x => x.Save(default)).ReturnsAsync(0);

        var response = await _followerApplication.InsertAsync(followerDto);

        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task UpdateAsync_ReturnsOk_WhenUpdateFollower()
    {
        var id = "1";
        var followerDto = CreateMockFollowerDTO(1);
        var follower = CreateMockFollower(1);

        _mockUnitOfWork.Setup(x => x.Followers.GetAsync(id, default)).ReturnsAsync(follower);
        _mockUnitOfWork.Setup(x => x.Followers.UpdateAsync(It.IsAny<Follower>())).ReturnsAsync(true);
        _mockUnitOfWork.Setup(x => x.Save(default)).ReturnsAsync(1);

        var response = await _followerApplication.UpdateAsync(id, followerDto);

        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data);
    }

    [TestMethod]
    public async Task UpdateAsync_ReturnsNotSuccess_WhenFollowerDoesNotExist()
    {
        var id = "1";
        var followerDto = CreateMockFollowerDTO(1);

        _mockUnitOfWork.Setup(x => x.Followers.GetAsync(id, default)).ReturnsAsync((Follower)null!);

        var response = await _followerApplication.UpdateAsync(id, followerDto);

        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task UpdateAsync_ReturnsNotSuccess_WhenRepositoryThrowsException()
    {
        var id = "1";
        var followerDto = CreateMockFollowerDTO(1);
        var follower = CreateMockFollower(1);

        _mockUnitOfWork.Setup(x => x.Followers.GetAsync(id, default)).ReturnsAsync(follower);
        _mockUnitOfWork.Setup(x => x.Followers.UpdateAsync(It.IsAny<Follower>())).ThrowsAsync(new Exception());

        var response = await _followerApplication.UpdateAsync(id, followerDto);

        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task UpdateAsync_ReturnsNotSuccess_WhenSaveReturnsZero()
    {
        var id = "1";
        var followerDto = CreateMockFollowerDTO(1);
        var follower = CreateMockFollower(1);

        _mockUnitOfWork.Setup(x => x.Followers.GetAsync(id, default)).ReturnsAsync(follower);
        _mockUnitOfWork.Setup(x => x.Followers.UpdateAsync(It.IsAny<Follower>())).ReturnsAsync(true);
        _mockUnitOfWork.Setup(x => x.Save(default)).ReturnsAsync(0);

        var response = await _followerApplication.UpdateAsync(id, followerDto);

        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
    }

    [TestMethod]
    public async Task GetAllAsync_ReturnAllFollowers()
    {
        var followers = new List<Follower>
        {
            CreateMockFollower(1),
            CreateMockFollower(2),
            CreateMockFollower(3)
        };

        _mockUnitOfWork.Setup(x => x.Followers.GetAllAsync(default)).ReturnsAsync(followers);

        var response = await _followerApplication.GetAllAsync();

        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(followers.Count, response.Data.Count());
    }

    [TestMethod]
    public async Task GetAllAsync_ReturnNotSuccess_WhenRepositoryThrowsException()
    {
        _mockUnitOfWork.Setup(x => x.Followers.GetAllAsync(default)).ThrowsAsync(new Exception());

        var response = await _followerApplication.GetAllAsync();

        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
    }

    [TestMethod]
    public async Task GetAsync_ReturnFollower_WhenFollowerIdExists()
    {
        var follower = CreateMockFollower(1);
        _mockUnitOfWork.Setup(x => x.Followers.GetAsync("1", default)).ReturnsAsync(follower);

        var response = await _followerApplication.GetAsync("1");

        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod]
    public async Task GetAsync_ReturnNotSuccess_WhenRepositoryThrowsException()
    {
        _mockUnitOfWork.Setup(x => x.Followers.GetAsync("1", default)).ThrowsAsync(new Exception());

        var response = await _followerApplication.GetAsync("1");

        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
    }

    [TestMethod]
    public async Task GetAsync_ReturnNotSuccess_WhenRepositoryThrowsInvalidOperationException()
    {
        _mockUnitOfWork.Setup(x => x.Followers.GetAsync("1", default)).ThrowsAsync(new InvalidOperationException());

        var response = await _followerApplication.GetAsync("1");

        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
    }

    [TestMethod]
    public async Task GetAsync_ReturnNotSuccess_WhenFollowerDoesNotExist()
    {
        _mockUnitOfWork.Setup(x => x.Followers.GetAsync("1", default)).ReturnsAsync((Follower)null!);

        var response = await _followerApplication.GetAsync("1");

        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
    }
}
