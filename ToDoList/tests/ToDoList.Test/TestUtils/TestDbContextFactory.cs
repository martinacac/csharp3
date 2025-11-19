// using System;

// namespace ToDoList.Test.TestUtils;

// using Microsoft.EntityFrameworkCore;
// using ToDoList.Persistence;

// public static class TestDbContextFactory
// {
//     public static ToDoItemsContext CreateTestDbContext()
//     {
//         // Build a safe, relative path to the test database
//         var baseDir = Directory.GetCurrentDirectory();
//         var dbDir = Path.Combine(baseDir, "IntegrationTests", "data");
//         Directory.CreateDirectory(dbDir); // ensure folder exists

//         var dbPath = Path.Combine(dbDir, "localdb_test.db");
//         var connectionString = $"Data Source={dbPath}";

//         var options = new DbContextOptionsBuilder<ToDoItemsContext>()
//             .UseSqlite(connectionString)
//             .Options;

//         var context = new ToDoItemsContext(options);
//         context.Database.EnsureDeleted();
//         //context.Database.EnsureCreated();
//         context.Database.Migrate(); // creates + applies migrations

//         return context;
//     }
// }
