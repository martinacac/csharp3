using System;
using ToDoList.Domain.Models;

namespace ToDoList.Domain.DTOs;

public record ToDoItemCreateRequestDto(string Name, string Description, bool IsCompleted) //id nebude řešit client ale přidělí server
{
    public ToDoItem ToDomain() => new() { Name = Name, Description = Description, IsCompleted = IsCompleted };
}
