namespace ToDoList.Domain.DTOs;

using ToDoList.Domain.Models;
using System.ComponentModel.DataAnnotations;

public record ToDoItemCreateRequestDto([Required] string Name, string Description, bool IsCompleted, string? Category) //id nebude řešit client ale přidělí server
{
    public ToDoItem ToDomain() => new() { Name = Name, Description = Description, IsCompleted = IsCompleted, Category = Category };
}
