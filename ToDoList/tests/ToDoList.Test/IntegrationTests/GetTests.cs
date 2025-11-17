namespace ToDoList.Test.IntegrationTests;

using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;

public class GetTests
{
    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        // Arrange
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);

        var todoItem1 = new ToDoItem
        {
            Name = "Jmeno1",
            Description = "Popis1",
            IsCompleted = false
        };
        var todoItem2 = new ToDoItem
        {
            Name = "Jmeno2",
            Description = "Popis2",
            IsCompleted = true
        };

        context.ToDoItems.Add(todoItem1);
        context.ToDoItems.Add(todoItem2);
        context.SaveChanges();

        // Act
        var result = controller.Read();
        var value = result.GetValue();

        // Assert
        Assert.NotNull(value);

        var firstToDo = value.First();
        Assert.Equal(todoItem1.ToDoItemId, firstToDo.Id);
        Assert.Equal(todoItem1.Name, firstToDo.Name);
        Assert.Equal(todoItem1.Description, firstToDo.Description);
        Assert.Equal(todoItem1.IsCompleted, firstToDo.IsCompleted);

        // Cleanup
        context.ToDoItems.Remove(todoItem1);
        context.ToDoItems.Remove(todoItem2);
        context.SaveChanges();
    }
}



// namespace ToDoList.Test.IntegrationTests;

// using NuGet.Frameworks;
// using Microsoft.AspNetCore.Mvc;
// using ToDoList.Domain.Models;
// using ToDoList.WebApi;
// using ToDoList.Domain.DTOs;

// public class GetTests
// {
//     [Fact]
//     public async Task Read_ReturnsAllItems()
//     {
//         // Arrange
//         await using var context = TestUtils.TestDbContextFactory.CreateTestDbContext();

//         context.ToDoItems.RemoveRange(context.ToDoItems); // Cleanup
//         await context.SaveChangesAsync();

//         context.ToDoItems.AddRange(
//             new ToDoItem { Name = "Task 1", Description = "First task", IsCompleted = false },
//             new ToDoItem { Name = "Task 2", Description = "Second task", IsCompleted = true }
//         );
//         await context.SaveChangesAsync();

//         var controller = new ToDoItemsController(context: context, repository: null); //if nepoužívám mock tak jen 1 parametr (context)

//         // Act
//         var result = await controller.Read();

//         // Assert
//         var okResult = Assert.IsType<OkObjectResult>(result.Result);
//         //var items = Assert.IsAssignableFrom<List<ToDoItem>>(okResult.Value);
//         var items = Assert.IsAssignableFrom<List<ToDoItemGetResponseDto>>(okResult.Value);

//         Assert.Equal(2, items.Count);
//         Assert.Contains(items, i => i.Name == "Task 1");
//         Assert.Contains(items, i => i.Name == "Task 2");

//         // Cleanup
//         context.ToDoItems.RemoveRange(context.ToDoItems);
//         await context.SaveChangesAsync();
//     }
//     [Fact]
//     public void Get_AllItems_ReturnsAllItems()
//     {
//         //Arrange
//         var toDoItem1 = new ToDoItem()
//         {
//             ToDoItemId = 1,
//             Name = "Jmeno1",
//             Description = "Popis1",
//             IsCompleted = false
//         };
//         var toDoItem2 = new ToDoItem()
//         {
//             ToDoItemId = 2,
//             Name = "Jmeno2",
//             Description = "Popis2",
//             IsCompleted = true
//         };
//         var controller = new ToDoItemsController();
//         controller.AddItemToStorage(toDoItem1);
//         controller.AddItemToStorage(toDoItem2);
//         //Act
//         var result = controller.Read();
//         var value = result.GetValue(); //řádek kvůli debugování
//                                        //Assert
//         Assert.NotNull(value);

//         var firstToDo = value.First();
//         Assert.Equal(1, firstToDo.Id); //manuálně
//         Assert.Equal(toDoItem1.ToDoItemId, firstToDo.Id); //nebo možno i takto
//         Assert.Equal(toDoItem1.Description, firstToDo.Description);
//         Assert.Equal(toDoItem1.IsCompleted, firstToDo.IsCompleted);
//     }
// }
