namespace ToDoList.Domain.Models;

using System.ComponentModel.DataAnnotations;

public class ToDoItem
{
    [Key]
    public int ToDoItemId { get; set; } //EF core looks for <id> nebo <nameId> ale raději to označení pomocí Key viz výše
    [Length(1, 50)] //vyžadovaná min a max délka
    public string Name { get; set; }
    [StringLength(250)] //max délka popisku
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}
