namespace ToDoList.Test.UnitTests;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;

using NSubstitute;
using ToDoList.Test;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;

public class PutTests
{
    [Fact]
    public void Put_ValidId_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var existingItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        repositoryMock.ReadById(existingItem.ToDoItemId).Returns(existingItem);

        //var controller = new ToDoItemsController(context: null, repository: repositoryMock);
        var controller = new ToDoItemsController(repositoryMock);

        var request = new ToDoItemUpdateRequestDto(
            Name: "Jine jmeno",
            Description: "Jiny popis",
            IsCompleted: true
        );

        // Act
        var result = controller.UpdateById(existingItem.ToDoItemId, request);

        // Assert
        Assert.IsType<NoContentResult>(result);

        // Verify update was called
        repositoryMock.Received(1).Update(Arg.Is<ToDoItem>(item =>
            item.ToDoItemId == existingItem.ToDoItemId &&
            item.Name == request.Name &&
            item.Description == request.Description &&
            item.IsCompleted == request.IsCompleted
        ));
    }
    [Fact]
    public void Put_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        repositoryMock.ReadById(Arg.Any<int>()).Returns((ToDoItem)null);

        //var controller = new ToDoItemsController(context: null, repository: repositoryMock);
        var controller = new ToDoItemsController(repository: repositoryMock);

        var request = new ToDoItemUpdateRequestDto(
            Name: "Jine jmeno",
            Description: "Jiny popis",
            IsCompleted: true
        );

        // Act
        var result = controller.UpdateById(-1, request);

        // Assert
        Assert.IsAssignableFrom<NotFoundResult>(result);
    }



    // public class PutTests
    // {
    //     [Fact]
    //     public void Put_ValidId_ReturnsNoContent()
    //     {
    //         // Arrange
    //         var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
    //         using var context = new ToDoItemsContext(connectionString);
    //         var controller = new ToDoItemsController(context: context, repository: null);

    //         var toDoItem = new ToDoItem
    //         {
    //             Name = "Jmeno",
    //             Description = "Popis",
    //             IsCompleted = false
    //         };
    //         context.ToDoItems.Add(toDoItem);
    //         context.SaveChanges();

    //         var request = new ToDoItemUpdateRequestDto(
    //             Name: "Jine jmeno",
    //             Description: "Jiny popis",
    //             IsCompleted: true
    //         );

    //         // Act
    //         var result = controller.UpdateById(toDoItem.ToDoItemId, request);

    //         // Assert
    //         Assert.IsType<NoContentResult>(result);

    //         // Cleanup
    //         context.ToDoItems.Remove(toDoItem);
    //         context.SaveChanges();
    //     }

    //     [Fact]
    //     public void Put_InvalidId_ReturnsNotFound()
    //     {
    //         // Arrange
    //         var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
    //         using var context = new ToDoItemsContext(connectionString);
    //         var controller = new ToDoItemsController(context: context, repository: null);

    //         var request = new ToDoItemUpdateRequestDto(
    //             Name: "Jine jmeno",
    //             Description: "Jiny popis",
    //             IsCompleted: true
    //         );

    //         // Act
    //         var invalidId = -1;
    //         var result = controller.UpdateById(invalidId, request);

    //         // Assert
    //         Assert.IsType<NotFoundResult>(result);
    //     }

}

