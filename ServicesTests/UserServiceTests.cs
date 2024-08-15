using Moq;
using Services.Models;
using Services.Services.Implementation;
using Services.Services.Interfaces;

namespace Services;

public class UserServiceTests
{
    private IUserService _userService;
    private User _user;

    [SetUp]
    public void Setup()
    {
        _user = new User
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            Email = "john_doe@mail.com",
            Password = "password",
        };

        var mockUserRepository = new Mock<IUserRepository>();
        // Setup Register method
        mockUserRepository.Setup(x => x.Register(It.IsAny<User>()))
            .ReturnsAsync(_user);

        // Setup Login method
        mockUserRepository.Setup(x => x.Login(It.IsAny<LoginModel>()))
            .ReturnsAsync(true);

        // Setup GetUsers method
        var users = new List<User> { _user };
        mockUserRepository.Setup(x => x.GetUsers())
            .ReturnsAsync(users);

        // Setup GetUserById method
        mockUserRepository.Setup(x => x.GetUserById(_user.Id))
            .ReturnsAsync(_user);
        _userService = new UserService(mockUserRepository.Object);
    }

    // Test for Register method
    [Test]
    public async Task Register_WhenCalled_ReturnsUser()
    {
        // Arrange
        var user = _user;

        // Act
        var result = await _userService.Register(user);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(user.Id), "User Id is not equal");
            Assert.That(result.Email, Is.EqualTo(user.Email), "User Email is not equal");
            Assert.That(result.Name, Is.EqualTo(user.Name), "User Name is not equal");
        });
    }

    // Test for Login method
    [Test]
    public async Task Login_WhenCalled_ReturnsUser()
    {
        // Arrange
        var user = new LoginModel
        {
            Login = _user.Email,
            Password = _user.Password,
        };

        // Act
        var result = await _userService.Login(user);

        // Assert
        Assert.That(result, Is.True, $"User with email {_user.Email} and password {_user.Password} not found");
    }

    // Test for GetUsers method
    [Test]
    public async Task GetUsers_WhenCalled_ReturnsUsers()
    {
        // Arrange
        var users = new List<User>();

        // Act
        var result = await _userService.GetUsers();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<IEnumerable<User>>());
    }


    // Test for GetUserById method
    [Test]
    public async Task GetUserById_WhenCalled_ReturnsUser()
    {
        // Arrange
        var userId = _user.Id;

        // Act
        var result = await _userService.GetUserById(userId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(_user.Id), "User Id is not equal");
            Assert.That(result.Email, Is.EqualTo(_user.Email), "User Email is not equal");
            Assert.That(result.Name, Is.EqualTo(_user.Name), "User Name is not equal");
        });
    }
}