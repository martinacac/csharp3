namespace ToDoList.WebApi;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

[Route("api/[controller]")] //localhost:5000/api/ToDoItems
[ApiController]
public class ToDoItemsController : ControllerBase
{
    //public readonly List<ToDoItem> items = []; //po dopsání úkolu již není potřeba a můžeme smazat
    //private readonly ToDoItemsContext context; //!!!dát pryč - už je v Repository
    private readonly IRepositoryAsync<ToDoItem> repository;

    public ToDoItemsController(IRepositoryAsync<ToDoItem> repository)
    //public ToDoItemsController(ToDoItemsContext context, IRepository<ToDoItem> repository)
    {
        //this.context = context;
        this.repository = repository;
        //vytvoření úkolu pro odzkoušení a jeho uložení do tabulky ToDoItems (viz DbSet v ToDoItemsContext):
        //ToDoItem item = new ToDoItem { Name = "Prvni ukol", Description = "prvni popisek", IsCompleted = false };
        //context.ToDoItems.Add(item);
        //context.SaveChanges();
    }
    [HttpPost]
    public async Task<ActionResult<ToDoItemGetResponseDto>> Create(ToDoItemCreateRequestDto request)
    {
        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            await repository.CreateAsync(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return CreatedAtAction(
            nameof(ReadById),
            new { toDoItemId = item.ToDoItemId },
            ToDoItemGetResponseDto.FromDomain(item)); //201
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoItemGetResponseDto>>> Read()
    {
        IEnumerable<ToDoItem> itemsToGet;
        try
        {
            itemsToGet = await repository.ReadAllAsync();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return (itemsToGet is null)
            ? NotFound() //404
            : Ok(itemsToGet.Select(ToDoItemGetResponseDto.FromDomain)); //200
    }

    [HttpGet("{toDoItemId:int}")]
    public async Task<ActionResult<ToDoItemGetResponseDto>> ReadById(int toDoItemId)
    {
        //try to retrieve the item by id
        ToDoItem? itemToGet;
        try
        {
            itemToGet = await repository.ReadByIdAsync(toDoItemId);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return (itemToGet is null)
            ? NotFound() //404
            : Ok(ToDoItemGetResponseDto.FromDomain(itemToGet)); //200
    }

    [HttpPut("{toDoItemId:int}")]
    public async Task<IActionResult> UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        //map to Domain object as soon as possible
        var updatedItem = request.ToDomain();
        updatedItem.ToDoItemId = toDoItemId;

        //try to update the item by retrieving it with given id
        try
        {
            //retrieve the item
            var itemToUpdate = await repository.ReadByIdAsync(toDoItemId);
            if (itemToUpdate is null)
            {
                return NotFound(); //404
            }

            await repository.UpdateAsync(updatedItem);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return NoContent(); //204
    }

    [HttpDelete("{toDoItemId:int}")]
    public async Task<IActionResult> DeleteById(int toDoItemId)
    {
        //try to delete the item
        try
        {
            var itemToDelete = await repository.ReadByIdAsync(toDoItemId);
            if (itemToDelete is null)
            {
                return NotFound(); //404
            }

            await repository.DeleteByIdAsync(toDoItemId);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        //respond to client
        return NoContent(); //204
    }
}
