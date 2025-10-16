namespace ToDoList.Test;

using System.ComponentModel;
using NuGet.Frameworks;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
public class UpdateTests
{
    [Fact]
    public void Update_ReturnsCorrectItemDescriptionAfterUpdate()
    {
        //Arrange
        var toDoItem1 = new ToDoItem()
        {
            ToDoItemId = 1,
            Name = "Jmeno1",
            Description = "Popis1",
            IsCompleted = false
        };
        var toDoItem2 = new ToDoItem()
        {
            ToDoItemId = 2,
            Name = "Jmeno2",
            Description = "Popis2",
            IsCompleted = true
        };
        var controller = new ToDoItemsController();
        controller.AddItemToStorage(toDoItem1);
        controller.AddItemToStorage(toDoItem2);

        string newDescription = "nový popis";

        //Act
        controller.UpdateById(1, toDoItem1.Description = newDescription);
        var result = controller.Read();

        //Assert
        Assert.Equal(newDescription, toDoItem1.Description);
    }
}
