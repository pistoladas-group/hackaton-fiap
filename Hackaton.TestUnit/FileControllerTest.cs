using System.Net;
using Microsoft.AspNetCore.Mvc;
using Hackaton.Api.Data;
using Hackaton.Api.Controller;

namespace TechNews.Core.Api.Tests;

public class FileControllerTest : IClassFixture<TestsFixture>
{
    private TestsFixture _testsFixture { get; set; }

    public FileControllerTest(TestsFixture testsFixture)
    {
        _testsFixture = testsFixture;
    }

    [Fact]
    public async void GetFileById_ShouldReturnBadRequest_WhenInvalidIdIsProvided()
    {
        //Arrange
        var dbContext = _testsFixture.GetDbContext();
        var controller = new FileController(dbContext);

        //Act
        var response = await controller.GetFileById(Guid.Empty);

        //Assert
        var objectResult = (ObjectResult?)response;
        var apiResponse = _testsFixture.GetApiResponseFromObjectResult(objectResult);

        Assert.Equal((int)HttpStatusCode.BadRequest, objectResult?.StatusCode);
        Assert.Null(apiResponse?.Data);
    }

    [Fact]
    public async void GetFileById_ShouldReturnOkWithData_WhenValidIdIsProvided()
    {
        //Arrange
        var dbContext = _testsFixture.GetDbContext();
        var controller = new FileController(dbContext);

        var id = _testsFixture.AddFileToDbContext();

        //Act
        var response = await controller.GetFileById(id);

        //Assert
        var objectResult = (ObjectResult?)response;
        var apiResponse = _testsFixture.GetApiResponseFromObjectResult(objectResult);
        var apiResponseData = _testsFixture.ConvertDataFromObjectResult<File?>(objectResult);

        Assert.Equal((int)HttpStatusCode.OK, objectResult?.StatusCode);
        Assert.NotNull(apiResponse?.Data);
        Assert.Equal(id, apiResponseData?.Id);
    }

    [Fact]
    public async void GetFileById_ShouldReturnNotFound_WhenNewsDoesNotExists()
    {
        //Arrange
        var dbContext = _testsFixture.GetDbContext();
        var controller = new FileController(dbContext);

        //Act
        var response = await controller.GetFileById(Guid.NewGuid());

        //Assert
        var objectResult = (ObjectResult)response;
        var apiResponse = _testsFixture.GetApiResponseFromObjectResult(objectResult);

        Assert.Equal((int)HttpStatusCode.NotFound, objectResult?.StatusCode);
        Assert.Null(apiResponse?.Data);
    }

    [Fact]
    public async void GetAllFiles_ShouldReturnOk()
    {
        //Arrange
        var dbContext = _testsFixture.GetDbContext();
        var controller = new FileController(dbContext);
        _testsFixture.AddFileToDbContext();

        //Act
        var response = await controller.GetAllFiles(1, 10);

        //Assert
        var objectResult = (ObjectResult?)response;
        var apiResponseData = _testsFixture.ConvertDataFromObjectResult<List<File>?>(objectResult);

        Assert.Equal((int)HttpStatusCode.OK, objectResult?.StatusCode);
        Assert.NotNull(apiResponseData);
    }

    [Fact]
    public async void UploadFile_ShouldReturnOk()
    {
        //Arrange
        var dbContext = _testsFixture.GetDbContext();
        var controller = new FileController(dbContext);
        _testsFixture.AddFileToDbContext();

        //Act
        var response = await controller.GetAllFiles(1,10);

        //Assert
        var objectResult = (ObjectResult?)response;
        var apiResponseData = _testsFixture.ConvertDataFromObjectResult<List<File>?>(objectResult);

        Assert.Equal((int)HttpStatusCode.OK, objectResult?.StatusCode);
        Assert.NotNull(apiResponseData);
    }
}