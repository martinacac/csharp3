namespace ToDoList.Test;

using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Mvc;

public class DeleteTests
{
    [Fact]
    public void DeleteById_SecondItemBecomesFirst()
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
        //Act
        var result = controller.DeleteById(1);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var value = Assert.IsAssignableFrom<IEnumerable<ToDoItem>>(okResult.Value);

        //Assert
        Assert.NotNull(value);

        var firstToDo = value.First();
        Assert.Equal(2, firstToDo.ToDoItemId);
    }
}
