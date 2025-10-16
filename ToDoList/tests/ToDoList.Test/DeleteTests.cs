namespace ToDoList.Test;

using ToDoList.Domain.Models;
using ToDoList.WebApi;

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
        var value = result.GetValue();
        //Assert
        Assert.NotNull(value);

        var firstToDo = value.First();
        Assert.Equal(2, firstToDo.Id);
    }
}
