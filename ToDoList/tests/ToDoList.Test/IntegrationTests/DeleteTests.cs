namespace ToDoList.Test.IntegrationTests;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;

public class DeleteTests
{
    [Fact]
    public async Task Delete_ValidId_ReturnsNoContent()
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
            IsCompleted = false,
            Category= "Kategorie"
        };
        context.ToDoItems.Add(toDoItem);
        await context.SaveChangesAsync();

        // Act
        var result = await controller.DeleteById(toDoItem.ToDoItemId);

        // Assert
        Assert.IsType<NoContentResult>(result);

        // Verify item was deleted
        var deletedItem = context.ToDoItems.Find(toDoItem.ToDoItemId);
        Assert.Null(deletedItem);
    }

    [Fact]
    public async Task Delete_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);

        // Act
        var invalidId = -1;
        var result = await controller.DeleteById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}

// namespace ToDoList.Test.IntegrationTests;

// using ToDoList.Domain.Models;
// using ToDoList.WebApi;
// using Microsoft.AspNetCore.Mvc;

// public class DeleteTests
// {
//     [Fact]
//     public async Task Delete_RemovesItemFromDatabase()
//     {
//         // Arrange
//         var context = TestUtils.TestDbContextFactory.CreateTestDbContext();

//         var item = new ToDoItem
//         {
//             Name = "Task to delete",
//             Description = "This will be removed",
//             IsCompleted = false
//         };

//         context.ToDoItems.Add(item);
//         await context.SaveChangesAsync();

//         var controller = new ToDoItemsController(context: context, repository: null); //if nepoužívám mock tak jen 1 parametr (context)

//         // Act
//         var result = await controller.DeleteById(item.ToDoItemId);

//         // Assert
//         Assert.IsType<NoContentResult>(result);

//         var deletedItem = await context.ToDoItems.FindAsync(item.ToDoItemId);
//         Assert.Null(deletedItem);

//         // Cleanup
//         context.ToDoItems.RemoveRange(context.ToDoItems);
//         await context.SaveChangesAsync();
//     }
// [Fact]
// public void DeleteById_SecondItemBecomesFirst()
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
//     //Act
//     var result = controller.DeleteById(1);
//     var okResult = Assert.IsType<OkObjectResult>(result);
//     var value = Assert.IsAssignableFrom<IEnumerable<ToDoItem>>(okResult.Value);

//     //Assert
//     Assert.NotNull(value);

//     var firstToDo = value.First();
//     Assert.Equal(2, firstToDo.ToDoItemId);
// }
//}
