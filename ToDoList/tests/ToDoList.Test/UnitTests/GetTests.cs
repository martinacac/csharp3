namespace ToDoList.Test.UnitTests;

using System.Collections.Generic;
using System.Linq;
using ToDoList.Domain.Models;
using NSubstitute;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;
using ToDoList.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using NSubstitute.ExceptionExtensions;
using Microsoft.AspNetCore.Http;

public class GetTests //Read
{
    [Fact]
    public async Task Get_ReadWhenSomeItemAvailable_ReturnsOk()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock); //odstranit context z controlleru!!!
        //konfigurace mocku
        var someItem = new ToDoItem { Name = "testname", Description = "testDesription", IsCompleted = false };
        repositoryMock.ReadAll().Returns([someItem]); //seznam o jedné položce

        // Act
        var result = await controller.Read();
        //var resultResult = result.Result; //nepotřebuji vědět vnitřek toho co se mi vrátilo

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var value = Assert.IsAssignableFrom<IEnumerable<ToDoItemGetResponseDto>>(okResult.Value);
        Assert.IsType<ActionResult<IEnumerable<ToDoItemGetResponseDto>>>(result);

        repositoryMock.Received().ReadAll(); //ještě zkontrolujeme že se něco zavolalo (že metoda není prázdná)
        repositoryMock.Received(1).ReadAll(); //ještě zkontrolujeme že se něco zavolalo právě jednou

        Assert.Equal(1, value.Count()); //Kontrola počtu položek

        var dto = value.First(); //Kontrola obsahu první položky
        Assert.Equal(someItem.ToDoItemId, dto.Id);
        Assert.Equal(someItem.Name, dto.Name);
        Assert.Equal(someItem.Description, dto.Description);
        Assert.Equal(someItem.IsCompleted, dto.IsCompleted);
    }

    [Fact]
    public async Task Get_ReadWhenNoItemAvailable_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        // Simulate repository returning null (or empty list)
        repositoryMock.ReadAll().Returns((IEnumerable<ToDoItem>)null);

        // Act
        var result = await controller.Read();

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
        repositoryMock.Received(1).ReadAll();
    }

    [Fact]
    public async Task Get_ReadUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        // Simulate an exception when ReadAll is called
        repositoryMock.ReadAll().Throws(new Exception("Unexpected error"));

        // Act
        var result = await controller.Read();

        // Assert
        Assert.IsType<ObjectResult>(result.Result);
        var objectResult = (ObjectResult)result.Result;
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        repositoryMock.Received(1).ReadAll();
    }
    [Fact]
    public async Task Get_ReadByIdWhenSomeItemAvailable_ReturnsOk()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var someId = 1;
        var item = new ToDoItem
        {
            ToDoItemId = someId,
            Name = "TestName",
            Description = "TestDescription",
            IsCompleted = false
        };

        repositoryMock.ReadById(someId).Returns(item);

        // Act
        var result = await controller.ReadById(someId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var value = Assert.IsType<ToDoItemGetResponseDto>(okResult.Value);

        Assert.Equal(item.ToDoItemId, value.Id);
        Assert.Equal(item.Name, value.Name);
        Assert.Equal(item.Description, value.Description);
        Assert.Equal(item.IsCompleted, value.IsCompleted);

        repositoryMock.Received(1).ReadById(someId);
    }
    [Fact]
    public async Task Get_ReadByIdWhenItemIsNull_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var someId = 1;

        // Simulate repository returning null for the given ID
        repositoryMock.ReadById(someId).Returns((ToDoItem)null);

        // Act
        var result = await controller.ReadById(someId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
        repositoryMock.Received(1).ReadById(someId);
    }
    [Fact]
    public async Task Get_ReadByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var someId = 1;

        // Simulate an exception when ReadById is called
        repositoryMock.ReadById(someId).Throws(new Exception("Unexpected error"));

        // Act
        var result = await controller.ReadById(someId);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        repositoryMock.Received(1).ReadById(someId);
    }

}
