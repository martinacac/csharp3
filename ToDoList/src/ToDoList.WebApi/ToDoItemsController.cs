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
    private readonly IRepository<ToDoItem> repository;

    public ToDoItemsController(IRepository<ToDoItem> repository)
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
    public ActionResult<ToDoItemGetResponseDto> Create(ToDoItemCreateRequestDto request)
    {
        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            repository.Create(item);
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
        //List<ToDoItem> itemsToGet;

        try
        {
            //itemsToGet = context.ToDoItems.ToList();
            var items = repository != null
            ? repository.ReadAll()
            : await context.ToDoItems.ToListAsync();
            if (items is null)
            {
                return NotFound(); // 404
            }
            var response = items.Select(ToDoItemGetResponseDto.FromDomain).ToList(); //.ToList()
            return Ok(response);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        // return (itemsToGet is null)
        //     ? NotFound() //404
        //     : Ok(itemsToGet.Select(ToDoItemGetResponseDto.FromDomain)); //200
    }

    [HttpGet("{toDoItemId:int}")]
    public ActionResult<ToDoItemGetResponseDto> ReadById(int toDoItemId)
    {
        //try to retrieve the item by id
        ToDoItem? itemToGet;
        try
        {
            //itemToGet = context.ToDoItems.Find(toDoItemId);
            itemToGet = repository.ReadById(toDoItemId);
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

        //try to update the item by retrieving it with given id
        try
        {
            //retrieve the item
            //var itemToUpdate = repository?.ReadById(toDoItemId); //context.ToDoItems.Find(toDoItemId);
            //Použij context, pokud není repository
            var itemToUpdate = repository != null
                ? repository.ReadById(toDoItemId)
                : await context.ToDoItems.FindAsync(toDoItemId);
            if (itemToUpdate is null)
            {
                return NotFound(); //404
            }

            // Update the entity properties
            itemToUpdate.Name = updatedItem.Name;
            itemToUpdate.Description = updatedItem.Description;
            itemToUpdate.IsCompleted = updatedItem.IsCompleted;

            if (repository != null)
            {
                repository.Update(itemToUpdate);
            }
            else
            {
                context.ToDoItems.Update(itemToUpdate);
                await context.SaveChangesAsync(); // nezapomeň uložit
            }

            return NoContent(); // 204
            //context.SaveChanges();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        //return NoContent(); //204
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        //try to delete the item
        try
        {
            //var itemToDelete = context.ToDoItems.Find(toDoItemId);
            var itemToDelete = repository.ReadById(toDoItemId);
            if (itemToDelete is null)
            {
                return NotFound(); //404
            }
            //context.ToDoItems.Remove(itemToDelete);
            //context.SaveChanges();
            repository.DeleteById(toDoItemId);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        //respond to client
        return NoContent(); //204
    }

    // [HttpPost]
    // //public ActionResult<ToDoItemGetResponseDto> Create(ToDoItemCreateRequestDto request) //použijeme DTO - Data Transfer Object, request ptž to přichází od klienta
    // public async Task<ActionResult<ToDoItemGetResponseDto>> Create(ToDoItemCreateRequestDto request)
    // {
    //     //return Ok();
    //     if (request == null)
    //     {
    //         return Problem("Request cannot be null.", null, StatusCodes.Status500InternalServerError);
    //     }

    //     if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Description) || string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Description))
    //     {
    //         return BadRequest("Name and Description are required.");
    //     }
    //     //map to Domain object as soon as possible
    //     var item = request.ToDomain();

    //     //try to create an item
    //     try
    //     {
    //         //item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
    //         //items.Add(item);

    //         //context.ToDoItems.Add(item);
    //         //context.SaveChanges();
    //         //await context.ToDoItems.AddAsync(item);
    //         //await context.SaveChangesAsync();
    //         //repository.Create(item);
    //         repository.Create(item);
    //     }
    //     catch (Exception ex)
    //     {
    //         return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
    //     }

    //     //respond to client
    //     return CreatedAtAction(
    //         nameof(ReadById),
    //         new { toDoItemId = item.ToDoItemId },
    //         ToDoItemGetResponseDto.FromDomain(item)); //201
    // }

    // [HttpGet]
    // //public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read() //api/ToDoItems GET
    // public async Task<ActionResult<List<ToDoItem>>> Read()
    // {
    //     //List<ToDoItem> itemsToGet;
    //     try
    //     {
    //         //itemsToGet = items;
    //         var items = await context.ToDoItems.ToListAsync();
    //         return Ok(items);
    //     }
    //     catch (Exception ex)
    //     {
    //         return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
    //     }

    //     //respond to client
    //     //return (itemsToGet is null)
    //     //    ? NotFound() //404
    //     //    : Ok(itemsToGet.Select(ToDoItemGetResponseDto.FromDomain)); //200
    // }

    // [HttpGet("{toDoItemId:int}")] //[HttpGet("read2")] //api/ToDoItems/id GET
    // //public ActionResult<ToDoItemGetResponseDto> ReadById(int toDoItemId)
    // public async Task<ActionResult<ToDoItemGetResponseDto>> ReadById(int toDoItemId)
    // {
    //     //try to retrieve the item by id
    //     //ToDoItem? itemToGet;
    //     try
    //     {
    //         //itemToGet = items.Find(i => i.ToDoItemId == toDoItemId);
    //         var itemToGet = await context.ToDoItems.FindAsync(toDoItemId);
    //         if (itemToGet == null)
    //             return NotFound();

    //         return Ok(ToDoItemGetResponseDto.FromDomain(itemToGet));
    //     }
    //     catch (Exception ex)
    //     {
    //         return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
    //     }

    //     //respond to client
    //     //return (itemToGet is null)
    //     //    ? NotFound() //404
    //     //    : Ok(ToDoItemGetResponseDto.FromDomain(itemToGet)); //200
    // }
    // [HttpPut("{toDoItemId:int}")]
    // //public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    // public async Task<IActionResult> UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    // {
    //     //map to Domain object as soon as possible
    //     var updatedItem = request.ToDomain();

    //     //try to update the item by retrieving it with given id
    //     try
    //     {
    //         //retrieve the item
    //         //var itemIndexToUpdate = items.FindIndex(i => i.ToDoItemId == toDoItemId);
    //         //if (itemIndexToUpdate == -1)
    //         //{
    //         //    return NotFound(); //404
    //         //}
    //         //updatedItem.ToDoItemId = toDoItemId;
    //         //items[itemIndexToUpdate] = updatedItem;

    //         var itemToUpdate = await context.ToDoItems.FindAsync(toDoItemId);
    //         if (itemToUpdate == null)
    //             return NotFound();

    //         itemToUpdate.Name = updatedItem.Name;
    //         itemToUpdate.Description = updatedItem.Description;
    //         itemToUpdate.IsCompleted = updatedItem.IsCompleted;

    //         await context.SaveChangesAsync();
    //     }
    //     catch (Exception ex)
    //     {
    //         return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
    //     }

    //     //respond to client
    //     return NoContent(); //204
    // }

    // [HttpDelete("{toDoItemId:int}")]
    // //public IActionResult DeleteById(int toDoItemId) //nechci umožnit smazat všechny úkoly najednou
    // public async Task<IActionResult> DeleteById(int toDoItemId)
    // {
    //     //try to delete the item
    //     try
    //     {
    //         //var itemToDelete = items.Find(i => i.ToDoItemId == toDoItemId);
    //         //if (itemToDelete is null)
    //         //{
    //         //    return NotFound(); //404
    //         //
    //         //items.Remove(itemToDelete);
    //         var itemToDelete = await context.ToDoItems.FindAsync(toDoItemId);
    //         if (itemToDelete == null)
    //             return NotFound();

    //         context.ToDoItems.Remove(itemToDelete);
    //         await context.SaveChangesAsync();
    //     }
    //     catch (Exception ex)
    //     {
    //         return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
    //     }

    //     //respond to client
    //     return NoContent(); //204
    //     //return Ok(items);
    // }

    // //public void AddItemToStorage(ToDoItem item) //this method is no longer needed — use EF Core and DbContext instead.
    // //{
    // //    items.Add(item);
    // //}
}

