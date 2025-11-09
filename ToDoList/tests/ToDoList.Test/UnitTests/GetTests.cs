namespace ToDoList.Test.UnitTests;

using System.Collections.Generic;
using System.Linq;
using ToDoList.Domain.Models;
using ToDoList.Persistence;

using NSubstitute;
using ToDoList.Test;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Mvc;

public class GetTests
{
    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();

        var controller = new ToDoItemsController(context: null, repository: repositoryMock);

        var todoItem1 = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno1",
            Description = "Popis1",
            IsCompleted = false
        };
        var todoItem2 = new ToDoItem
        {
            ToDoItemId = 2,
            Name = "Jmeno2",
            Description = "Popis2",
            IsCompleted = true
        };

        //var items = new List<ToDoItem> { todoItem1, todoItem2 };
        repositoryMock.GetAll().Returns(new List<ToDoItem> { todoItem1, todoItem2 });
        //repositoryMock.GetAll().Returns(new List<ToDoItem> { todoItem1, todoItem2 });
        //repositoryMock.Received(1).GetAll();

        // Act
        var result = controller.Read();

        var value = result.Value;

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(2, result.Value.Count());

        var firstToDo = value.First();
        Assert.Equal(todoItem1.ToDoItemId, firstToDo.Id);
        Assert.Equal(todoItem1.Name, firstToDo.Name);
        Assert.Equal(todoItem1.Description, firstToDo.Description);
        Assert.Equal(todoItem1.IsCompleted, firstToDo.IsCompleted);

    }
}

