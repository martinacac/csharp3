namespace ToDoList.Test.UnitTests;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;

using ToDoList.WebApi;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
//using ToDoList.Test.IntegrationTests; //kvůli ActionResultExtensions.cs ale ten jsem si přesunula do složky výše
using ToDoList.Test; //kvůli ActionResultExtensions.cs

using Microsoft.AspNetCore.Http;

public class PostTests //Create
{
    [Fact]
    public async Task Post_CreateValidRequest_ReturnsCreatedAtAction()
    {
        // Arrange
        //var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db"; //nepotřebuji if using NSubstitute
        //using var context = new ToDoItemsContext(connectionString); //nepotřebuji if using NSubstitute
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        //var controller = new ToDoItemsController(context: null, repository: repositoryMock);
        var controller = new ToDoItemsController(repositoryMock);
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        // Act
        //var result = controller.Create(request);
        var result = await controller.Create(request);
        //var resultResult = result.Result;
        //var value = result.GetValue();

        // Assert
        var resultResult = Assert.IsType<CreatedAtActionResult>(result.Result); //kontrola výsledku
        //Assert.IsType<CreatedAtActionResult>(resultResult);
        var value = Assert.IsType<ToDoItemGetResponseDto>(resultResult.Value); //získej DTO
        Assert.NotNull(value);

        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.IsCompleted, value.IsCompleted);
        Assert.Equal(request.Name, value.Name);

        // Cleanup - if using NSubstitute - nepotřebuji cleanup protože nepracuji s DB
        // var createdItem = context.ToDoItems.Find(value.Id);
        // if (createdItem != null)
        // {
        //     context.ToDoItems.Remove(createdItem);
        //     context.SaveChanges();
        // }
    }

    [Fact]
    public async Task Post_CreateUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        // Simulate exception during repository.Create
        repositoryMock
            .When(r => r.CreateAsync(Arg.Any<ToDoItem>()))
            .Do(_ => throw new Exception("Unexpected error"));

        // Act
        var result = await controller.Create(request); //var result = controller.Create(request);
        var resultResult = result.Result;

        // Assert
        //Assert.IsType<ObjectResult>(resultResult);
        //var objectResult = (ObjectResult)resultResult;

        var objectResult = Assert.IsType<ObjectResult>(resultResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
    }

}

