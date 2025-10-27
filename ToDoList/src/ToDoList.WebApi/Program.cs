using Microsoft.EntityFrameworkCore;
using ToDoList.Persistence;
var builder = WebApplication.CreateBuilder(args);
{
    //configure DI
    //sem musíme přidat controllery takto:
    builder.Services.AddControllers();
    //builder.Services.AddDbContext<ToDoItemsContext>();
    //EF Core context
    builder.Services.AddDbContext<ToDoItemsContext>(options => options.UseSqlite("Data Source=../../data/localdb.db"));
}
var app = builder.Build();
{
    // Apply pending migrations at startup
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ToDoItemsContext>();
        db.Database.Migrate(); // applies migrations automatically
    }
    //configure Middleware (HTTP request pipeline)
    app.MapControllers(); //namapuji controllery
}


//app.MapGet("/", () => "Hello World!");
//app.MapGet("/test", () => "This is a test.");
//app.MapGet("/czechitas", () => "Vitej na kurzu Czechitas.");
//app.MapGet("/pozdrav/{jmeno}", (string jmeno) => $"Ahoj {jmeno}");
//app.MapGet("/nazdarSvete", () => $"Nazdar světe!");
//app.MapGet("/secti/{a:int}/{b:int}", (int a, int b) => $"Vysledek {a} + {b} = {a + b}");

app.Run();
