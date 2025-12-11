using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;

public abstract class IntegrationTestBase : IDisposable
{
    protected readonly string ConnectionString =
        "Data Source=../../../IntegrationTests/data/localdb_test.db";

    protected readonly ToDoItemsContext Context;
    protected readonly ToDoItemsRepository Repository;
    protected readonly ToDoItemsController Controller;

    protected IntegrationTestBase()
    {
        Context = new ToDoItemsContext(ConnectionString);
        Repository = new ToDoItemsRepository(Context);
        Controller = new ToDoItemsController(Repository);

    }

    public void Dispose()
    {
        // uklid po testu
        Context.Dispose();
    }
}
