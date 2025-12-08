namespace ToDoList.Test.IntegrationTests;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;

public class PutTests
{
    // [Fact]
    // public void Put_ValidId_ReturnsNoContent()
    // {
    //     // Arrange
    //     var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
    //     using var context = new ToDoItemsContext(connectionString);
    //     var repository = new ToDoItemsRepository(context);
    //     var controller = new ToDoItemsController(repository);

    //     var toDoItem = new ToDoItem
    //     {
    //         Name = "Jmeno",
    //         Description = "Popis",
    //         IsCompleted = false
    //     };
    //     context.ToDoItems.Add(toDoItem);
    //     context.SaveChanges();

    //     var request = new ToDoItemUpdateRequestDto(
    //         Name: "Jine jmeno",
    //         Description: "Jiny popis",
    //         IsCompleted: true
    //     );

    //     // Act
    //     var result = controller.UpdateById(toDoItem.ToDoItemId, request);

    //     // Assert
    //     Assert.IsType<NoContentResult>(result);

    //     // Cleanup
    //     context.ToDoItems.Remove(toDoItem);
    //     context.SaveChanges();
    // }

    [Fact]
    public async Task Put_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);

        var request = new ToDoItemUpdateRequestDto(
            Name: "Jine jmeno",
            Description: "Jiny popis",
            IsCompleted: true,
            Category: null
        );

        // Act
        var invalidId = -1;
        var result = await controller.UpdateById(invalidId, request);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task Put_ValidId_ReturnsNoContent()
    {
        // Arrange
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);

        //context.Database.EnsureDeleted();
        //context.Database.EnsureCreated();

        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);

        var item = new ToDoItem
        {
            Name = "Původní jméno",
            Description = "Původní popis",
            IsCompleted = false,
            Category = null
        };
        context.ToDoItems.Add(item);
        await context.SaveChangesAsync();

        var request = new ToDoItemUpdateRequestDto(
            Name: "Nové jméno",
            Description: "Nový popis",
            IsCompleted: true,
            Category: "TestCategory"
        );

        // Act
        var result = await controller.UpdateById(item.ToDoItemId, request);

        // Assert
        Assert.IsType<NoContentResult>(result);

        var updated = await context.ToDoItems.FindAsync(item.ToDoItemId);
        Assert.Equal("Nové jméno", updated.Name);
        Assert.Equal("Nový popis", updated.Description);
        Assert.True(updated.IsCompleted);
        Assert.Equal("TestCategory", updated.Category);

        // Cleanup
        context.ToDoItems.Remove(updated);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task Put_ChangesItemCorrectly()
    {
        // Arrange
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);

        //context.Database.EnsureDeleted();
        //context.Database.EnsureCreated();

        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);

        var item = new ToDoItem
        {
            Name = "Původní jméno",
            Description = "Původní popis",
            IsCompleted = false,
            Category = null
        };
        context.ToDoItems.Add(item);
        await context.SaveChangesAsync();

        var request = new ToDoItemUpdateRequestDto(
            Name: "Nové jméno",
            Description: "Nový popis",
            IsCompleted: true,
            Category: "TestCategory"
        );

        // Act
        var result = await controller.UpdateById(item.ToDoItemId, request);

        // Assert
        var updated = await context.ToDoItems.FindAsync(item.ToDoItemId);
        Assert.Equal("Nové jméno", updated.Name);
        Assert.Equal("Nový popis", updated.Description);
        Assert.True(updated.IsCompleted);
        Assert.Equal("TestCategory", updated.Category);

        // Cleanup
        context.ToDoItems.Remove(updated);
        await context.SaveChangesAsync();
    }
}

// namespace ToDoList.Test.IntegrationTests;

// using System.ComponentModel;
// using NuGet.Frameworks;
// using System.Linq;
// using Microsoft.AspNetCore.Mvc;
// using ToDoList.Domain.Models;
// using ToDoList.Domain.DTOs;
// using ToDoList.WebApi;
// using Xunit;
// public class UpdateTests
// {
//     [Fact]
//     public async Task Update_ChangesItemDescriptionCorrectly()
//     {
//         // Arrange
//         await using var context = TestUtils.TestDbContextFactory.CreateTestDbContext();

//         var originalItem = new ToDoItem
//         {
//             Name = "Original Task",
//             Description = "Initial description",
//             IsCompleted = false
//         };

//         context.ToDoItems.Add(originalItem);
//         await context.SaveChangesAsync();

//         var controller = new ToDoItemsController(context: context, repository: null); //if nepoužívám mock tak jen 1 parametr (context)

//         var updateDto = new ToDoItemUpdateRequestDto(
//             Name: originalItem.Name,
//             Description: "Updated description",
//             IsCompleted: originalItem.IsCompleted
//         );

//         // Act
//         var result = await controller.UpdateById(originalItem.ToDoItemId, updateDto);

//         // Assert
//         Assert.IsType<NoContentResult>(result);

//         var updatedItem = await context.ToDoItems.FindAsync(originalItem.ToDoItemId);
//         Assert.NotNull(updatedItem);
//         Assert.Equal("Updated description", updatedItem.Description);
//     }
// }

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
