namespace ToDoList.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Models;

public class ToDoItemsRepository : IRepositoryAsync<ToDoItem> //implementace IRepository
{
    private readonly ToDoItemsContext context; //context přesunut z controlleru sem do repository

    public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;
    }
    public async Task Create(ToDoItem item) //add
    {
        context.ToDoItems.Add(item);
        await context.SaveChangesAsync();
    }

    public async Task<ToDoItem?> ReadById(int id) => await context.ToDoItems.FindAsync(id); //=> context.ToDoItems.Find(id);//místo {return context.ToDoItems.Find(id);}

    public async Task<IEnumerable<ToDoItem>> ReadAll() //GetAll()
    {
        return await context.ToDoItems.ToListAsync();
    }

    public async Task DeleteById(int id)
    {
        var item = context.ToDoItems.Find(id);
        if (item != null)
        {
            context.ToDoItems.Remove(item);
            await context.SaveChangesAsync();
        }
    }

    public async Task Update(ToDoItem item)
    {
        context.ToDoItems.Update(item);
        await context.SaveChangesAsync();
    }
}

