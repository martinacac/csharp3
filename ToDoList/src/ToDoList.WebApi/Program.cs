using ToDoList.Persistence;
var builder = WebApplication.CreateBuilder(args);
{
    //configure DI
    //sem musíme přidat controllery takto:
    builder.Services.AddControllers();
    builder.Services.AddDbContext<ToDoItemsContext>();
}
var app = builder.Build();
{
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
