namespace ToDoList.Test.UnitTests;

using System.Collections.Generic;
using System.Linq;
using ToDoList.Domain.Models;
using NSubstitute;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;
using ToDoList.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;


public class GetTests //Read
{
    [Fact]
    public void Get_ReadWhenSomeItemAvailable_ReturnsOk()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock); //odstranit context z controlleru!!!
        //konfigurace mocku
        var someItem = new ToDoItem { Name = "testname", Description = "testDesription", IsCompleted = false };
        repositoryMock.ReadAll().Returns([someItem]); //seznam o jedné položce

        // Act
        var result = controller.Read();
        //var resultResult = result.Result; //nepotřebuji vědět vnitřek toho co se mi vrátilo

        // Assert
        Assert.IsType<ActionResult<IEnumerable<ToDoItemGetResponseDto>>>(result);

        repositoryMock.Received().ReadAll(); //ještě zkontrolujeme že se něco zavolalo (že metoda není prázdná)
        repositoryMock.Received(1).ReadAll(); //ještě zkontrolujeme že se něco zavolalo právě jednou
    }

}
