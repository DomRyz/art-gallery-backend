using Xunit;
using Moq;
using Art_Gallery_Backend.Models.Database_Models;
using Art_Gallery_Backend.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class ExhibitionControllerTests
{
    private readonly Mock<IExhibitionDataAccess> _exhibitionRepoMock;
    private readonly Mock<IArtworkDataAccess> _artworkRepoMock;

    private readonly ExhibitionController _exhibitionController;

    public ExhibitionControllerTests()
    {
        _exhibitionRepoMock = new Mock<IExhibitionDataAccess>();
        _artworkRepoMock = new Mock<IArtworkDataAccess>();

        _exhibitionController = new ExhibitionController(_exhibitionRepoMock.Object, _artworkRepoMock.Object);
    }

    [Fact]
    public void GetExhibitions_ReturnsListOfExhibitions()
    {
        // Arrange
        var expectedExhibitions = new List<Exhibition>();

        _exhibitionRepoMock.Setup(repo => repo.GetExhibitions()).Returns(expectedExhibitions);

        // Act
        var result = _exhibitionController.GetExhibitions();

        // Assert
        Assert.Equal(expectedExhibitions, result);
    }

    [Fact]
    public void GetExhibitionById_ValidId_ReturnsExhibition()
    {
        // Arrange
        var exhibitionId = 1;
        var expectedExhibition = new Exhibition { Id = exhibitionId };

        _exhibitionRepoMock.Setup(repo => repo.GetExhibitionById(exhibitionId)).Returns(expectedExhibition);

        // Act
        var result = _exhibitionController.GetExhibitionById(exhibitionId);

        // Assert
        Assert.Equal(expectedExhibition, result);
    }

    [Fact]
    public void GetExhibitionById_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var exhibitionId = 1;

        _exhibitionRepoMock.Setup(repo => repo.GetExhibitionById(exhibitionId)).Returns((Exhibition)null);

        // Act
        var result = _exhibitionController.GetExhibitionById(exhibitionId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    // Add unit tests for other endpoints in a similar manner
    // ...
}
