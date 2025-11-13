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
