namespace ToDoList.WebApi;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[Route("api/[controller]")] //localhost:5000/api/ToDoItems
[ApiController]
public class ToDoItemsController : ControllerBase
{
    private static List<ToDoItem> items = [];
    [HttpPost]
    public IActionResult Create(ToDoItemCreateRequestDto request) //použijeme DTO - Data Transfer Object, request ptž to přichází od klienta
    {
        //return Ok();

        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            items.Add(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return Created(); //201 //tato metoda z nějakého důvodu vrací status code No Content 204, zjištujeme proč ;)
    }
    [HttpGet]
    public IActionResult Read() //api/ToDoItems GET
    {
        return Ok();
    }
    [HttpGet("{toDoItemId:int}")] //[HttpGet("read2")]
    public IActionResult ReadById(int toDoItemId) //api/ToDoItems/id GET
    {
        try
        {
            throw new Exception("neco se nepovedlo");
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //chci vrátit 500 if zavolám neexistující podadresář např. api/ToDoItems/876
        }
        return Ok(); //response status code 200
    }
    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {

        return Ok();
    }
    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId) //nechci umožnit smazat všechny úkoly najednou
    {
        return Ok();
    }
}

