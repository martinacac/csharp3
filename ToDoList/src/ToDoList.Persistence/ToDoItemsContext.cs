namespace ToDoList.Persistence;

using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Models;

public class ToDoItemsContext : DbContext
{
    private readonly string connectionString;
    public ToDoItemsContext(string connectionString = "DataSource=../../data/localdb.db")
    {
        this.connectionString = connectionString;
        this.Database.Migrate();
    }
    public DbSet<ToDoItem> ToDoItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString);
    }
}

// public class ToDoItemsContext : DbContext
// {
//     public ToDoItemsContext(DbContextOptions<ToDoItemsContext> options) : base(options) { }
//     //private readonly string connectionString;
//     //public ToDoItemsContext(string connectionString = "DataSource=../../data/localdb.db")
//     //{
//     //    this.connectionString = connectionString;
//     //    this.Database.Migrate();
//     //}
//     public DbSet<ToDoItem> ToDoItems { get; set; }

//     //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     //{
//     //    optionsBuilder.UseSqlite(connectionString);
//     //}
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<ToDoItem>()
//             .HasKey(t => t.ToDoItemId);

//         modelBuilder.Entity<ToDoItem>()
//             .Property(t => t.ToDoItemId)
//             .ValueGeneratedOnAdd(); // důležité pro testy a SQLite
//     }

// }


