namespace ToDoList.Frontend.Models;

//public record ToDoItemView(int Id, string Name, string Description, bool IsCompleted);
public class ToDoItemView
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}
