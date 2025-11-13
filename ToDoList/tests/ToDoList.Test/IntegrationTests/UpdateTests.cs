namespace ToDoList.Test.IntegrationTests;

using System.ComponentModel;
using NuGet.Frameworks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Domain.DTOs;
using ToDoList.WebApi;
using Xunit;
public class UpdateTests
{
    [Fact]
    public async Task Update_ChangesItemDescriptionCorrectly()
    {
        // Arrange
        await using var context = TestUtils.TestDbContextFactory.CreateTestDbContext();

        var originalItem = new ToDoItem
        {
            Name = "Original Task",
            Description = "Initial description",
            IsCompleted = false
        };

        context.ToDoItems.Add(originalItem);
        await context.SaveChangesAsync();

        var controller = new ToDoItemsController(context: context, repository: null); //if nepoužívám mock tak jen 1 parametr (context)

        var updateDto = new ToDoItemUpdateRequestDto(
            Name: originalItem.Name,
            Description: "Updated description",
            IsCompleted: originalItem.IsCompleted
        );

        // Act
        var result = await controller.UpdateById(originalItem.ToDoItemId, updateDto);

        // Assert
        Assert.IsType<NoContentResult>(result);

        var updatedItem = await context.ToDoItems.FindAsync(originalItem.ToDoItemId);
        Assert.NotNull(updatedItem);
        Assert.Equal("Updated description", updatedItem.Description);
    }
}

// [Fact]
// public void Update_ReturnsCorrectItemDescriptionAfterUpdate()
// {
//     //Arrange
//     var toDoItem1 = new ToDoItem()
//     {
//         ToDoItemId = 1,
//         Name = "Jmeno1",
//         Description = "Popis1",
//         IsCompleted = false
//     };
//     var toDoItem2 = new ToDoItem()
//     {
//         ToDoItemId = 2,
//         Name = "Jmeno2",
//         Description = "Popis2",
//         IsCompleted = true
//     };
//     var controller = new ToDoItemsController();
//     controller.AddItemToStorage(toDoItem1);
//     controller.AddItemToStorage(toDoItem2);

//     var updateDto = new ToDoItemUpdateRequestDto(
//         toDoItem1.Name,
//         "nový popis",
//         toDoItem1.IsCompleted
//     );

//     // Act
//     var updateResult = controller.UpdateById(1, updateDto);
//     var readResult = controller.Read();
//     var okResult = Assert.IsType<OkObjectResult>(readResult.Result);
//     var items = Assert.IsAssignableFrom<IEnumerable<ToDoItemGetResponseDto>>(okResult.Value);

//     // Assert
//     var updatedItem = items.First(i => i.Id == 1);
//     Assert.Equal("nový popis", updatedItem.Description);
// }
//}
