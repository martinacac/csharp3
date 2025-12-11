namespace ToDoList.Frontend.Models;

using System.ComponentModel.DataAnnotations;

//public record ToDoItemView(int Id, string Name, string Description, bool IsCompleted);
public class ToDoItemView
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is mandatory.")]
    public string Name { get; set; }
    [StringLength(250)]
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public string? Category { get; set; }
}
