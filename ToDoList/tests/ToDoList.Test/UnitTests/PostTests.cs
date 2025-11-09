namespace ToDoList.Test.UnitTests;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;

using ToDoList.WebApi;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
//using ToDoList.Test.IntegrationTests; //kvůli ActionResultExtensions.cs ale ten jsem si přesunula do složky výše
using ToDoList.Test; //kvůli ActionResultExtensions.cs

public class PostTests
{
    [Fact]
    public void Post_ValidRequest_ReturnsNewItem()
    {
        // Arrange
        //var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db"; //nepotřebuji if using NSubstitute
        //using var context = new ToDoItemsContext(connectionString); //nepotřebuji if using NSubstitute
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(context: null, repository: repositoryMock);
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        // Act
        var result = controller.Create(request);
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<CreatedAtActionResult>(resultResult);
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
}

