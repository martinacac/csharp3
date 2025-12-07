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
    public async Task CreateAsync(ToDoItem item) //add
    {
        await context.ToDoItems.AddAsync(item);
        await context.SaveChangesAsync();
    }

    public async Task<ToDoItem?> ReadByIdAsync(int id) => await context.ToDoItems.FindAsync(id); //=> context.ToDoItems.Find(id);//místo {return context.ToDoItems.Find(id);}

    public async Task<IEnumerable<ToDoItem>> ReadAllAsync() //GetAll()
    {
        return await context.ToDoItems.ToListAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var item = await context.ToDoItems.FindAsync(id);
        if (item != null)
        {
            context.ToDoItems.Remove(item);
            await context.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(ToDoItem item)
    {
        //context.ToDoItems.Update(item);
        //await context.SaveChangesAsync();

        var foundItem = await context.ToDoItems.FindAsync(item.ToDoItemId) ?? throw new ArgumentOutOfRangeException($"ToDo item with ID {item.ToDoItemId} not found.");
        context.Entry(foundItem).CurrentValues.SetValues(item);
        await context.SaveChangesAsync();
    }
}

