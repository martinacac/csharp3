using System;

namespace ToDoList.Test.TestUtils;

using Microsoft.EntityFrameworkCore;
using ToDoList.Persistence;

public static class TestDbContextFactory
{
    public static ToDoItemsContext CreateTestDbContext()
    {
        var options = new DbContextOptionsBuilder<ToDoItemsContext>()
            .UseSqlite("Data Source=C:/Users/w/csharp3/csharp3/ToDoList/tests/ToDoList.Test/IntegrationTests/data/localdb_test.db")
            .Options;

        var context = new ToDoItemsContext(options);
        context.Database.Migrate(); // applies migrations
        //context.Database.EnsureDeleted(); // optional: reset before each test

        context.Database.EnsureCreated();
        return context;

    }
}
