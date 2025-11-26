namespace ToDoList.Test.IntegrationTests;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;

public class GetByIdTests
{
    [Fact]
    public async Task GetById_ValidId_ReturnsItem()
    {
        // Arrange
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);

        var toDoItem = new ToDoItem
        {
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        context.ToDoItems.Add(toDoItem);
        context.SaveChangesAsync();

        // Act
        var result = await controller.ReadById(toDoItem.ToDoItemId);
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);

        Assert.Equal(toDoItem.ToDoItemId, value.Id);
        Assert.Equal(toDoItem.Description, value.Description);
        Assert.Equal(toDoItem.IsCompleted, value.IsCompleted);
        Assert.Equal(toDoItem.Name, value.Name);

        // Cleanup
        context.ToDoItems.Remove(toDoItem);
        context.SaveChangesAsync();
    }

    [Fact]
    public async Task GetById_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);

        // Act
        var invalidId = -1;
        var result = await controller.ReadById(invalidId);
        var resultResult = result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);
    }
}

