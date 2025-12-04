namespace ToDoList.Domain.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ToDoItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ToDoItemId { get; set; } //EF core looks for <id> nebo <nameId> ale raději to označení pomocí Key viz výše
    [Length(1, 50)] //vyžadovaná min a max délka
    public string Name { get; set; }
    [StringLength(250)] //max délka popisku
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public string? Category { get; set; }
}
