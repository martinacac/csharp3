using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
var builder = WebApplication.CreateBuilder(args);
{
    //configure DI
    //sem musíme přidat controllery takto:
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();
    //builder.Services.AddDbContext<ToDoItemsContext>();
    //EF Core context
    builder.Services.AddDbContext<ToDoItemsContext>(options => options.UseSqlite("Data Source=../../data/localdb.db"));
    builder.Services.AddScoped<IRepository<ToDoItem>, ToDoItemsRepository>(); //když se odkazuji na IRepository<ToDoItem> odkáže mě to na ToDoItemsRepository (implementace) a po celou dobu zpracování požadavku to bude stejná instance
    //AddTransient - dává pokaždé jinou instanci
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
    app.UseSwagger();
    app.UseSwaggerUI(config => config.SwaggerEndpoint("v1/swagger.json", "ToDoList API V1"));
}


//app.MapGet("/", () => "Hello World!");
//app.MapGet("/test", () => "This is a test.");
//app.MapGet("/czechitas", () => "Vitej na kurzu Czechitas.");
//app.MapGet("/pozdrav/{jmeno}", (string jmeno) => $"Ahoj {jmeno}");
//app.MapGet("/nazdarSvete", () => $"Nazdar světe!");
//app.MapGet("/secti/{a:int}/{b:int}", (int a, int b) => $"Vysledek {a} + {b} = {a + b}");

app.Run();
