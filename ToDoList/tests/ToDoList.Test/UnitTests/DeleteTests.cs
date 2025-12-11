namespace ToDoList.Test.UnitTests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using Microsoft.AspNetCore.Http;
using NSubstitute.ExceptionExtensions;

public class DeleteTests
{
    [Fact]
    public async Task Delete_ValidItemId_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns(new ToDoItem { Name = "testItem", Description = "testDescription", IsCompleted = false, Category="testCategory" });
        var someId = 1;

        // Act
        var result = await controller.DeleteById(someId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        await repositoryMock.Received(1).ReadByIdAsync(someId);
        await repositoryMock.Received(1).DeleteByIdAsync(someId);
    }

    [Fact]
    public async Task Delete_InvalidItemId_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns(null as ToDoItem);
        var someId = 1;

        // Act
        var result = await controller.DeleteById(someId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        repositoryMock.Received(1).ReadByIdAsync(someId);
        repositoryMock.Received(0).DeleteByIdAsync(Arg.Any<int>()); // nothing was deleted
    }

    [Fact]
    public async Task Delete_AnyItemIdExceptionOccurredDuringReadById_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Throws(new Exception());
        var someId = 1;

        // Act
        var result = await controller.DeleteById(someId);

        // Assert
        Assert.IsType<ObjectResult>(result);
        await repositoryMock.Received(1).ReadByIdAsync(someId);
        Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
    }

    [Fact]
    public async Task Delete_AnyItemIdExceptionOccurredDuringDeleteById_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns(new ToDoItem { Name = "testItem", Description = "testDescription", IsCompleted = false, Category="testCategory" });
        repositoryMock.When(r => r.DeleteByIdAsync(Arg.Any<int>())).Do(r => throw new Exception());
        var someId = 1;

        // Act
        var result = await controller.DeleteById(someId);

        // Assert
        Assert.IsType<ObjectResult>(result);
        await repositoryMock.Received(1).ReadByIdAsync(someId);
        await repositoryMock.Received(1).DeleteByIdAsync(someId);
        Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
    }
}

