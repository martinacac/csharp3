namespace ToDoList.Test.IntegrationTests;

using NuGet.Frameworks;
using ToDoList.Domain.Models;
using ToDoList.WebApi;

public class GetTests
{
    // [Fact]
    // public void Get_AllItems_ReturnsAllItems()
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
    //     var result = controller.Read();
    //     var value = result.GetValue(); //řádek kvůli debugování
    //     //Assert
    //     Assert.NotNull(value);

    //     var firstToDo = value.First();
    //     Assert.Equal(1, firstToDo.Id); //manuálně
    //     Assert.Equal(toDoItem1.ToDoItemId, firstToDo.Id); //nebo možno i takto
    //     Assert.Equal(toDoItem1.Description, firstToDo.Description);
    //     Assert.Equal(toDoItem1.IsCompleted, firstToDo.IsCompleted);
    // }
}
