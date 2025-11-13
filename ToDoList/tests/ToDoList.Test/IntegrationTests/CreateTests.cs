namespace ToDoList.Test.IntegrationTests;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;
using Xunit;
using NSubstitute;

public class CreateTests
{

    [Fact]
    public async Task Create_AddsItemToTestDatabase()
    {
        // Arrange
        await using var context = TestUtils.TestDbContextFactory.CreateTestDbContext();
        var controller = new ToDoItemsController(context: context, repository: null); //if nepoužívám mock tak jen 1 parametr (context)

        var request = new ToDoItemCreateRequestDto("Test", "Popis", false);
        // Act
        var result = await controller.Create(request);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        var dto = Assert.IsType<ToDoItemGetResponseDto>(created.Value);
        // Assert
        Assert.Equal("Test", dto.Name);
        // Cleanup
        context.ToDoItems.RemoveRange(context.ToDoItems);
        await context.SaveChangesAsync();
    }


    [Fact]
    public async Task Create_ReturnsBadRequest_WhenNameIsNull()
    {
        // Arrange
        await using var context = TestUtils.TestDbContextFactory.CreateTestDbContext();
        var controller = new ToDoItemsController(context: context, repository: null);

        var request = new ToDoItemCreateRequestDto(
            Name: null!,
            Description: "Valid description",
            IsCompleted: false
        );

        // Act
        var result = await controller.Create(request);
        var resultResult = result.Result;

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(resultResult);
        Assert.Equal("Name and Description are required.", badRequest.Value);

        // Cleanup
        context.ToDoItems.RemoveRange(context.ToDoItems);
        await context.SaveChangesAsync();
    }
}

// [Fact]
// public void Create_ReturnsBadRequest_WhenNameIsNull()
// {
//     // Arrange
//     var controller = new ToDoItemsController();
//     var requestDto = new ToDoItemCreateRequestDto(
//         Name: null!,
//         Description: "Missing name",
//         IsCompleted: false
//     );

//     // Act
//     var result = controller.Create(requestDto);

//     // Assert
//     Assert.IsType<BadRequestObjectResult>(result.Result);
// }
// [Fact]
// public void Create_ReturnsBadRequest_WhenDescriptionIsEmpty()
// {
//     // Arrange
//     var controller = new ToDoItemsController();
//     var requestDto = new ToDoItemCreateRequestDto(
//         Name: "Valid Name",
//         Description: "",
//         IsCompleted: false
//     );

//     // Act
//     var result = controller.Create(requestDto);

//     // Assert
//     Assert.IsType<BadRequestObjectResult>(result.Result);
// }
// [Fact]
// public void Create_ReturnsProblem_WhenInternalErrorOccurs()
// {
//     // Arrange
//     var controller = new ToDoItemsController();

//     // Simulate internal error by passing a request that causes exception (e.g., null DTO)
//     ToDoItemCreateRequestDto requestDto = null!;

//     // Act
//     var result = controller.Create(requestDto);

//     // Assert
//     var problemResult = Assert.IsType<ObjectResult>(result.Result);
//     Assert.Equal(500, problemResult.StatusCode);
// }

//[Fact]
// public void Create_ReturnsCreatedItemWithCorrectValues()
// {
//     // Arrange
//     var controller = new ToDoItemsController();
//     var requestDto = new ToDoItemCreateRequestDto(
//         Name: "Test Task",
//         Description: "This is a test task",
//         IsCompleted: false
//     );

//     // Act
//     var result = controller.Create(requestDto);
//     var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
//     var responseDto = Assert.IsType<ToDoItemGetResponseDto>(createdResult.Value);

//     // Assert
//     Assert.Equal("Test Task", responseDto.Name);
//     Assert.Equal("This is a test task", responseDto.Description);
//     Assert.False(responseDto.IsCompleted);
//     Assert.True(responseDto.Id > 0); // ID should be assigned
// }

//}
