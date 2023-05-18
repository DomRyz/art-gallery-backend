using NUnit.Framework;
using Moq;

[TestFixture]
public class UserControllerTests
{
    private UserController _controller;
    private Mock<IUserDataAccess> _mockUserDataAccess;

    [SetUp]
    public void Setup()
    {
        _mockUserDataAccess = new Mock<IUserDataAccess>();
        _controller = new UserController(_mockUserDataAccess.Object);
    }

    [Test]
    public void GetUsers_WhenCalled_ReturnsListOfUsers()
    {
        // Arrange
        var expectedUsers = new List<User>();
        _mockUserDataAccess.Setup(repo => repo.GetUsers()).Returns(expectedUsers);

        // Act
        var result = _controller.GetUsers();

        // Assert
        Assert.That(result, Is.EqualTo(expectedUsers));
    }

    [Test]
    public void GetUserById_ExistingId_ReturnsUser()
    {
        // Arrange
        int id = 1;
        var expectedUser = new User { Id = id };
        _mockUserDataAccess.Setup(repo => repo.GetUserById(id)).Returns(expectedUser);

        // Act
        var result = _controller.GetUserById(id);

        // Assert
        Assert.That(result, Is.EqualTo(expectedUser));
    }

    [Test]
    public void GetUserById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        int id = 1;
        _mockUserDataAccess.Setup(repo => repo.GetUserById(id)).Returns((User)null);

        // Act
        var result = _controller.GetUserById(id);

        // Assert
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void CreateUser_ValidUser_ReturnsCreatedUser()
    {
        // Arrange
        var userInput = new UserInputDto { /* Set valid user properties */ };
        var expectedUser = new User { /* Set expected user properties */ };
        _mockUserDataAccess.Setup(repo => repo.InsertUser(userInput)).Returns(expectedUser);

        // Act
        var result = _controller.CreateUser(userInput);

        // Assert
        Assert.That(result, Is.TypeOf<CreatedAtActionResult>());
        var createdResult = (CreatedAtActionResult)result;
        Assert.That(createdResult.Value, Is.EqualTo(expectedUser));
    }

    // Add more tests for other endpoints...

    // Example test for the signup endpoint
    [Test]
    public void Signup_ValidUser_ReturnsCreatedUser()
    {
        // Arrange
        var userInput = new UserInputDto { /* Set valid user properties */ };
        var expectedUser = new User { /* Set expected user properties */ };
        _mockUserDataAccess.Setup(repo => repo.InsertUser(userInput)).Returns(expectedUser);

        // Act
        var result = _controller.Signup(userInput);

        // Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var okResult = (OkObjectResult)result;
        Assert.That(okResult.Value, Is.EqualTo(expectedUser));
    }
}
