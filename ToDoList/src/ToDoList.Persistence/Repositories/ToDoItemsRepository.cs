namespace ToDoList.Persistence.Repositories;

using ToDoList.Domain.Models;

public class ToDoItemsRepository : IRepository<ToDoItem> //implementace IRepository
{
    private readonly ToDoItemsContext context; //context přesunut z controlleru sem do repository

    public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;
    }
    public void Create(ToDoItem item) //add
    {
        context.ToDoItems.Add(item);
        context.SaveChanges();
    }

    public ToDoItem? ReadById(int id) => context.ToDoItems.Find(id);//místo {return context.ToDoItems.Find(id);}

    public IEnumerable<ToDoItem> ReadAll() //GetAll()
    {
        return context.ToDoItems.ToList();
    }

    public void DeleteById(int id)
    {
        var item = context.ToDoItems.Find(id);
        if (item != null)
        {
            context.ToDoItems.Remove(item);
            context.SaveChanges();
        }
    }

    public void Update(ToDoItem item)
    {
        context.ToDoItems.Update(item);
        context.SaveChanges();
    }
}

