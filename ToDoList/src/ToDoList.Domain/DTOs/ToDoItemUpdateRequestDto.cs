namespace ToDoList.Domain.DTOs;

using ToDoList.Domain.Models;
using System.ComponentModel.DataAnnotations;

public record ToDoItemUpdateRequestDto([Required] string Name, string Description, bool IsCompleted, string? Category)
{
    public ToDoItem ToDomain() => new() { Name = Name, Description = Description, IsCompleted = IsCompleted, Category = Category };
}
